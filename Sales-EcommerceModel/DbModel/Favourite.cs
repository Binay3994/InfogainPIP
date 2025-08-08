using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.DbModel
{
    public class Favourite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavouriteId { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
