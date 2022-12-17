﻿using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IAuthService
    {
        public Task<object> Login(LoginDTO model);
        public Task<object> Register(RegisterDTO user);
        public Task<object> ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task<object> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);
    }
}
