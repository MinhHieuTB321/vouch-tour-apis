using Application.Interfaces;
using Application.ViewModels.TourGuideDTO;
using AutoMapper;
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
        public TourGuideService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
