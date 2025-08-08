using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.DbModel
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionId { get; set; }

        [Required]
        public string ClaimNumber { get; set; }

        public string Jurisdiction { get; set; }

        public string ClientId { get; set; }

        public string? ClaimAdminClaimNumber { get; set; } 

        public string TriggerType { get; set; }

        public string? MGStatus { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TriggerStatus { get; set; }

        public string? AssignedTo { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
