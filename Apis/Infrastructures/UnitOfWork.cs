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
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        public UnitOfWork(AppDbContext dbContext, IUserRepository userRepository, 
            IProductRepository productRepository, 
            IProductImageRepository productImageRepository, ICategoryRepository categoryRepository
            , ISupplierRepository supplierRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
        }

        public IUserRepository UserRepository => _userRepository;
        public IProductRepository ProductRepository => _productRepository;

        public IProductImageRepository ProductImageRepository => _productImageRepository; 
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public ISupplierRepository SupplierRepository => _supplierRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
