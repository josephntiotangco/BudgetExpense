using BudgetExpense.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BudgetExpense.ViewModels;
using Xamarin.Forms;
using BudgetExpense.Data;
using BudgetExpense.View;
using System.Linq;

namespace BudgetExpense.ViewModels
{
    public class UserLoginViewModel : BaseViewModel
    {
        private UserViewModel _currentUser;
        private IUserStore _userStore;
        private IPageService _pageService;
        private string _inputUserName;
        public string InputUserName
        {
            get { return _inputUserName; }
            set { SetValue(ref _inputUserName, value); OnPropertyChanged(nameof(InputUserName)); }
        }
        private string _inputPassword;
        public string InputPassword
        {
            get { return _inputPassword; }
            set { SetValue(ref _inputPassword, value); OnPropertyChanged(nameof(InputPassword)); }
        }

        private bool _isDataLoaded;
        public UserViewModel CurrentUser
        {
            get { return _currentUser; }
            set { SetValue(ref _currentUser, value); }
        }
        public ObservableCollection<UserViewModel> Users { get; private set; } = new ObservableCollection<UserViewModel>();
        public ICommand ValidateCommand { get; private set; }
        public ICommand LoadDataCommand { get; private set; }

        public UserLoginViewModel(IUserStore UserStore, IPageService pageService)
        {
            _userStore = UserStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            ValidateCommand = new Command<UserViewModel>(async c => await ValidateUser(c, InputUserName,InputPassword));
        }
        private async Task LoadData()
        {
            if (_isDataLoaded) return;
            _isDataLoaded = true;
            var users = await _userStore.GetUsersAsync();
            foreach (var user in users)
                Users.Add(new UserViewModel(user));
        }
        private async Task ValidateUser(UserViewModel user, string userName, string passWord)
        {
            try
            {
                var curUser = Users.Where(x => x.UserName == userName).ToList();
                if(curUser != null)
                {
                    if(curUser[0].PassWord == passWord)
                    {
                        Constants.curFullName = curUser[0].FullName;
                        Constants.curUserName = curUser[0].UserName;
                        Constants.curUserId = curUser[0].Id;
                        await _pageService.DisplayAlert("Successful", $"Welcome back {Constants.curFullName}", "OK");
                        await _pageService.PushAsync(new BudgetingPage(null, null));
                    }
                    else
                    {
                        await _pageService.DisplayAlert("INCORRECT CREDENTIALS", "Please input correct username and password!", "OK");
                        return;
                    }

                }
                else
                {
                    await _pageService.DisplayAlert("INCORRECT CREDENTIALS","Please input correct username and password!","OK");
                    return;
                }
            }
            catch(Exception error)
            {
                await _pageService.DisplayAlert("Error", "ERROR : not a valid user , " + error.Message, "OK");
                return;
            }


        }
    }
}
