﻿using Application.Repositories;

namespace Application
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductImageRepository ProductImageRepository { get; }
        public ISupplierRepository SupplierRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ITourGuideRepository TourGuideRepository { get; }
        public IGroupRepository GroupRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IMenuRepository MenuRepository { get; }
        public IProductMenuRepository ProductMenuRepository { get; }
        public IOrderDetailRepository OrderDetailRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
