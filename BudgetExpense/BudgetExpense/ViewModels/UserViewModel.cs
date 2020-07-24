using BudgetExpense.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace BudgetExpense.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public UserViewModel() { }
        public UserViewModel(User user)
        {
            Id = user.Id;
            FullName = user.FullName;
            UserName = user.UserName;
            PassWord = user.PassWord;
        }
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                SetValue(ref _fullName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                SetValue(ref _userName, value);
                OnPropertyChanged(nameof(UserName));
            }
        }
        private string _passWord;
        public string PassWord
        {
            get { return _passWord; }
            set
            {
                SetValue(ref _passWord, value);
                OnPropertyChanged(nameof(PassWord));
            }
        }
    }
}
