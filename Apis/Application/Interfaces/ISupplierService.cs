using Application.ViewModels.Product;
using Application.ViewModels.SupplierDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierViewDTO>> GetAll();
        Task<SupplierViewDTO> GetById(Guid id);
        Task<SupplierViewDTO> Create(SupplierCreateDTO createdItem);
        Task<bool> Update(SupplierUpdateDTO updatedItem);
        Task<bool> Delete(Guid id);
        Task<List<ViewProductDTO>> GetProductBySupplierId(Guid supplierId);
        Task<SupplierReport> GetSupplierReportById(Guid id);
    }
}
