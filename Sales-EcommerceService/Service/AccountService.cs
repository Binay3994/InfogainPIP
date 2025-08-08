using Microsoft.AspNetCore.Identity;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceModel.ViewModel;
using Sales_EcommerceService.IService;
using System.Security.Claims;

namespace Sales_EcommerceService.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountService(UserManager<ApplicationUser> userManager) {
        _userManager = userManager;
        }
        public async Task<bool> Login(string username, string password)
        {
            ApplicationUser? user= await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                bool found= await _userManager.CheckPasswordAsync(user, password);
                if (found)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RegisterUser(RegisterUserModel registerUserModel)
        {
            try
            {

                Random rnd = new Random();
                int uniqueNumber = rnd.Next(10000, 100000);

                ApplicationUser user = new ApplicationUser()
                {
                    FirstName=registerUserModel.FirstName,
                    LastName=registerUserModel.LastName,
                    UserName = registerUserModel.FirstName+registerUserModel.LastName+ uniqueNumber,
                    Email = registerUserModel.Email,
                    PasswordHash = registerUserModel.Password
                };
                IdentityResult result = await _userManager.CreateAsync(user,registerUserModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
