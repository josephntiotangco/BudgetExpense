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
    public partial class BudgetingPage : ContentPage
    {
        public BudgetingPage(FundViewModel fundView, ExpenseViewModel expenseView)
        {
            var expenseStore = new SQLiteExpenseStore(DependencyService.Get<ISQLiteDb>());
            var fundStore = new SQLiteFundStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new BudgetViewModel(pageService,fundView ?? new FundViewModel(), fundStore, expenseView ?? new ExpenseViewModel(), expenseStore);
            InitializeComponent();
        }
        public BudgetViewModel ViewModel
        {
            get { return BindingContext as BudgetViewModel; }
            set { BindingContext = value; }
        }
        void OnExpenseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectExpenseCommand.Execute(e.SelectedItem);
        }
        protected override void OnAppearing()
        {
            ViewModel.LoadFundDataCommand.Execute(null);
            ViewModel.LoadExpenseDataCommand.Execute(null);
            base.OnAppearing();
        }
        
    }
}