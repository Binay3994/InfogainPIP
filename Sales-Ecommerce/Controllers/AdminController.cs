using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceModel.ViewModel;
using Sales_EcommerceService.IService;
using System.Security.Claims;

namespace Sales_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IClaimService _claimService;
        public AdminController(IClaimService claimService)
        {
            _claimService = claimService;
        }
        [HttpPatch("AssginClaim")]
        public async Task<IActionResult> AssignClaim([FromBody]AssignClaim assignClaim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("AssignTo cannot be null.");
            }
            try
            {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                bool result = await _claimService.AssignClaim(assignClaim);
                if (result)
                {
                    return Ok("Claim assigned successfully.");
                }
                else
                {
                    return NotFound("Transaction not found or assignment failed.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("GetAllClaims")]
        public async Task<IActionResult> GetAllClaims()
        {
            try
            {
                var claims = await _claimService.GetAllTransaction();
                if (claims == null || !claims.Any())
                {
                    return NotFound("No claims found.");
                }
                return Ok(claims);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("GetClientIdList")]
        public async Task<IActionResult> GetClientIdList()
        {
            var data = await _claimService.GetClientIdList();
            if (data == null || !data.Any())
            {
                return NotFound("No claims found.");
            }
            return Ok(data);
        }

        [HttpPost("CreateMTC")]
        public async Task<IActionResult> CreateMTC([FromBody]MTC mTC)
        {
            var result = await _claimService.CreateMTC(mTC);
            if (result)
            {
                return Ok("MTC Created");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("GetMTC")]
        public async Task<IActionResult>GetMTC(string claimId)
        {
            try
            {
                var result = await _claimService.GetMTC(claimId);
                return Ok(result);


            }
            catch (Exception)
            {
                return BadRequest("something went wrong");
            }
        }

        [HttpGet("GetAssignedTransaction")]
        public async Task<IActionResult> GetAssignedTransaction()
        {
            try
            {
                var claims = await _claimService.GetAllAssignedClaim();
                if (claims == null || !claims.Any())
                {
                    return NotFound("No claims found.");
                }
                return Ok(claims);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
