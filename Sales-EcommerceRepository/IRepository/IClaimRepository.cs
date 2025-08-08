using Sales_EcommerceModel.DbModel;
using Sales_EcommerceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Sales_EcommerceRepository.IRepository
{
    public interface IClaimRepository
    {
        Task<bool> AddClaim(Sales_EcommerceModel.DbModel.Transaction transaction);
        Task<bool> UpdateClaim(Sales_EcommerceModel.DbModel.Transaction transaction);
        Task<Sales_EcommerceModel.DbModel.Transaction> TransactionDetailsById(Guid guid);
        Task<IEnumerable<Sales_EcommerceModel.DbModel.Transaction>>AllTransactionList();
        Task<IEnumerable<Sales_EcommerceModel.DbModel.Transaction>> GetAllAssignedClaim();
        Task<bool> DeleteClaim(Guid guid);
        Task<bool>CreateMTC(MTC mTC);
        Task<IEnumerable<MTC>> GetMTC(string claimId);
    }
}
