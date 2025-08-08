using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.ViewModel
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string Category { get; set; }

        public int Popularity { get; set; }
    }
}
