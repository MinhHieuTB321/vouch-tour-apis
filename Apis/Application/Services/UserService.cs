using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserViewDTO>> GetAllUsers()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return _mapper.Map<List<UserViewDTO>>(users);
        }

        public async Task<UserViewDTO?> GetUserById(Guid Id) =>  
            _mapper.Map<UserViewDTO>(await _unitOfWork.UserRepository.GetByIdAsync(Id));
    }
}
