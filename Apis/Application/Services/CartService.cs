using Application.Interfaces;
using Application.ViewModels.CartDTO;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using Application.ViewModels.Product.ProductImage;
using Application.GlobalExceptionHandling.Exceptions;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;

        private  readonly IFirebaseConfig config;
        private  readonly IFirebaseClient _client;

        public CartService(IUnitOfWork unitOfWork,IClaimsService claimsService, IMapper mapper)
        {
            _unitOfWork= unitOfWork;
            _claimsService= claimsService;
            _mapper= mapper;
            config= new FirebaseConfig
            {
                AuthSecret = "LBnpcQLTqzjMUbS6shchSCcfPmQvAbPkLBPTIuUj",
                BasePath = "https://vouch-tour-default-rtdb.asia-southeast1.firebasedatabase.app"
            };
            _client= new FireSharp.FirebaseClient(config);
        }

        public async Task<bool> AddToCart(ItemAddDTO addDTO)
        {
            var product=await _unitOfWork.ProductRepository.GetByIdAsync(addDTO.ProductId,x=>x.Category,x=>x.Supplier,x=>x.Images);
            if (product == null) throw new NotFoundException("Not found product");
            var checkPrice= (product.ResellPrice - addDTO.ActualPrice)/product.ResellPrice;
            if (checkPrice < 0 || checkPrice > 0.1) throw new BadRequestException("Price can't be lower 10% of supplier Price");
            var items=_mapper.Map<ItemViewDTO>(product);
            items.ActualPrice = addDTO.ActualPrice;
            items.Images= _mapper.Map<List<ImageViewDTO>>(product.Images);
            var result =await SetToFirebase(items); 
            return result;
        }

        private async Task<bool> SetToFirebase(ItemViewDTO data)
        {
            var root = "Cart-" + _claimsService.GetCurrentUser;
            data.CartId= root;
            PushResponse response = await _client.PushAsync($"{root}/", data);
            data.Id = response.Result.name;
            SetResponse setResponse = await _client.SetAsync($"{root}/" + data.Id, data);
            if (setResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
        
        public async Task<bool> DeleteItem(string cartId,string id)
        {
            FirebaseResponse response = await _client.DeleteAsync($"{cartId}/" + id);
            return true;
        }

        public async Task<List<ItemViewDTO>> GetAllItems()
        {
            var root = "Cart-" + _claimsService.GetCurrentUser;
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
            if (list.Count == 0) throw new NotFoundException("Not found!");
            return list;
        }

        public async Task<bool> UpdateItem(ItemUpdateDTO updateDTO)
        {
            var root = "Cart-" + _claimsService.GetCurrentUser;
            FirebaseResponse response = await _client.GetAsync($"{root}/" + updateDTO.Id);
            ItemViewDTO data = JsonConvert.DeserializeObject<ItemViewDTO>(response.Body);
            if (data == null) throw new NotFoundException("Not found item with" + updateDTO.Id);
            data.ActualPrice = updateDTO.ActualPrice;
            SetResponse setResponse = await _client.SetAsync($"{root}/" + data.Id, data);
            if (setResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<ItemViewDTO> GetItemById(String cartid, string id)
        {
            FirebaseResponse response = await _client.GetAsync($"{cartid}/" + id);
            ItemViewDTO data = JsonConvert.DeserializeObject<ItemViewDTO>(response.Body);
            if (data == null) throw new NotFoundException("Not found item with" + id);
            return data;
        }
    }
}
