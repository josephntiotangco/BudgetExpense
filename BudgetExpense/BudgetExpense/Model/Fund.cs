using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BudgetExpense.Model
{
    public class Fund
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Source { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Fund() { }

        public Fund(int userId, decimal amount,string source,DateTime lastupdate)
        {
            this.UserId = userId;
            this.Amount = amount;
            this.LastUpdateDate = lastupdate;
            this.Source = source;
        }
    }
}
