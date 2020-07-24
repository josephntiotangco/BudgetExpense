using BudgetExpense.Model;
using BudgetExpense.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using BudgetExpense.Persistence;

namespace BudgetExpense
{
    public class SQLiteExpenseStore : IExpenseStore
    {
        private SQLiteAsyncConnection _connection;
        public SQLiteExpenseStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<Expense>();
        }
        public async Task<IEnumerable<Expense>> GetExpensesByUserId(int userid)
        {
            return await _connection.Table<Expense>().Where(x => x.UserId == userid).ToListAsync();
        }
        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return await _connection.Table<Expense>().ToListAsync();
        }
        public async Task DeleteExpense(Expense expense)
        {
            await _connection.DeleteAsync(expense);
        }
        public async Task AddExpense(Expense expense)
        {
            await _connection.InsertAsync(expense);
        }
        public async Task UpdateExpense(Expense expense)
        {
            await _connection.UpdateAsync(expense);
        }
        public async Task<Expense> GetExpense(int id)
        {
            return await _connection.FindAsync<Expense>(id);
        }

    }
}
