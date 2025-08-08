using Microsoft.EntityFrameworkCore;
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
    public class CommonRepository : ICommonRepository
    {
        private readonly Sales_EcommerceDbContext _context;
        public CommonRepository(Sales_EcommerceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Jurisdiction>> GetJurisdiction()
        {
            return await _context.Jurisdiction
                 .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TransactionType>> GetTransactionType()
        {
            return await _context.TransactionType.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TriggerStatus>> GetTriggerStatus()
        {
            return await _context.TriggerStatus
                .AsNoTracking()
               .ToListAsync();
        }
    }
}
