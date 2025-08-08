using Sales_EcommerceModel.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceService.IService
{
    public interface ICommonService
    {
        Task<IEnumerable<TransactionType>> GetTransactionType();
        Task<IEnumerable<TriggerStatus>> GetTriggerStatus();
        Task<IEnumerable<Jurisdiction>> GetJurisdiction();
        Task<bool> AddStickyFilter(StickyFilter model);
        Task<StickyFilter> GetSticklyFilter(string userId);
        Task<bool> DeleteSticky(string userId);
    }
}
