using BudgetExpense.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BudgetExpense.ViewModels
{
    public class UserDetailViewModel
    {
        private readonly IUserStore _userStore;
        private readonly IPageService _pageService;

        public User User { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public UserDetailViewModel(UserViewModel viewModel, IUserStore userStore, IPageService pageService)
        {
            if (viewModel == null) throw new ArgumentException(nameof(viewModel));

            _pageService = pageService;
            _userStore = userStore;

            SaveCommand = new Command(async () => await Save());

            User = new User
            {
                Id = viewModel.Id,
                FullName = viewModel.FullName,
                UserName = viewModel.UserName,
                PassWord = viewModel.PassWord
            };
        }

        async Task Save()
        {
            if(string.IsNullOrWhiteSpace(User.FullName) && string.IsNullOrWhiteSpace(User.UserName) && string.IsNullOrWhiteSpace(User.PassWord))
            {
                await _pageService.DisplayAlert("ERROR", "Please complete all required fields(full name, username, password)", "OK");
                return;
            }

            if(User.Id == 0)
            {
                await _userStore.AddUser(User);
                MessagingCenter.Send(this, Events.UserAdded, User);
            }
            else
            {
                await _userStore.UpdateUser(User);
                MessagingCenter.Send(this, Events.UserUpdated, User);
            }
            await _pageService.PopAsync();
        }
    }
}
