using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class ProductMenuRepository : GenericRepository<ProductInMenu>, IProductMenuRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IClaimsService _claimsService;
        private readonly ICurrentTime _currentTime;
        public ProductMenuRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _appDbContext = context;
            _currentTime = timeService;
            _claimsService = claimsService;
        }

        public async Task<List<ProductInMenu>> GetProductByMenuId(Guid menuId)
        {
            var result = await _appDbContext.ProductInMenu.Where(x => x.MenuId == menuId && x.IsDeleted==false).ToListAsync();
            return result;
        }
    }
}
