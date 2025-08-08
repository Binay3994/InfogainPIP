using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceModel.ViewModel
{
    public class AssignClaim
    {
        public string Jurisdiction {  get; set; }   
        public string TriggerType {  get; set; }   
        public string TriggerStatus {  get; set; }   
        public string AssignTo {  get; set; }
        [Required]
        public Guid TransactionId {  get; set; }
        public string AssignBy { get; set; }
        public string? MgStatus { get; set; }

    }
}