using Microsoft.AspNetCore.Mvc;
using Sales_EcommerceService.IService;

namespace Sales_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutiveController : Controller
    {
        //private readonly ILogger<ExecutiveController> _logger;
        private readonly IExecutiveService _executiveService;
        private readonly IClaimService _claimService;
        public ExecutiveController(IExecutiveService executiveService, IClaimService claimService)
        {
            // _logger = logger;
            _executiveService = executiveService;
            _claimService = claimService;
        }

        [HttpPatch("ChangeTransactionStatus")]
        public async Task<IActionResult> UpdateTransactionStatusByExecutive(Guid transId, string status)
        {
            if (transId == Guid.Empty || string.IsNullOrEmpty(status))
            {
                return BadRequest("Invalid transaction ID or status.");
            }
            try
            {
                bool result = await _executiveService.UpdateTransactionStatusByExecutive(transId, status);
                if (result)
                {
                    return Ok("Transaction status updated successfully.");
                }
                else
                {
                    return NotFound("Transaction not found or update failed.");
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error updating transaction status.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("GetClaimsByAssignedId")]
        public async Task<IActionResult> GetAllAssignedTransaction(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return BadRequest("Invalid User ID.");
            }
            try
            {
                var transactions = await _executiveService.GetAllAssignedTransaction(UserId);
                if (transactions == null || !transactions.Any())
                {
                    return NotFound("No transactions found for the specified user.");
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error retrieving assigned transactions.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("GetClaimByTransactionId")]
        public async Task<IActionResult> GetAllAssignedTransaction(Guid guid)
        {
            try
            {
                var transactions = await _claimService.TransactionDetailsById(guid);
                if (transactions == null)
                {
                    return NotFound("No transactions found for the specified user.");
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error retrieving assigned transactions.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
