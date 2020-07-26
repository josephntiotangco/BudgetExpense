using BudgetExpense.Data;
using BudgetExpense.Model;
using BudgetExpense.Persistence;
using BudgetExpense.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetExpense.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRegistrationPage : ContentPage
    {
        public UserRegistrationPage(UserViewModel viewModel)
        {
            Constants.myAdID = "ca-app-pub-6838059012127071/5905505242";
            InitializeComponent();

            var userStore = new SQLiteUserStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();

            Title = (viewModel.Id == 0) ? "New User" : "Edit User";
            BindingContext = new UserDetailViewModel(viewModel ?? new UserViewModel(), userStore, pageService);
        }
    }
}