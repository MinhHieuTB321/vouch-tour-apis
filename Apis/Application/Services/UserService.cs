﻿using Application.Commons;
using Application.GlobalExceptionHandling.Exceptions;
using Application.Interfaces;
using Application.Utils;
using Application.ViewModels;
using Application.ViewModels.UserDTOs;
using AutoMapper;
using Domain.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.Extensions.Configuration;
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
        private readonly IClaimsService _claimsService;
        private readonly IConfiguration _config;
        private readonly IFirebaseConfig _fireBaseConfig;
        private readonly IFirebaseClient _client;
        public UserService(IUnitOfWork unitOfWork, 
                            IMapper mapper,
                            IConfiguration config,
                            IClaimsService claimsService)
        {
            _claimsService = claimsService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
            _fireBaseConfig = new FirebaseConfig
            {
                AuthSecret = _config["RealTimeDatabase:AuthSecret"],
                BasePath = _config["RealTimeDatabase:BasePath"],
            };
            _client = new FireSharp.FirebaseClient(_fireBaseConfig);
        }

        public async Task<List<UserViewDTO>> GetAllUsers()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync(x=>x.Role);
            return _mapper.Map<List<UserViewDTO>>(users);
        }

        public async Task<UserViewDTO?> GetUserById(Guid Id) =>  
            _mapper.Map<UserViewDTO>(await _unitOfWork.UserRepository.GetByIdAsync(Id));

        public async Task<AuthToken> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.UserRepository.FindByField(x => x.Email == loginDTO.EMail, x => x.Role);
            if (user == null)
            {
                throw new BadRequestException("Email is not allow to access app!");
            }
            var secretKey = _config["JWTSecretKey"];
            var authToken = new AuthToken
            {
                Id=user.UserId,
                Role=user.Role.RoleName,
                AccessToken = GenerateJsonWebTokenString.GenerateJsonWebToken(user, secretKey!),
                RefreshToken = GenerateRefreshTokenString.GenerateRefreshToken()
            };

            return authToken;
        }



    }
}
