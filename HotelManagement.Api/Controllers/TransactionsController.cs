using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
           
         
        }
        [HttpGet, Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllTransactionForAdmin()
        {
          var result = await _transactionService.DisplayAllTransactionToAdmin();
           
          if (!result.Succeeded) return BadRequest($"unable to get transactions{result}");
                return Ok(result);
           
            
        }

        
    }
}
