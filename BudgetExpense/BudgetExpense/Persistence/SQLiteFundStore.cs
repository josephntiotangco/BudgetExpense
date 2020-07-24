using BudgetExpense.Model;
using BudgetExpense.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using BudgetExpense.Persistence;

namespace BudgetExpense
{
    public class SQLiteFundStore : IFundStore
    {
        private SQLiteAsyncConnection _connection;
        public SQLiteFundStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<Fund>();
        }
        public async Task<IEnumerable<Fund>> GetFundsByUserId(int userid)
        {
            return await _connection.Table<Fund>().Where(x => x.UserId == userid).ToListAsync();
        }
        public async Task<IEnumerable<Fund>> GetFundsAsync()
        {
            return await _connection.Table<Fund>().ToListAsync();
        }
        public async Task DeleteFund(Fund fund)
        {
            await _connection.DeleteAsync(fund);
        }
        public async Task AddFund(Fund fund)
        {
            await _connection.InsertAsync(fund);
        }
        public async Task UpdateFund(Fund fund)
        {
            await _connection.UpdateAsync(fund);
        }
        public async Task<Fund> GetFund(int id)
        {
            return await _connection.FindAsync<Fund>(id);
        }
    }
}
