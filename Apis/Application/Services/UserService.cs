using Application;
using Application.Interfaces;
using Application.Utils;
using Application.ViewModels.UserViewModels;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentTime _currentTime;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
        }

        public async Task<string> LoginAsync(UserLoginDTO userObject)
        {
            //var user = await _unitOfWork.UserRepository.GetUserByUserNameAndPasswordHash(userObject.UserName, userObject.Password.Hash());
            var user = new User { UserName = "test" };
            return user.GenerateJsonWebToken("1234567812345678", _currentTime.GetCurrentTime());
        }

        public async Task RegisterAsync(UserLoginDTO userObject)
        {
            // check username exited
            var isExited = await _unitOfWork.UserRepository.CheckUserNameExited(userObject.UserName);

            if(isExited)
            {
                throw new Exception("Username exited please try again");
            }

            var newUser = new User
            {
                UserName = userObject.UserName,
                PasswordHash = userObject.Password.Hash()
            };

            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
