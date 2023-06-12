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
        private readonly IClaimsService _claimsService;
        private readonly ICurrentTime _currentTime;
        public SupplierRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) 
            : base(context, currentTime, claimsService) 
        {
            _context = context;
            _claimsService = claimsService;
            _currentTime= currentTime;
        }

        public async Task<Supplier> AddSupplierAsync(Supplier createDTO)
        {
            createDTO.CreatedBy = _claimsService.GetCurrentUser;
            createDTO.CreationDate=_currentTime.GetCurrentTime();
            var result = await _context.Supplier.AddAsync(createDTO);
            return result.Entity;
        }
    }
}
