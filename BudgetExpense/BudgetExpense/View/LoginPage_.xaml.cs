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
    public partial class LoginPage_ : ContentPage
    {
        public LoginPage_()
        {
            Constants.myAdID = "ca-app-pub-6838059012127071/7646742577";
            var userStore = new SQLiteUserStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            LoginModel = new UserLoginViewModel(userStore, pageService);
            InitializeComponent();
            Constants.curFullName = "";
            Constants.curUserId = 0;
            Constants.curUserName = "";
            Constants.isExpenseLoaded = false;
            Constants.isFundLoaded = false;
        }
        public UserLoginViewModel LoginModel
        {
            get { return BindingContext as UserLoginViewModel; }
            set { BindingContext = value; }
        }
        private async void ViewUsers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsersListPage());
        }
        protected override void OnAppearing()
        {

            LoginModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }
    }
}