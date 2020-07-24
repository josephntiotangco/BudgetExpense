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
    public partial class FundsListPage : ContentPage
    {
        public FundsListPage(FundViewModel fundView)
        {
            var fundStore = new SQLiteFundStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            
            ViewModel = new BudgetViewModel(pageService,fundView,fundStore);

            InitializeComponent();
        }
        public BudgetViewModel ViewModel
        {
            get { return BindingContext as BudgetViewModel; }
            set { BindingContext = value; }
        }
        protected override void OnAppearing()
        {
            Constants.isFundLoaded = false;
            ViewModel.LoadFundDataCommand.Execute(null);
            base.OnAppearing();
        }
        void OnFundSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectFundCommand.Execute(e.SelectedItem);
        }
    }
}