using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace BudgetExpense.Model
{
    public class Expense
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime PostDate { get; set; }
        public int UserId { get; set; }
        public Expense() { }
        public Expense(string description, int userid, decimal amount, DateTime postdate)
        {
            this.Description = description;
            this.UserId = userid;
            this.Amount = amount;
            this.PostDate = postdate;
        }
    }
}
