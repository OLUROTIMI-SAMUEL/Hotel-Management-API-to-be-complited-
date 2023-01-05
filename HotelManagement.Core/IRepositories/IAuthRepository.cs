using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IAuthRepository
    {
        Task<object> Login(LoginDTO model);
        Task<object> Register(RegisterDTO user);
    }
}
