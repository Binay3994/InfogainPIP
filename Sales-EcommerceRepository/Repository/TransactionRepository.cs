using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceRepository.Context;
using Sales_EcommerceRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceRepository.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly Sales_EcommerceDbContext _context;
        public TransactionRepository(Sales_EcommerceDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddStickyFilter(StickyFilter model)
        {
            await _context.StickyFilter.AddAsync(model);
            return await _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<StickyFilter> GetSticklyFilter(string userId)
        {
            return await _context.StickyFilter.FirstOrDefaultAsync(t => t.UserId == userId).ConfigureAwait(true);
        }
        public async Task<bool> DeleteSticky(string userId)
        {
            return await _context.StickyFilter
               .Where(p => p.UserId == userId)
               .ExecuteDeleteAsync()
               .ContinueWith(t => t.Result > 0);
        }

        public async Task<bool> UpdateSticky(StickyFilter model)
        {
            return await _context.StickyFilter
                .Where(p => p.UserId == model.UserId)
                .ExecuteUpdateAsync(upd => upd
                    .SetProperty(p => p.Jurisdiction, model.Jurisdiction)
                    .SetProperty(p => p.ClientId, model.ClientId)
                    .SetProperty(p => p.ClaimAdminClaim, model.ClaimAdminClaim)
                    .SetProperty(p => p.FromDate, model.FromDate)
                    .SetProperty(p => p.ToDate, model.ToDate)
                    .SetProperty(p => p.TransactionType, model.TransactionType)
                    .SetProperty(p => p.TriggerStatus, model.TriggerStatus)
                    .SetProperty(p => p.User, model.User))
                .ContinueWith(t => t.Result > 0);

        }
    }
}
