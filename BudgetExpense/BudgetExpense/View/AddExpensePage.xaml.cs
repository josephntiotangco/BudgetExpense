using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetExpense.Data;
using BudgetExpense.Persistence;
using BudgetExpense.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetExpense.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpensePage : ContentPage
    {
        public AddExpensePage(ExpenseViewModel expenseView)
        {
            Constants.myAdID = "ca-app-pub-6838059012127071/2589200474";
            InitializeComponent();

            var expenseStore = new SQLiteExpenseStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();

            Title = (expenseView.Id == 0) ? "New Expense" : "Edit Expense";
            BindingContext = new ExpenseDetailViewModel(expenseView ?? new ExpenseViewModel(), expenseStore, pageService);

        }
    }
}