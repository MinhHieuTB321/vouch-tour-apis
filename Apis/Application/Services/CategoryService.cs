using Application.Commons;
using Application.GlobalExceptionHandling.Exceptions;
using Application.Interfaces;
using Application.ViewModels.CartDTO;
using Application.ViewModels.CategoryDTO;
using Application.ViewModels.Product;
using AutoMapper;
using Domain.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobClient _jobClient;
        private readonly IConfiguration _config;
        private readonly IFirebaseConfig _fireBaseConfig;
        private readonly IFirebaseClient _client;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IBackgroundJobClient jobClient,
            IConfiguration config)
        {
                _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jobClient=jobClient;
            _config = config;
            _fireBaseConfig = new FirebaseConfig
            {
                AuthSecret = _config["RealTimeDatabase:AuthSecret"],
                BasePath = _config["RealTimeDatabase:BasePath"],
            };
            _client = new FireSharp.FirebaseClient(_fireBaseConfig);
        }

        public async Task<CategoryViewDTO> Create(CategoryCreateDTO categoryCreateDTO)
        {
            var file = await categoryCreateDTO.File.UploadFileAsync();
            var createDTO = _mapper.Map<Category>(categoryCreateDTO);
            createDTO.FileName = file.FileName;
            createDTO.URL = file.URL;
            var result =await _unitOfWork.CategoryRepository.AddAsync(createDTO);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CategoryViewDTO>(result);
        }

        public async Task<bool> DeleteById(Guid Id)
        {
            var category=await _unitOfWork.CategoryRepository.GetByIdAsync(Id,x=>x.Products);
            if (category == null) throw new NotFoundException("Can not found category in system!");
             _unitOfWork.CategoryRepository.SoftRemove(category);
            var result = await _unitOfWork.SaveChangeAsync();
            var productIds= category.Products.Select(x=>x.Id).ToList();
            _jobClient.Enqueue(() => DeleteProduct(productIds));
            _jobClient.Enqueue(() => CheckAllCart(productIds));
            _jobClient.Enqueue(() => DeleteProductInMenu(productIds));
            return result > 0;

        }


        public async Task DeleteProduct(List<Guid> products)
        {
            if (products.Count == 0) return;
            var removeList = await _unitOfWork.ProductRepository.FindListByField(x => products.Contains(x.Id));
            _unitOfWork.ProductRepository.SoftRemoveRange(removeList);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteProductInMenu(List<Guid> products)
        {
            var listProductMenus = await _unitOfWork.ProductMenuRepository.FindListByField(x => products.Contains(x.ProductId!.Value));
            if (listProductMenus.Count == 0) return;
            _unitOfWork.ProductMenuRepository.SoftRemoveRange(listProductMenus);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task CheckAllCart(List<Guid> products)
        {
            var tourGuide = await _unitOfWork.TourGuideRepository.GetAllAsync();
            for (int i = 0; i < tourGuide.Count; i++)
            {
                await DeleteProductInCart(products, tourGuide[i].Id);
            }
        }

        public async Task DeleteProductInCart(List<Guid> products, Guid tourGuideId)
        {
            var root = "Cart-" + tourGuideId;
            FirebaseResponse response = await _client.GetAsync(root);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<ItemViewDTO>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<ItemViewDTO>(((JProperty)item).Value.ToString()));
                }
            }
            if (list.Count == 0) return;
            var removeList = list.Where(x => products.Contains(x.ProductId)).ToList();
            if (removeList.Count == 0) return;
            for (int i = 0; i < removeList.Count; i++)
            {
                await _client.DeleteAsync($"{root}/" + removeList[i].Id);
            }
        }


        public async Task<IEnumerable<CategoryViewDTO>> GetAll()
        {
            var result =  await _unitOfWork.CategoryRepository.GetAllAsync();
            if (result.Count() > 0) return _mapper.Map<IEnumerable<CategoryViewDTO>>(result);
            else throw new Exception("Not have any category");
        }

        public async Task<CategoryViewDTO> GetById(Guid id)
        {
            var result = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (result is not null) return _mapper.Map<CategoryViewDTO>(result);
            else throw new Exception("Not Found");
        }

        public async Task<List<ViewProductDTO>> GetProuductByCategoryId(Guid Id)
        {
            var products=await _unitOfWork.ProductRepository.FindListByField(x=>x.CategoryId==Id && x.IsDeleted==false);
            if (products.Count() > 0)
            {
                return _mapper.Map<List<ViewProductDTO>>(products);
            }
            throw new NotFoundException("Not Found");
        }

        public async Task<bool> Update(CategoryUpdateDTO catregoryUpdateDTO)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(catregoryUpdateDTO.Id);
            if (category == null) throw new NotFoundException("Can not found category in system!");
            var removeFile= await category.FileName.RemoveFileAsync();
            if (!removeFile) throw new BadRequestException("Can not delete file!");
            var updateFile = await catregoryUpdateDTO.File.UploadFileAsync();
            //category=_mapper.Map(catregoryUpdateDTO, category);
            category.CategoryName = catregoryUpdateDTO.CategoryName;
            category.FileName = updateFile.FileName;
            category.URL = updateFile.URL;
            _unitOfWork.CategoryRepository.Update(category);
            var result=await _unitOfWork.SaveChangeAsync();
            return (result > 0);
        }
    }
}
