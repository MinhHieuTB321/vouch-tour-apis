using Application.ViewModels.TourGuideDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITourGuideService
    {
        public Task<IEnumerable<TourGuideViewDTO>> GetAll();

        public Task<TourGuideViewDTO> GetById(Guid id);
    }
}
