using Application;
using Application.Repositories;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        public UnitOfWork(AppDbContext dbContext, IUserRepository userRepository, IProductRepository productRepository, IProductImageRepository productImageRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
        }

        public IUserRepository UserRepository => _userRepository;
        public IProductRepository ProductRepository => _productRepository;

        public IProductImageRepository ProductImageRepository => _productImageRepository; 

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
