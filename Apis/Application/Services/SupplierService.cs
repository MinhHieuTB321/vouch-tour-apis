using Application.Interfaces;
using Application.ViewModels.Product;
using Application.ViewModels.SupplierDTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.GlobalExceptionHandling.Exceptions;
using Hangfire;
using FireSharp.Interfaces;
using Microsoft.Extensions.Configuration;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Application.ViewModels.CartDTO;

namespace Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        private readonly IBackgroundJobClient _jobClient;
        private readonly IConfiguration _config;
        private readonly IFirebaseConfig _fireBaseConfig;
        private readonly IFirebaseClient _client;
        public SupplierService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IClaimsService claimsService,
            IBackgroundJobClient jobClient,
            IConfiguration config)
        {
             _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
            _jobClient = jobClient;
            _config = config;
            _fireBaseConfig = new FirebaseConfig
            {
                AuthSecret = _config["RealTimeDatabase:AuthSecret"],
                BasePath = _config["RealTimeDatabase:BasePath"],
            };
            _client = new FireSharp.FirebaseClient(_fireBaseConfig);
        }

        public async Task<SupplierViewDTO> Create(SupplierCreateDTO createdItem)
        {
            var createDTO = _mapper.Map<Supplier>(createdItem);
            createDTO.AdminId = _claimsService.GetCurrentUser;
            var supplier= await _unitOfWork.SupplierRepository.AddSupplierAsync(createDTO);
            var newUser = new User{UserId=supplier.Id,RoleId=2,Email=createdItem.Email}; 
            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<SupplierViewDTO>(supplier);
        }
        
        public async Task<bool> Delete(Guid id)
        {
            var supplier=await _unitOfWork.SupplierRepository.GetByIdAsync(id,x=>x.Products);
            var user= await _unitOfWork.UserRepository.FindByField(x=>x.UserId==id);
            if(supplier == null)
            {
                throw new NotFoundException("Supplier is not exist in system!");
            }
            _unitOfWork.SupplierRepository.SoftRemove(supplier);
            _unitOfWork.UserRepository.SoftRemove(user);
            var result = await _unitOfWork.SaveChangeAsync();
            var products = supplier.Products.Select(x=>x.Id).ToList();
            _jobClient.Enqueue(() => DeleteProduct(products));
            _jobClient.Enqueue(() => CheckAllCart(products));
            _jobClient.Enqueue(() => DeleteProductInMenu(products));
            return result > 0;
        }

        #region DeleteProduct
        public async Task DeleteProduct(List<Guid> products)
        {
            if (products.Count == 0) return;
            var removeList = await _unitOfWork.ProductRepository.FindListByField(x=>products.Contains(x.Id));
            _unitOfWork.ProductRepository.SoftRemoveRange(removeList);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteProductInMenu(List<Guid> products)
        {
            var listProductMenus= await _unitOfWork.ProductMenuRepository.FindListByField(x=>products.Contains(x.ProductId!.Value));
            if (listProductMenus.Count == 0) return;
            _unitOfWork.ProductMenuRepository.SoftRemoveRange(listProductMenus);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task CheckAllCart(List<Guid> products)
        {
            var tourGuide= await _unitOfWork.TourGuideRepository.GetAllAsync();
            for (int i = 0; i < tourGuide.Count; i++)
            {
                await DeleteProductInCart(products, tourGuide[i].Id);
            }
        }

        public async Task DeleteProductInCart(List<Guid> products,Guid tourGuideId)
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
            if(removeList.Count== 0) return;
            for (int i = 0; i < removeList.Count; i++)
            {
                await _client.DeleteAsync($"{root}/" + removeList[i].Id);
            }
        }

        #endregion

        public async Task<IEnumerable<SupplierViewDTO>> GetAll()
        {
            var result = await _unitOfWork.SupplierRepository.GetAllAsync();
            if (result.Count > 0) return _mapper.Map<IEnumerable<SupplierViewDTO>>(result);
            else throw new Exception("Not have any supplier");
        }

        #region Get Supplier by Id
        public async Task<SupplierViewDTO> GetById(Guid id)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(id,x=>x.Products);
            if (supplier != null)
            {
                var result= _mapper.Map<SupplierViewDTO>(supplier);
                return result;
            }
            else throw new Exception("Not found");
        }
        #endregion

        #region Get Supplier Report
        public async Task<SupplierReport> GetSupplierReportById(Guid id)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(id, x => x.Products);
            if (supplier != null)
            {
                var result = await GetSupplierReport(supplier.Products.ToList());
                result = _mapper.Map(supplier, result);
                return result;
            }
            else throw new Exception("Not found");
        }
        private async Task<SupplierReport> GetSupplierReport(List<Product> products)
        {
            var listProductId = products.Where(x => x.IsDeleted == false).Select(x => x.Id).ToList();
            var listProductInMenu = await GetNumberProductInMenu(listProductId);
            var report = new SupplierReport()
            {
                NumberOfProducts = listProductId.Count,
                NumberOfProductInMenu = GetNumberProductInMenu(listProductInMenu),
                NumberOfProductOutMenu = listProductId.Count - GetNumberProductInMenu(listProductInMenu),
                NumberOfProductSold = listProductInMenu.Sum(x => x.OrderDetails.Sum(x => x.Quantity)),
                TotalMoneySold = listProductInMenu.Sum(x => x.OrderDetails.Sum(o => o.UnitPrice * o.Quantity))
            };
           
            return report;
        }

        private async Task<List<ProductInMenu>> GetNumberProductInMenu(List<Guid> listProductId)
        {
            var productInMenu= await _unitOfWork.ProductMenuRepository
                .FindListByField(x=>
                    listProductId.Contains(x.ProductId!.Value) && 
                    x.IsDeleted==false,
                    x=>x.OrderDetails);
            return productInMenu;
        }

        private int GetNumberProductInMenu(List<ProductInMenu> productInMenus)
        {
            productInMenus = productInMenus.DistinctBy(x => x.ProductId).ToList();
            return productInMenus.Count;
        }

        #endregion

        public async Task<bool> Update(SupplierUpdateDTO updatedItem)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(_claimsService.GetCurrentUser);
            if(supplier == null)
            {
                throw new NotFoundException("Suppler is not exist!");
            }
            supplier = _mapper.Map(updatedItem,supplier);
            _unitOfWork.SupplierRepository.Update(supplier);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;
            
        }

        public async Task<List<ViewProductDTO>> GetProductBySupplierId(Guid supplierId)
        {
            var result = await _unitOfWork.ProductRepository.FindListByField(x=>x.SupplierId==supplierId,x => x.Images, x => x.Category, x => x.Supplier);
            if (result.Count > 0)
            {
                result = result.OrderByDescending(x => x.CreationDate).ToList();
                return _mapper.Map<List<ViewProductDTO>>(result);
            }
            else throw new Exception("Not have any product");


        }
    }
}
