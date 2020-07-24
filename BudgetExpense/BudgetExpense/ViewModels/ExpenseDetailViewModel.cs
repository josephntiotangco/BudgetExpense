using BudgetExpense.Data;
using BudgetExpense.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetExpense.ViewModels
{
    public class ExpenseDetailViewModel
    {
        private readonly IExpenseStore _expenseStore;
        private readonly IPageService _pageService;

        public Expense Expense { get; private set; }
        public ICommand SaveExpenseCommand { get; private set; }
        public ExpenseDetailViewModel(ExpenseViewModel viewModel, IExpenseStore expenseStore, IPageService pageService)
        {
            if (viewModel == null) throw new ArgumentException(nameof(viewModel));
            _pageService = pageService;
            _expenseStore = expenseStore;

            SaveExpenseCommand = new Command(async () => await SaveExpense());

            Expense = new Expense
            {
                Id = viewModel.Id,
                Amount = viewModel.Amount,
                Description = viewModel.Description,
                PostDate = Convert.ToDateTime(Constants.phoneDate),
                UserId = Constants.curUserId
            };
        }
        private async Task SaveExpense()
        {
            decimal expenseOverFund = Constants.myTotalFund > 0 ? Constants.myTotalExpense / Constants.myTotalFund : 0;
            decimal negativeFund = Constants.myTotalFund - Constants.myTotalExpense - Expense.Amount;

            if (string.IsNullOrWhiteSpace(Expense.Description) && Expense.Amount == 0)
            {
                await _pageService.DisplayAlert("EXPENSE", "Please complete expense details.", "OK");
                return;
            }
            if(expenseOverFund * 100 >= 75)
            {
                bool result = await _pageService.DisplayAlert("Expense Breach", "Current Expense has breach 75% of Total Fund, Would you still want to save expense?", "YES", "NO");

                if (!result) return;
            }
            if(negativeFund <= 0)
            {
                bool result = await _pageService.DisplayAlert("Expense Breach", "You are about to post an expense that will generate a negative fund. Would you still want to save expense?", "YES", "NO");

                if (!result) return;
            }
            if (Expense.Id == 0)
            {
                await _expenseStore.AddExpense(Expense);
                MessagingCenter.Send(this, Events.ExpenseAdded, Expense);
                await _pageService.PopAsync();
            }
            else
            {
                await _expenseStore.UpdateExpense(Expense);
                MessagingCenter.Send(this, Events.ExpenseUpdated, Expense);
                await _pageService.PopAsync();
            }
        }
    }
}
