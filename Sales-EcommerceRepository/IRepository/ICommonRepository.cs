using Sales_EcommerceModel.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceRepository.IRepository
{
    public interface ICommonRepository
    {
        Task<IEnumerable<TransactionType>> GetTransactionType();
        Task<IEnumerable<TriggerStatus>> GetTriggerStatus();
        Task<IEnumerable<Jurisdiction>> GetJurisdiction();
    }
}
