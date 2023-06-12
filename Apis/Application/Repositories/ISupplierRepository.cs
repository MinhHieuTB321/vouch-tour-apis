using Application.ViewModels.SupplierDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        public Task<Supplier> AddSupplierAsync(Supplier createDTO);
    }
}
