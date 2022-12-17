﻿using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;

        public AuthRepository(UserManager<User> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {            
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }

        public string GetId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<object> Login(LoginDTO model)
        {
            JwtSecurityToken token = null;
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            }
            else
            {
                return "Wrong Credential";
            }

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }

        public async Task<object> Register(RegisterDTO user)
        {
            User newUser = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = false,
                PhoneNumber = user.Phone,
                Age = user.Age,
                Avatar = "www.xyz.com",
                Gender = user.Gender,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Publicid = user.Publicid,
            };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded) return "Successfully registered";
            return "Registration failed: " + result.Errors;
        }

        public async Task<object> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(GetId());
            if (user == null) return "Please login to change password";
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
            if (!result.Succeeded) return "Unable to change password: password should contain a Capital, number, character and minimum length of 8";
            return "Password changed succesffully";
        }

        public async Task<object> ForgottenPassword(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return ("The Email Provided is not associated with a user account");
            }

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var emailMsg = new EmailMessage(new string[] { user.Email }, "Reset your password", $"Please Follow the Link to reset your Password: https://localhost:7255/api/Auth/Reset-Update-Password?token={resetPasswordToken}");
            await _emailService.SendEmailAsync(emailMsg);
            return "A password reset Link has been sent to your email address";
        }

        public async Task<object> ResetPassword(UpdatePasswordDTO model)
        {
            
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return "The Email Provided is not associated with a user account.";
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                return "The Provided Reset Token is Invalid or Has expired";
            } 
            return "Password Reset Successfully";
        }

    }
}
