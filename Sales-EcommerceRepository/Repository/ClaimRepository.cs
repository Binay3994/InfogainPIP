using Microsoft.EntityFrameworkCore;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceRepository.Context;
using Sales_EcommerceRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Sales_EcommerceRepository.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly Sales_EcommerceDbContext _EcommerceDbContext;
        public ClaimRepository(Sales_EcommerceDbContext context)
        {
            _EcommerceDbContext = context;
        }
        public async Task<bool> AddClaim(Sales_EcommerceModel.DbModel.Transaction transaction)
        {
            await _EcommerceDbContext.Transaction.AddAsync(transaction);
            return await _EcommerceDbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<IEnumerable<Sales_EcommerceModel.DbModel.Transaction>> AllTransactionList()
        {
            return await _EcommerceDbContext.Transaction.Where(x => x.IsDeleted != true).ToListAsync();
        }
        public async Task<IEnumerable<Sales_EcommerceModel.DbModel.Transaction>> GetAllAssignedClaim()
        {
            return await _EcommerceDbContext.Transaction.Where(x => x.IsDeleted != true && x.AssignedTo != null).ToListAsync();
        }

        public async Task<Sales_EcommerceModel.DbModel.Transaction> TransactionDetailsById(Guid guid)
        {
            return await _EcommerceDbContext.Transaction.FirstOrDefaultAsync(x => x.TransactionId == guid);
        }

        public async Task<bool> UpdateClaim(Sales_EcommerceModel.DbModel.Transaction transaction)
        {
            var existingTransaction = await _EcommerceDbContext.Transaction
                .FirstOrDefaultAsync(t => t.TransactionId == transaction.TransactionId);

            if (existingTransaction == null)
                return false;

            // Update fields
            existingTransaction.ClaimNumber = transaction.ClaimNumber;
            existingTransaction.Jurisdiction = transaction.Jurisdiction;
            existingTransaction.ClientId = transaction.ClientId;
            existingTransaction.ClaimAdminClaimNumber = transaction.ClaimAdminClaimNumber;
            existingTransaction.TriggerType = transaction.TriggerType;
            existingTransaction.MGStatus = transaction.MGStatus;
            existingTransaction.TransactionDate = transaction.TransactionDate;
            existingTransaction.TriggerStatus = transaction.TriggerStatus;
            existingTransaction.AssignedTo = transaction.AssignedTo;
            existingTransaction.LastModified = DateTime.UtcNow; // Automatically update timestamp
            existingTransaction.IsDeleted = transaction.IsDeleted;

            _EcommerceDbContext.Transaction.Update(existingTransaction);
            await _EcommerceDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteClaim(Guid guid)
        {
            var existingTransaction = await _EcommerceDbContext.Transaction
               .FirstOrDefaultAsync(t => t.TransactionId == guid);
            if (existingTransaction == null) return false;
            existingTransaction.IsDeleted = true;
            _EcommerceDbContext.Transaction.Update(existingTransaction);
            await _EcommerceDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CreateMTC(MTC mTC)
        {
            await _EcommerceDbContext.MTC.AddAsync(mTC);
            return await _EcommerceDbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<IEnumerable<MTC>> GetMTC(string claimId)
        {
            return await _EcommerceDbContext.MTC.Where(x => x.ClaimId == claimId).ToListAsync();
        }

    }
}
