using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.DbModel
{
    public class TriggerStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TriggerStatusId { get; set; }

        [Required]
        public string StatusCode { get; set; } // e.g., MG, TA, TEC

        public string Description { get; set; }
    }

}
