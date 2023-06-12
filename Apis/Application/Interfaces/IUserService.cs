using Application.ViewModels;
using Application.ViewModels.UserDTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserViewDTO>> GetAllUsers();
        Task<UserViewDTO?> GetUserById(Guid Id);
        Task<AuthToken> LoginAsync(LoginDTO loginDTO);
    }
}
