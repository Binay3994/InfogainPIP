using Sales_EcommerceModel.DbModel;
using Sales_EcommerceModel.ViewModel;
using Sales_EcommerceRepository.IRepository;
using Sales_EcommerceService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sales_EcommerceService.Service
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;
        Random rnd = new Random();
        public ClaimService(IClaimRepository claim)
        {
            _claimRepository = claim;
        }
        public async Task<bool> AddClaim(AddClaimModel model)
        {
            try
            {

                // Here you would typically map the AddClaimModel to a Transaction entity
                if (model == null)
                {
                    return false;
                }
                var transaction = new Transaction
                {
                    ClaimNumber = Convert.ToString(rnd.Next(10000, 100000)),
                    Jurisdiction = model.Jurisdiction,
                    ClientId = model.ClientId,
                    //ClaimAdminClaimNumber = model.ClaimAdminClaimNumber,
                    TriggerType = model.TriggerType,
                    //MGStatus = model.MGStatus,
                    TransactionDate = DateTime.UtcNow,
                    TriggerStatus = model.TriggerStatus,
                    LastModified = DateTime.UtcNow // Automatically set the last modified time
                };
                // Here you would typically call a repository method to save the transaction
                return await _claimRepository.AddClaim(transaction);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Transaction>> AllTransactionListByClientId(string userId)
        {
            try
            {
                var recod = await _claimRepository.AllTransactionList();

                return recod.Where(x => x.ClientId == userId).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<bool> UpdateClaim(Transaction transaction)
        {
            if (transaction == null)
            {
                return false;
            }
            try
            {
                var existingTransaction = await _claimRepository.TransactionDetailsById(transaction.TransactionId);
                if (existingTransaction == null)
                {
                    return false;
                }
                // Update fields
                existingTransaction.ClaimNumber = transaction.ClaimNumber;
                existingTransaction.Jurisdiction = transaction.Jurisdiction;
                existingTransaction.ClientId = transaction.ClientId;
                //existingTransaction.ClaimAdminClaimNumber = transaction.ClaimAdminClaimNumber;
                existingTransaction.TriggerType = transaction.TriggerType;
                existingTransaction.MGStatus = transaction.MGStatus;
                existingTransaction.TransactionDate = transaction.TransactionDate;
                existingTransaction.TriggerStatus = transaction.TriggerStatus;
                existingTransaction.AssignedTo = transaction.AssignedTo;
                existingTransaction.LastModified = DateTime.UtcNow; // Automatically update timestamp
                existingTransaction.IsDeleted = transaction.IsDeleted;
                return await _claimRepository.UpdateClaim(existingTransaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Transaction> TransactionDetailsById(Guid guid)
        {
            try
            {
                return await _claimRepository.TransactionDetailsById(guid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AssignClaim(AssignClaim assignClaim)
        {
            try
            {
                var transaction = await _claimRepository.TransactionDetailsById(assignClaim.TransactionId);
                if (transaction == null)
                {
                    return false; // Transaction not found
                }
                transaction.ClaimAdminClaimNumber = assignClaim.AssignBy +"_"+ assignClaim.TransactionId;
                transaction.AssignedTo = assignClaim.AssignTo;
                transaction.Jurisdiction = assignClaim.Jurisdiction;
                transaction.TriggerStatus = assignClaim.TriggerStatus;
                transaction.MGStatus = assignClaim.MgStatus;
                transaction.TriggerType = assignClaim.TriggerType;                
                transaction.LastModified = DateTime.UtcNow; // Update last modified time
                return await _claimRepository.UpdateClaim(transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTransaction()
        {
            try
            {
                var claims = await _claimRepository.AllTransactionList();
                if (claims == null || !claims.Any())
                {
                    return Enumerable.Empty<Transaction>();
                }
                return claims;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllAssignedClaim()
        {
            try
            {
                var claims = await _claimRepository.GetAllAssignedClaim();
                if (claims == null || !claims.Any())
                {
                    return Enumerable.Empty<Transaction>();
                }
                return claims;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<string>> GetClientIdList()
        {
            var claims = await _claimRepository.AllTransactionList();
            return claims.Select(x => x.ClientId);
        }

        public async Task<IEnumerable<Transaction>> GetFilteredTransaction(StickyFilter filter)
        {
            try
            {
                var query = await _claimRepository.AllTransactionList();
                if (!string.IsNullOrEmpty(filter.ClientId))
                {
                    query = query.Where(t => t.ClientId == filter.ClientId);
                }

                if (!string.IsNullOrEmpty(filter.Jurisdiction))
                {
                    query = query.Where(t => t.Jurisdiction == filter.Jurisdiction);
                }
                if (!string.IsNullOrEmpty(filter.ClaimAdminClaim))
                {
                    query = query.Where(t => t.ClaimAdminClaimNumber == filter.ClaimAdminClaim);
                }

                if (!string.IsNullOrEmpty(filter.TransactionType))
                {
                    query = query.Where(t => t.TriggerType == filter.TransactionType);
                }

                if (filter.FromDate.HasValue)
                {
                    query = query.Where(t => t.TransactionDate >= filter.FromDate.Value);
                }

                if (filter.ToDate.HasValue)
                {
                    query = query.Where(t => t.TransactionDate <= filter.ToDate.Value);
                }
                if (!string.IsNullOrEmpty(filter.User))
                {
                    query = query.Where(t => t.AssignedTo == filter.User);
                }
                if(!string.IsNullOrEmpty(filter.TriggerStatus))
                {
                    query = query.Where(t => t.TriggerStatus == filter.TriggerStatus);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteClaim(Guid guid)
        {
            return await _claimRepository.DeleteClaim(guid);
        }
        public async Task<bool> CreateMTC(MTC mTC)
        {
            return await _claimRepository.CreateMTC(mTC);
        }
        public async Task<IEnumerable<MTC>> GetMTC(string claimId)
        {
            return await _claimRepository.GetMTC(claimId);
        }
    }
}
