using Microsoft.EntityFrameworkCore;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceRepository.IRepository;
using Sales_EcommerceService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceService.Service
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        private readonly ITransactionRepository _transactionRepository;
        public CommonService(ICommonRepository commonRepository, ITransactionRepository transactionRepository)
        {
            _commonRepository = commonRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<IEnumerable<Jurisdiction>> GetJurisdiction()
        {
            return await _commonRepository.GetJurisdiction();
        }

        public async Task<IEnumerable<TransactionType>> GetTransactionType()
        {
            return await _commonRepository.GetTransactionType();
        }

        public async Task<IEnumerable<TriggerStatus>> GetTriggerStatus()
        {
            return await _commonRepository.GetTriggerStatus();
        }
        public async Task<bool> AddStickyFilter(StickyFilter model)
        {
            if (model.UserId != null)
            {
                var isAdded = await GetSticklyFilter(model.UserId);
                if (isAdded == null)
                {
                    return await _transactionRepository.AddStickyFilter(model);
                }
                return await _transactionRepository.UpdateSticky(model);
            }
            return false;
        }

        public async Task<StickyFilter> GetSticklyFilter(string userId)
        {
            return await _transactionRepository.GetSticklyFilter(userId);
        }
        public async Task<bool>DeleteSticky(string userId)
        {
            return await _transactionRepository.DeleteSticky(userId);            
        }
    }
}
