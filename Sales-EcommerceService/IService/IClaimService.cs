using Sales_EcommerceModel.DbModel;
using Sales_EcommerceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceService.IService
{
    public interface IClaimService
    {
        Task<bool> AddClaim(AddClaimModel model);
        Task<IEnumerable<Transaction>> AllTransactionListByClientId(string userId);
        Task<bool> UpdateClaim(Transaction transaction);
        Task<Transaction> TransactionDetailsById(Guid guid);
        Task<bool> AssignClaim(AssignClaim assignClaim);
        Task<IEnumerable<Transaction>> GetAllTransaction();
        Task<IEnumerable<string>> GetClientIdList();
        Task<IEnumerable<Transaction>> GetFilteredTransaction(StickyFilter stickyFilter);
        Task<bool> DeleteClaim(Guid guid);
        Task<bool> CreateMTC(MTC mTC);
        Task<IEnumerable<MTC>> GetMTC(string claimId);
        Task<IEnumerable<Transaction>> GetAllAssignedClaim();
    }
}
