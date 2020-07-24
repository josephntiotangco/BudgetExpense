using BudgetExpense.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetExpense.ViewModels
{
    public interface IFundStore
    {
        Task<IEnumerable<Fund>> GetFundsByUserId(int userid);
        Task<IEnumerable<Fund>> GetFundsAsync();
        Task<Fund> GetFund(int id);
        Task AddFund(Fund fund);
        Task UpdateFund(Fund fund);
        Task DeleteFund(Fund fund);
    }
}
