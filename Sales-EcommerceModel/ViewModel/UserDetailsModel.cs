using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.ViewModel
{
    public class UserDetailsModel
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public string role { get; set; }
    }
}
