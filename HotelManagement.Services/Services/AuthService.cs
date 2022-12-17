﻿using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<object> ChangePassword(ChangePasswordDTO model)
        {
            if (model.ConfirmNewPassword != model.NewPassword) return "Password does not match";
            var response = await _authRepository.ChangePassword(model);
            return response;
        }

        public async Task<object> ResetPasswordAsync(UpdatePasswordDTO model)
        {
            var response = await _authRepository.ResetPassword(model);
            return response;
        }

        public async Task<object> Login(LoginDTO model)
        {
            var response = await _authRepository.Login(model);
            return response;
        }

        public async Task<object> Register(RegisterDTO user)
        {
            var response = await _authRepository.Register(user);
            return response;
        }

        public async Task<object> ForgottenPassword(ResetPasswordDTO model)
        {
            var response = await _authRepository.ForgottenPassword(model);
            return response;
        }
    }
}
