using BudgetExpense.Data;
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
    public partial class UsersListPage : ContentPage
    {
        public UsersListPage()
        {
            Constants.myAdID = "ca-app-pub-6838059012127071/5923910449";
            var userStore = new SQLiteUserStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();

            ViewModel = new UsersPageViewModel(userStore, pageService);
            InitializeComponent();
        }
        public UsersPageViewModel ViewModel
        {
            get { return BindingContext as UsersPageViewModel; }
            set { BindingContext = value; }
        }
        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }
        void OnUserSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectUserCommand.Execute(e.SelectedItem);
        }
    }
}