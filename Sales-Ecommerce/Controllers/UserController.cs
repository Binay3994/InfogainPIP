using Microsoft.AspNetCore.Mvc;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceModel.ViewModel;
using Sales_EcommerceService.IService;
using System.Security.Claims;

namespace Sales_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IClaimService _claim;
        private readonly ICommonService _commonService;
        public UserController(IClaimService claim, ICommonService commonService)
        {
            _claim = claim;
            _commonService = commonService;
        }

        [HttpPost("AddClaim")]
        public async Task<IActionResult> CreateClaim([FromBody] AddClaimModel model)
        {
            if (ModelState.IsValid)
            {
                model.ClientId = User.FindFirst(ClaimTypes.Name)?.Value;
                var result = await _claim.AddClaim(model);
                if (result)
                {
                    return Ok(new { Message = "Claim added successfully." });
                }
                return BadRequest("Failed to add claim.");
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetAllClaims")]
        public async Task<IActionResult> GetAllClaims(String userId)
        {
            var claims = await _claim.AllTransactionListByClientId(userId);
            if (claims != null && claims.Count() > 0)
            {
                return Ok(claims);
            }
            return NotFound("No claims found.");
        }

        [HttpGet("GetClaimById/{id}")]
        public async Task<IActionResult> GetClaimById(Guid id)
        {
            var claim = await _claim.TransactionDetailsById(id);
            if (claim != null)
            {
                return Ok(claim);
            }
            return NotFound("Claim not found.");
        }
        [HttpPut("UpdateClaim")]
        public async Task<IActionResult> UpdateClaim([FromBody] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var result = await _claim.UpdateClaim(transaction);
                if (result)
                {
                    return Ok(new { Message = "Claim updated successfully." });
                }
                return BadRequest("Failed to update claim.");
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetDrodownsList")]
        public async Task<IActionResult> GetDrodownsList()
        {
            try
            {
                var transactionTypes = await _commonService.GetTransactionType();
                var jurisdictions = await _commonService.GetJurisdiction();
                var triggerStatus = await _commonService.GetTriggerStatus();

                if (transactionTypes != null && transactionTypes.Any())
                {
                    var data = new
                    {
                        TransactionTypes = transactionTypes,
                        Jurisdictions = jurisdictions,
                        TriggerStatus = triggerStatus
                    };
                    return Ok(data);
                }
                return NotFound("No transactions found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddSticky")]
        public async Task<IActionResult> AddSticky(StickyFilter model)
        {
            var result = await _commonService.AddStickyFilter(model);
            if (result)
            {
                return Ok("Sticky Add successfully");
            }
            return BadRequest("Something worng");
        }

        [HttpGet("GetSticky")]
        public async Task<IActionResult> GetSticky(string userId)
        {
            var data = await _commonService.GetSticklyFilter(userId);
            if (data == null)
            {
                return BadRequest("No Data");
            }
            return Ok(data);
        }

        [HttpGet("FilterTransactions")]
        public async Task<IActionResult> FilteredTransactionList([FromQuery] StickyFilter model)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _commonService.AddStickyFilter(model);

            var result = await _claim.GetFilteredTransaction(model);
            if (result == null)
            {
                BadRequest("Data not found");
            }
            return Ok(result);
        }

        [HttpDelete("DeleteSticky")]
        public async Task<IActionResult> DeleteSticky(string userId)
        {
            var result = await _commonService.DeleteSticky(userId);
            if (result)
            {
                return Ok("Sticky deleted");
            }
            else
            {
                return BadRequest("Somthing went wrong");
            }
        }

        [HttpDelete("DeleteClaim")]
        public async Task<IActionResult> DeleteClaim(Guid id)
        {
            var result = await _claim.DeleteClaim(id);
            if (result)
            {
                return Ok("Claim Deleted");
            }
            else
            {
                return BadRequest("somthing went wrong");
            }
        }

    }
}
