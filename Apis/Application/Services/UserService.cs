using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task<User?> GetUserById(Guid Id) => await _unitOfWork.UserRepository.GetByIdAsync(Id);
    }
}
