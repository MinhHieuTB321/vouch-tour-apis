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

        public async Task<TourGuideViewDTO> AddTourGuide(TourGuideCreateDTO dto)
        {
            var createDTO = _mapper.Map<TourGuide>(dto);
            createDTO.AdminId = _claimsService.GetUserRoleId;
            var tourGuide = await _unitOfWork.TourGuideRepository.AddTourGuideAsync(createDTO);
            var createUser = _mapper.Map<User>(dto);
            createUser.UserId = tourGuide.Id;
            createUser.RoleId = 3;
            var checkUser = await CheckUser(dto.Email, tourGuide.Id);
            if (!checkUser)
            {
                await _unitOfWork.UserRepository.AddAsync(createUser);
            }
            await _unitOfWork.SaveChangeAsync();
            var result = _mapper.Map<TourGuideViewDTO>(tourGuide);
            return result;
        }

        private async Task<bool> CheckUser(string email,Guid id)
        {
            var checkUser= await _unitOfWork.UserRepository.FindByField(x=>x.Email == email);
            if(checkUser==null) {
                return false;
            }
            checkUser.UserId = id;
            _unitOfWork.UserRepository.Update(checkUser);
            return true;
        }

        public async Task<IEnumerable<TourGuideViewDTO>> GetAll()
        {
            var result = await _unitOfWork.TourGuideRepository.GetAllAsync();
            if(result.Count() > 0)
            {
                return _mapper.Map<List<TourGuideViewDTO>>(result);
            }
            throw new Exception("Not have any tour guide");
        }

        public async Task<TourGuideViewDTO> GetById(Guid id)
        {
            var result = await _unitOfWork.TourGuideRepository.GetByIdAsync(id);
            if(result is not null)
            {
                return _mapper.Map<TourGuideViewDTO>(result);
            }
            throw new Exception("Not found!");
        }
    }
}
