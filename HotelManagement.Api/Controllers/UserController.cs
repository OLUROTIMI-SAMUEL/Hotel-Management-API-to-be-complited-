using AutoMapper;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Services.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
            private readonly IMapper _mapper;
            private readonly HotelDbContext _dbContext;
            private readonly IUpdate _service;
            public UserController(IUpdate service, IMapper mapper, HotelDbContext dbContext)
            {
                _service = service;
                _mapper = mapper;
                _dbContext = dbContext;
            }
            [HttpPut]
            [Route("Update_wallet")]
            public async Task<IActionResult> UpdateUsers([FromBody] UpdateUse model)
            {


                if (!ModelState.IsValid) return BadRequest(model);
                var user = _mapper.Map<User>(model);
                await _service.Update(user);
                return Ok(user);
            }
        
    }
}
