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
                    var report = await GetTourGuideReport(result[i].Id);
                    result[i].ReportInMonth = _mapper.Map(report, result[i].ReportInMonth);
                    result[i].ReportInMonth.NumberOfGroup = tourGuides[i].Groups.Count;
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
                var report = await GetTourGuideReport(tourGuide.Id);
                result.ReportInMonth = _mapper.Map(report, result.ReportInMonth);
                result.ReportInMonth.NumberOfGroup = tourGuide.Groups.Count(x => x.CreationDate > DateTime.Now.AddYears(-1));
                return result;
            }
            
            throw new NotFoundException("Not found!");
        }

        private async Task<TourGuideReport> GetTourGuideReport(Guid tourGuideId)
        {
            var orders = await _unitOfWork.OrderRepository
                        .FindListByField(x =>
                            x.TourGuideId == tourGuideId &&
                            x.CreationDate>=DateTime.Now.AddMonths(-1)
                            , x => x.OrderDetails);
            var result = new TourGuideReport()
            {
                NumberOfOrderCompleted = orders.Count(x=>x.Status==OrderEnums.Completed.ToString()),
                NumberOfOrderCanceled= orders.Count(x => x.Status == OrderEnums.Canceled.ToString()),
                NumberOfOrderWaiting = orders.Count(x => x.Status == OrderEnums.Waiting.ToString()),
                Point=GetPoint(orders.Count(x => x.Status == OrderEnums.Completed.ToString()))
            };
            return result;
        }

        private int GetPoint(int orderSuccess)
        {
            if(orderSuccess > 5)
            {
                orderSuccess += (orderSuccess / 10) * 5;
                return orderSuccess;
            }

            return orderSuccess;
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
