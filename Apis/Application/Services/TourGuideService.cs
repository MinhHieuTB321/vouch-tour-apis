using Application.Interfaces;
using Application.ViewModels.TourGuideDTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.GlobalExceptionHandling.Exceptions;

namespace Application.Services
{
    public class TourGuideService : ITourGuideService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        public TourGuideService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
        }

        #region Add TourGuide
        public async Task<TourGuideViewDTO> AddTourGuide(TourGuideCreateDTO dto)
        {
            var createDTO = _mapper.Map<TourGuide>(dto);
            createDTO.AdminId = _claimsService.GetCurrentUser;
            var tourGuide = await _unitOfWork.TourGuideRepository.AddTourGuideAsync(createDTO);
            var createUser = new User{UserId=tourGuide.Id,RoleId=3,Email=tourGuide.Email};
            await _unitOfWork.UserRepository.AddAsync(createUser);
            await _unitOfWork.SaveChangeAsync();
            var result = _mapper.Map<TourGuideViewDTO>(tourGuide);
            return result;
        }
        #endregion

        #region Delete TourGuide
        public async Task<bool> DeleteTourGuideAsync(Guid id)
        {
            var tourguide=await _unitOfWork.TourGuideRepository.GetByIdAsync(id);
            var user= await _unitOfWork.UserRepository.FindByField(x=>x.UserId==id);
            if (tourguide == null) throw new NotFoundException("Tour-Guide is not exist in system!");
            _unitOfWork.UserRepository.SoftRemove(user);
            _unitOfWork.TourGuideRepository.SoftRemove(tourguide);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;
        }
        #endregion

        #region Get All Tourguide
        public async Task<IEnumerable<TourGuideViewDTO>> GetAll()
        {
            var tourGuides = await _unitOfWork.TourGuideRepository.GetAllAsync(x=>x.Groups);
            if(tourGuides.Count() > 0)
            {
                var result= _mapper.Map<List<TourGuideViewDTO>>(tourGuides);
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].NumberOfGroup = tourGuides[i].Groups.Count;
                    result[i] = await GetNumberOfSuccessOrder(result[i]);
                }
                return result;
            }
            throw new Exception("Not have any tour guide");
        }
        #endregion

        #region Get Tour-Guide by Id

        /// <summary>
        /// Get tour- guide by id
        /// </summary>
        /// <param Guid="id"></param>
        /// <returns>TourGuideViewDTO</returns>
        /// <exception cref="Exception"></exception>
        public async Task<TourGuideViewDTO> GetById(Guid id)
        {
            var tourGuide = await _unitOfWork.TourGuideRepository.GetByIdAsync(id,x=>x.Groups);
            if(tourGuide is not null)
            {
                var result= _mapper.Map<TourGuideViewDTO>(tourGuide);
                result.NumberOfGroup = tourGuide.Groups.Count(x => x.CreationDate > DateTime.Now.AddYears(-1));
                return await GetNumberOfSuccessOrder(result);
            }
            
            throw new NotFoundException("Not found!");
        }

        private async Task<TourGuideViewDTO> GetNumberOfSuccessOrder(TourGuideViewDTO tourGuide)
        {
            var orders = await _unitOfWork.OrderRepository
                        .FindListByField(x =>
                            x.TourGuideId == tourGuide.Id &&
                            x.Status == OrderEnums.Completed.ToString()&&
                            x.CreationDate>=DateTime.Now.AddMonths(-1)
                            , x => x.OrderDetails);
            tourGuide.NumberOfOrderCompleted = orders.Count;
            tourGuide.Point = orders.Count;
            tourGuide.NumberOfProductSold = orders.Sum(x => x.OrderDetails.Count);
            return tourGuide;
        }
        #endregion

        #region Update TourGuide
        public async Task<bool> UpdateTourGuideAsync(TourGuideUpdateDTO dto)
        {
            var tourguide = await _unitOfWork.TourGuideRepository.GetByIdAsync(_claimsService.GetCurrentUser);
            if(tourguide==null)
            {
                throw new NotFoundException("Tour-Guide is not exist!");
            }
            tourguide = _mapper.Map(dto, tourguide);
            _unitOfWork.TourGuideRepository.Update(tourguide);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;
        }
        #endregion
    }
}
