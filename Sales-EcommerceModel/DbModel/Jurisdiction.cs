using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.DbModel
{

    public class Jurisdiction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JurisdictionId { get; set; }

        [Required]
        public string Name { get; set; }
    }

}
