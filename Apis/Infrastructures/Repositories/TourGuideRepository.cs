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
        public TourGuideRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) :
            base(context, currentTime, claimsService)
        {
            _context = context;
        }

        public async Task<TourGuide> AddTourGuideAsync(TourGuide tourGuide)
        {
            var result= await _context.TourGuide.AddAsync(tourGuide);
            return result.Entity;
        }
    }
}
