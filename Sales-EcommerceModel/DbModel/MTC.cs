using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.DbModel
{
    public class MTC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClaimId { get; set; }
        public string UserId { get; set; }
        public string? Hospital { get; set; }
        public string? Treatment { get; set; }
        public float? Cost { get; set; }
        public DateTime Date { get; set; }
    }
}
