using Sales_EcommerceModel.DbModel;
using Sales_EcommerceRepository.IRepository;
using Sales_EcommerceService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceService.Service
{
    public class ExecutiveService : IExecutiveService
    {
        private readonly IClaimService _claimService;
        private readonly IClaimRepository _claimRepository;
        public ExecutiveService(IClaimService claimService, IClaimRepository claimRepository)
        {
            _claimService = claimService;
            _claimRepository = claimRepository;
        }
        public async Task<bool> UpdateTransactionStatusByExecutive(Guid transId, string status)
        {
            var transaction = await _claimService.TransactionDetailsById(transId);
            if (transaction == null)
            {
                return false; // Transaction not found
            }
            transaction.TriggerStatus = status;
            transaction.LastModified = DateTime.UtcNow;
            return await _claimService.UpdateClaim(transaction);
        }

        public async Task<IEnumerable<Transaction>> GetAllAssignedTransaction(string UserId)
        {
            var allTransactions = await _claimRepository.AllTransactionList();
            if (allTransactions == null || !allTransactions.Any())
            {
                return Enumerable.Empty<Transaction>();
            }
            return allTransactions.Where(x => x.AssignedTo == UserId).ToList();            
        }
    }
}
