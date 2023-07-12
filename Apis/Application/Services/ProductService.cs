using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.CartDTO;
using Application.ViewModels.Product;
using AutoMapper;
using Domain.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Hangfire;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        private readonly IBackgroundJobClient _jobClient;
        private readonly IConfiguration _config;
        private readonly IFirebaseConfig _fireBaseConfig;
        private readonly IFirebaseClient _client;

        #region Contructor
        public ProductService( IMapper mapper, 
            IUnitOfWork unitOfWork,
            IClaimsService claimsService,
            IBackgroundJobClient jobClient,
            IConfiguration config)
        {
           _jobClient=jobClient;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
            _config = config;
            _fireBaseConfig = new FirebaseConfig
            {
                AuthSecret = _config["RealTimeDatabase:AuthSecret"],
                BasePath = _config["RealTimeDatabase:BasePath"],
            };
            _client = new FireSharp.FirebaseClient(_fireBaseConfig);
        }

        #endregion

        #region Create Product
        public async Task<Product> CreateProduct(CreateProductDTO createDTO)
        {   
            var createItem = _mapper.Map<Product>(createDTO);
            createItem.SupplierId = _claimsService.GetCurrentUser;
            if (createItem != null)
            {
                await _unitOfWork.ProductRepository.AddAsync(createItem);
                if (await _unitOfWork.SaveChangeAsync() > 0) return createItem;
                else throw new Exception("Save changes failed");
            }
            else throw new Exception("Error Mapping");
        }

        #endregion

        #region Delete Product
        public async Task<bool> DeleteProduct(Guid id)
        {
            var deletedItem = await _unitOfWork.ProductRepository.FindByField(x=>x.Id==id && x.SupplierId==_claimsService.GetCurrentUser, x => x.Images);
            if (deletedItem != null)
            {
                _unitOfWork.ProductRepository.SoftRemove(deletedItem);
                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    _jobClient.Enqueue(() => DeleteProductInMenu(id));
                    _jobClient.Enqueue(() => CheckAllCart(id));
                    return true;
                }
                else throw new Exception("Save change failed!");

            }
            else throw new Exception("Not found");
        }

        public async Task DeleteProductInMenu(Guid productId)
        {
            var listProductMenus = await _unitOfWork.ProductMenuRepository.FindListByField(x => x.ProductId==productId);
            if (listProductMenus.Count == 0) return;
            _unitOfWork.ProductMenuRepository.SoftRemoveRange(listProductMenus);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task CheckAllCart(Guid productId)
        {
            var tourGuide = await _unitOfWork.TourGuideRepository.GetAllAsync();
            for (int i = 0; i < tourGuide.Count; i++)
            {
                await DeleteProductInCart(productId, tourGuide[i].Id);
            }
        }

        public async Task DeleteProductInCart(Guid productId, Guid tourGuideId)
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
            var removeList = list.Where(x => x.ProductId==productId).ToList();
            if (removeList.Count == 0) return;
            for (int i = 0; i < removeList.Count; i++)
            {
                await _client.DeleteAsync($"{root}/" + removeList[i].Id);
            }
        }

        #endregion

        #region Get All Product
        public async Task<IEnumerable<ViewProductDTO>> GetAll()
        {
            var result =  await _unitOfWork.ProductRepository.GetAllAsync(x => x.Images, x => x.Category, x=> x.Supplier);
            if (result.Count > 0)
            {
                result = result.OrderByDescending(x => x.CreationDate).ToList();
                return _mapper.Map<List<ViewProductDTO>>(result);
            }
            else throw new Exception("Not have any product");


        }
        #endregion

        #region GetProduct By Id
        public async Task<ViewProductDTO> GetById(Guid id)
        {
            var result = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (result is not null) return _mapper.Map<ViewProductDTO>(result);
            else throw new Exception("Not found");
        }
        #endregion

        #region Update Product
        public async Task<bool> UpdateProduct(UpdateProductDTO updateDTO)
        {
            var updatedItem = await _unitOfWork.ProductRepository.GetByIdAsync(updateDTO.ProductId);
            if (updatedItem != null)
            {
                updatedItem = (Product)_mapper.Map(updateDTO, typeof(UpdateProductDTO), typeof(Product));
                _unitOfWork.ProductRepository.Update(updatedItem);
                if (await _unitOfWork.SaveChangeAsync() > 0) return true;
                else throw new Exception("Save change failed!");
            }
            else throw new Exception("Not found");
        }
        #endregion
    }
}
