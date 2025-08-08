using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceModel.ViewModel;
using Sales_EcommerceService.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sales_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountService _accountService;
        private readonly IConfiguration config;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAccountService accountService, IConfiguration configuration)
        {
            _userManager = userManager;
            _accountService = accountService;
            config = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                var regUser = await _accountService.RegisterUser(model);
                if (regUser)
                {
                    return Ok(new {Message= "Account Created", data=model });
                }
                else
                {
                    return BadRequest("Somthing is wrong");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LogInModel userDTO)
        {
            try
            {
                if (ModelState.IsValid)

                {

                    ApplicationUser? UserFromDB = await _userManager.FindByNameAsync(userDTO.UserName);

                    if (UserFromDB != null)

                    {

                        bool found = await _userManager.CheckPasswordAsync(UserFromDB, userDTO.Password);

                        if (found)

                        {
                            //Create Token
                            List<Claim> myclaims = new List<Claim>();

                            myclaims.Add(new Claim(ClaimTypes.Name, UserFromDB.UserName));

                            myclaims.Add(new Claim(ClaimTypes.NameIdentifier, UserFromDB.Id));

                            myclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                            var roles = await _userManager.GetRolesAsync(UserFromDB);

                            foreach (var role in roles)

                            {

                                myclaims.Add(new Claim(ClaimTypes.Role, role));

                            }

                            var SignKey = new SymmetricSecurityKey(

                               Encoding.UTF8.GetBytes(config["JwtSettings:Key"]));

                            SigningCredentials signingCredentials =

                                new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);

                            JwtSecurityToken mytoken = new JwtSecurityToken(

                               issuer: config["JwtSettings:Issuer"],//provider create token

                               audience: config["JwtSettings:Audience"],//cousumer url

                            expires: DateTime.Now.AddHours(1),

                               claims: myclaims,

                               signingCredentials: signingCredentials);

                            return Ok(new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                                expired = mytoken.ValidTo,
                                claim=myclaims,
                                message="Login successful",
                            });

                        }

                    }

                    return BadRequest("Invalid Request");

                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                return Ok(new
                {
                    user.UserName,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    Role
                });
            }
            return NotFound("User not found");
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            if (users != null && users.Any())
            {
                var userList = users.Select(u => new
                {
                    u.UserName,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.PhoneNumber
                }).ToList();
                return Ok(userList);
            }
            return NotFound("No users found");
        }

        [HttpPost("UpdatePassword")]
        public async Task<IActionResult>UpdatePassword(string userName, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if (result.Succeeded)
                {
                    return Ok("Password updated successfully");
                }
                else
                {
                    return BadRequest("Failed to update password");
                }
            }
            return NotFound("User not found");
        }


        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return BadRequest("Role already exists.");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return Ok("Role created successfully.");
            }

            return StatusCode(500, "Internal server error.");
        }

        [HttpPut]
        [Route("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UserDetailsModel user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.userName))
            {
                return BadRequest("Invalid user data.");
            }
            var existingUser = await _userManager.FindByNameAsync(user.userName);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }
            existingUser.FirstName = user.firstName;
            existingUser.LastName = user.lastName;
            existingUser.Email = user.email;
            existingUser.PhoneNumber = user.phoneNumber;
            var result = await _userManager.UpdateAsync(existingUser);
            if (result.Succeeded)
            {
                var currentRoles = await _userManager.GetRolesAsync(existingUser);
                var removeResult = await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
                if (!removeResult.Succeeded)
                    return BadRequest("Failed to remove existing roles");

                // Add new role
                var addResult = await _userManager.AddToRoleAsync(existingUser, user.role);
                if (!addResult.Succeeded)
                    return BadRequest("Failed to add new role");
                return Ok("User details updated successfully.");
            }
            return StatusCode(500, "Internal server error.");
        }

        [HttpGet("RoleList")]
        public async Task<IActionResult>RoleListAsync()
        {
            var data= _roleManager.Roles.ToList();
            return Ok(data);
        }

        [HttpGet("GetAllEmployeesWithRoles")]
        public async Task<IActionResult> GetAllUsersWithRoles(string role)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(role);

            var result = usersInRole.Select(user => new
            {
                user.UserName
            });

            return Ok(result);
        }
    }
}
