using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.DbModel
{
    public class StickyFilter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Jurisdiction { get; set; }
        public string? ClientId { get; set; }
        public string? ClaimAdminClaim{ get; set; }
        public DateTime? FromDate{ get; set; }
        public DateTime? ToDate{ get; set; }
        public string? TransactionType{ get; set; }
        public string? TriggerStatus{ get; set; }
        public string? User { get; set; }
    }
}
