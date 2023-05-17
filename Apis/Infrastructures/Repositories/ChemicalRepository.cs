using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class ChemicalRepository : GenericRepository<Chemical>, IChemicalRepository
    {
        private readonly AppDbContext _dbContext;

        public ChemicalRepository(AppDbContext dbContext,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(dbContext,
                  timeService,
                  claimsService)
        {
            _dbContext = dbContext;
        }

        public List<Chemical> GetTop3LatestChemical()
        {
            // this is how we create the custom method with repository pattern

            return _dbContext.Chemicals.Take(3).OrderByDescending(x => x.CreationDate).ToList();
        }
    }
}
