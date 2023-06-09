using Application.Interfaces;
using Application.ViewModels.CategoryDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
                _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CategoryViewDTO>> GetAll()
        {
            var result =  await _unitOfWork.CategoryRepository.GetAllAsync();
            if (result.Count() > 0) return _mapper.Map<IEnumerable<CategoryViewDTO>>(result);
            else throw new Exception("Not have any category");
        }

        public async Task<CategoryViewDTO> GetById(Guid id)
        {
            var result = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (result is not null) return _mapper.Map<CategoryViewDTO>(result);
            else throw new Exception("Not Found");
        }
    }
}
