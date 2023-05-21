using Application;
using Application.Repositories;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;


        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
