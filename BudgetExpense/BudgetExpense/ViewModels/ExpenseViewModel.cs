using BudgetExpense.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace BudgetExpense.ViewModels
{
    public class ExpenseViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public ExpenseViewModel() { }
        public ExpenseViewModel(Expense expense)
        {
            Id = expense.Id;
            Description = expense.Description;
            UserId = expense.UserId;
            Amount = expense.Amount;
            PostDate = expense.PostDate;
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                SetValue(ref _description, value);
                OnPropertyChanged(nameof(Description));
            }
        }
        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set
            {
                SetValue(ref _userId, value);
                OnPropertyChanged(nameof(UserId));
            }
        }
        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                SetValue(ref _amount, value);
                OnPropertyChanged(nameof(Amount));
            }
        }
        private DateTime _postDate;
        public DateTime PostDate
        {
            get { return _postDate; }
            set
            {
                SetValue(ref _postDate, value);
                OnPropertyChanged(nameof(PostDate));
            }
        }
    }
}
