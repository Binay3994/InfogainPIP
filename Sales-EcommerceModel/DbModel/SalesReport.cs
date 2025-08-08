using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.DbModel
{
    public class SalesReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesReportId { get; set; }

        public DateTime ReportDate { get; set; }

        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public int ProductsSold { get; set; }
        public int NewCustomers { get; set; }
    }
}
