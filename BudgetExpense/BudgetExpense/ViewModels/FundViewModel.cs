using BudgetExpense.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetExpense.ViewModels
{
    public class FundViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public FundViewModel() { }

        public FundViewModel(Fund fund)
        {
            Id = fund.Id;
            UserId = fund.UserId;
            Source = fund.Source;
            Amount = fund.Amount;
            LastUpdateDate = fund.LastUpdateDate;
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
        private string _source;
        public string Source
        {
            get { return _source; }
            set
            {
                SetValue(ref _source, value);
                OnPropertyChanged(nameof(Amount));
            }
        }
        private DateTime _lastUpdateDate;
        public DateTime LastUpdateDate
        {
            get { return _lastUpdateDate; }
            set
            {
                SetValue(ref _lastUpdateDate, value);
                OnPropertyChanged(nameof(LastUpdateDate));
            }
        }
    }
}
