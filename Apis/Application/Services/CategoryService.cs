using Application.Commons;
using Application.GlobalExceptionHandling.Exceptions;
using Application.Interfaces;
using Application.ViewModels.CategoryDTO;
using Application.ViewModels.Product;
using AutoMapper;
using Domain.Entities;
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

        public async Task<CategoryViewDTO> Create(CategoryCreateDTO categoryCreateDTO)
        {
            var file = await categoryCreateDTO.File.UploadFileAsync();
            var createDTO = _mapper.Map<Category>(categoryCreateDTO);
            createDTO.FileName = file.FileName;
            createDTO.URL = file.URL;
            var result =await _unitOfWork.CategoryRepository.AddAsync(createDTO);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CategoryViewDTO>(result);
        }

        public async Task<bool> DeleteById(Guid Id)
        {
            var category=await _unitOfWork.CategoryRepository.GetByIdAsync(Id);
            if (category == null) throw new NotFoundException("Can not found category in system!");
             _unitOfWork.CategoryRepository.SoftRemove(category);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;

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

        public async Task<List<ViewProductDTO>> GetProuductByCategoryId(Guid Id)
        {
            var products=await _unitOfWork.ProductRepository.FindListByField(x=>x.CategoryId==Id && x.IsDeleted==false);
            if (products.Count() > 0)
            {
                return _mapper.Map<List<ViewProductDTO>>(products);
            }
            throw new NotFoundException("Not Found");
        }

        public async Task<bool> Update(CategoryUpdateDTO catregoryUpdateDTO)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(catregoryUpdateDTO.Id);
            if (category == null) throw new NotFoundException("Can not found category in system!");
            var removeFile= await category.FileName.RemoveFileAsync();
            if (!removeFile) throw new BadRequestException("Can not delete file!");
            var updateFile = await catregoryUpdateDTO.File.UploadFileAsync();
            category=_mapper.Map(catregoryUpdateDTO, category);
            category.FileName = updateFile.FileName;
            category.URL = updateFile.URL;
            _unitOfWork.CategoryRepository.Update(category);
            var result=await _unitOfWork.SaveChangeAsync();
            return (result > 0);
        }
    }
}
