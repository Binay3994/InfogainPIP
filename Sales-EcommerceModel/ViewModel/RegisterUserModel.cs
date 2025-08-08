using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.ViewModel
{
    public class RegisterUserModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Compare("Password",ErrorMessage ="Password do not match")]
        public string ConfirmPassword {  get; set; }
    }
}
