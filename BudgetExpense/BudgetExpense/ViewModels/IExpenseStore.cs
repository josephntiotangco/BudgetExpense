using BudgetExpense.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetExpense.ViewModels
{
    public interface IExpenseStore
    {
        Task<IEnumerable<Expense>> GetExpensesByUserId(int userid);
        Task<IEnumerable<Expense>> GetExpensesAsync();
        Task<Expense> GetExpense(int id);
        Task AddExpense(Expense expense);
        Task UpdateExpense(Expense expense);
        Task DeleteExpense(Expense expense);
    }
}
