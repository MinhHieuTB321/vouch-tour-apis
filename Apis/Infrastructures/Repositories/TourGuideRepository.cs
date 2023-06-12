using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class TourGuideRepository : GenericRepository<TourGuide>, ITourGuideRepository
    {
        private readonly AppDbContext _context;
        private readonly IClaimsService _claimsService;
        private readonly ICurrentTime _currentTime;
        public TourGuideRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) :
            base(context, currentTime, claimsService)
        {
            _claimsService= claimsService;
            _context = context;
            _currentTime=currentTime;
        }

        public async Task<TourGuide> AddTourGuideAsync(TourGuide tourGuide)
        {
            tourGuide.CreatedBy = _claimsService.GetCurrentUser;
            tourGuide.CreationDate=_currentTime.GetCurrentTime();
            var result= await _context.TourGuide.AddAsync(tourGuide);
            return result.Entity;
        }
    }
}
