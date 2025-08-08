using Sales_EcommerceModel.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceService.IService
{
    public interface IExecutiveService
    {
        Task<bool> UpdateTransactionStatusByExecutive(Guid transId, string status);
        Task<IEnumerable<Transaction>>GetAllAssignedTransaction(string UserId);
    }
}
