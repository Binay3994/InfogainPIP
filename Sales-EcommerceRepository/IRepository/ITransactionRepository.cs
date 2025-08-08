using Sales_EcommerceModel.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceRepository.IRepository
{
    public interface ITransactionRepository
    {
        Task<bool>AddStickyFilter(StickyFilter model);
        Task<StickyFilter> GetSticklyFilter(string userId);
        Task<bool> DeleteSticky(string userId);
        Task<bool> UpdateSticky(StickyFilter model);
    }
}
