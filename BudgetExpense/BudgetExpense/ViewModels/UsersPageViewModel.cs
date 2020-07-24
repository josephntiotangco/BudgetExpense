using BudgetExpense.Model;
using BudgetExpense.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BudgetExpense.ViewModels
{
    public class UsersPageViewModel : BaseViewModel
    {
        private UserViewModel _selectedUser;
        private IUserStore _userStore;
        private IPageService _pageService;

        private bool _isDataLoaded;

        public ObservableCollection<UserViewModel> Users { get; private set; } = new ObservableCollection<UserViewModel>();

        public UserViewModel SelectedUser
        {
            get { return _selectedUser; }
            set { SetValue(ref _selectedUser, value); }
        }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand SelectUserCommand { get; private set; }
        public ICommand DeleteUserCommand { get; private set; }

        public UsersPageViewModel(IUserStore UserStore, IPageService pageService)
        {
            _userStore = UserStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            AddUserCommand = new Command(async () => await AddUser());
            SelectUserCommand = new Command<UserViewModel>(async c => await SelectUser(c));
            DeleteUserCommand = new Command<UserViewModel>(async c => await DeleteUser(c));

            MessagingCenter.Subscribe<UserDetailViewModel, User>
                (this, Events.UserAdded, OnUserAdded);

            MessagingCenter.Subscribe<UserDetailViewModel, User>
            (this, Events.UserUpdated, OnUserUpdated);
        }
        private void OnUserAdded(UserDetailViewModel source, User User)
        {
            Users.Add(new UserViewModel(User));
        }
        private void OnUserUpdated(UserDetailViewModel source, User User)
        {
            var UserInList = Users.Single(c => c.Id == User.Id);

            UserInList.Id = User.Id;
            UserInList.FullName = User.FullName;
            UserInList.UserName = User.UserName;
            UserInList.PassWord = User.PassWord;
        }
        private async Task LoadData()
        {
            if (_isDataLoaded) return;
            _isDataLoaded = true;
            var users = await _userStore.GetUsersAsync();
            foreach (var user in users)
                Users.Add(new UserViewModel(user));
        }
        private async Task AddUser()
        {
            await _pageService.PushAsync(new UserRegistrationPage(new UserViewModel()));
        }
        private async Task SelectUser(UserViewModel user)
        {
            if (user == null) return;

            SelectedUser = null;

            await _pageService.PushAsync(new UserRegistrationPage(user));
        }
        private async Task DeleteUser(UserViewModel userViewModel)
        {
            if(await _pageService.DisplayAlert("Warning",$"Are you sure you want to delete {userViewModel.FullName}?", "Yes", "No"))
            {
                Users.Remove(userViewModel);

                var user = await _userStore.GetUser(userViewModel.Id);
                await _userStore.DeleteUser(user);
            }
        }
    }
}
