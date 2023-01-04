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
        private readonly ILogger _logger;

        public TransactionsController(ITransactionService transactionService, ILogger logger)
        {
            _transactionService = transactionService;
            _logger = logger;   
         
        }
        [HttpGet, Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllTransactionForAdmin()
        {
            var result = await _transactionService.DisplayAllTransactionToAdmin();
            try
            {
               
               
                if (!result.Succeeded) return BadRequest($"unable to get transactions{result}");
                return Ok(result);
            }
            catch (Exception ex) 
            { 
                
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

        
    }
}
