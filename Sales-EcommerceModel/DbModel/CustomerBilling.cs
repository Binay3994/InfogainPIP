using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.DbModel
{
    public class CustomerBilling
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerBillingId { get; set; }
        public string UserId { get; set; }
        public string BillingAddress { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
