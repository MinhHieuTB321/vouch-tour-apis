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
        private readonly ITourGuideRepository _tourGuideRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IProductMenuRepository _productMenuRepository;
        public UnitOfWork(AppDbContext dbContext, IUserRepository userRepository, 
            IProductRepository productRepository, 
            IProductImageRepository productImageRepository, ICategoryRepository categoryRepository
            , ISupplierRepository supplierRepository
            , ITourGuideRepository tourGuideRepository,
            IGroupRepository groupRepository,
            IPaymentRepository paymentRepository,
            IMenuRepository menuRepository,
            IProductMenuRepository productMenuRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _tourGuideRepository = tourGuideRepository;
            _groupRepository = groupRepository;
            _paymentRepository = paymentRepository;
            _menuRepository = menuRepository;
            _productMenuRepository = productMenuRepository;
        }

        public IUserRepository UserRepository => _userRepository;
        public IProductRepository ProductRepository => _productRepository;

        public IProductImageRepository ProductImageRepository => _productImageRepository; 
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public ISupplierRepository SupplierRepository => _supplierRepository;
        public ITourGuideRepository TourGuideRepository => _tourGuideRepository;    
        public IGroupRepository GroupRepository => _groupRepository;
        public IPaymentRepository PaymentRepository => _paymentRepository;

        public IMenuRepository MenuRepository => _menuRepository;

        public IProductMenuRepository ProductMenuRepository => _productMenuRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
