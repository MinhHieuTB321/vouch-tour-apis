using Application.Interfaces;
using Application.Repositories;
using Application.ViewModels.SupplierDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        private readonly AppDbContext _context;
        public SupplierRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) 
            : base(context, currentTime, claimsService) 
        {
            _context = context;
        }

        public async Task<Supplier> AddSupplierAsync(Supplier createDTO)
        {
            var result = await _context.Supplier.AddAsync(createDTO);
            return result.Entity;
        }
    }
}
