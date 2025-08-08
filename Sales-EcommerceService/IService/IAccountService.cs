using Sales_EcommerceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceService.IService
{
    public interface IAccountService
    {
        Task<bool> Login(string username, string password);
        Task<bool> RegisterUser(RegisterUserModel registerUserModel);

    }
}
