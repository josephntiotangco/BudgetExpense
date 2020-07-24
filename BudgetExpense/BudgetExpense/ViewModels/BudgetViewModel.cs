using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using BudgetExpense.Model;
using BudgetExpense.Data;
using BudgetExpense.View;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace BudgetExpense.ViewModels
{
    public class BudgetViewModel : BaseViewModel
    {
        private IFundStore _fundStore;
        private IExpenseStore _expenseStore;
        private IPageService _pageService;
        public Expense Expense { get; private set; }
        public Fund Fund { get; private set; }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); OnPropertyChanged(nameof(IsRefreshing)); }
        }
        private decimal _myFundValue;
        public decimal MyFundValue
        {
            get { return _myFundValue; }
            set { SetValue(ref _myFundValue, value); OnPropertyChanged(nameof(MyFundValue)); }
        }
        private decimal _myExpenseValue;
        public decimal MyExpenseValue
        {
            get { return _myExpenseValue; }
            set { SetValue(ref _myExpenseValue, value); OnPropertyChanged(nameof(MyExpenseValue)); }
        }
        private decimal _myBalanceValue;
        public decimal MyBalanceValue
        {
            get { return _myBalanceValue; }
            set { SetValue(ref _myBalanceValue, value); OnPropertyChanged(nameof(MyBalanceValue)); }
        }
        private ExpenseViewModel _selectedExpense;
        public ExpenseViewModel SelectedExpense
        {
            get { return _selectedExpense; }
            set { SetValue(ref _selectedExpense, value);}
        }
        private FundViewModel _selectedFund;
        public FundViewModel SelectedFund
        {
            get { return _selectedFund; }
            set { SetValue(ref _selectedFund, value); }
        }
        public ObservableCollection<ExpenseViewModel> Expenses { get; private set; } = new ObservableCollection<ExpenseViewModel>();
        public ObservableCollection<FundViewModel> Funds { get; private set; } = new ObservableCollection<FundViewModel>();

        public ICommand LoadExpenseDataCommand { get; private set; }
        public ICommand LoadFundDataCommand { get; private set; }
        public ICommand AddFundCommand { get; private set; }
        public ICommand AddExpenseCommand { get; private set; }
        public ICommand ViewFundCommand { get; private set; }
        public ICommand SelectExpenseCommand { get; private set; }
        public ICommand SelectFundCommand { get; private set; }
        public ICommand RefreshDataCommand { get; private set; }
        public ICommand DeleteExpenseCommand { get; private set; }
        public ICommand DeleteFundCommand { get; private set; }
        public BudgetViewModel(IPageService pageService,FundViewModel fundViewModel=null, IFundStore fundStore = null, ExpenseViewModel expenseViewModel= null,IExpenseStore expenseStore = null)
        {
            //if (fundViewModel == null) throw new ArgumentException(nameof(fundViewModel));
            //if (expenseViewModel == null) throw new ArgumentException(nameof(expenseViewModel));

            _fundStore = fundStore;
            _expenseStore = expenseStore;
            _pageService = pageService;

            LoadExpenseDataCommand = new Command(async () => await LoadExpenseData());
            LoadFundDataCommand = new Command(async () => await LoadFundData());
            AddFundCommand = new Command(async () => await AddFund(fundViewModel));
            SelectExpenseCommand = new Command<ExpenseViewModel>(async c => await SelectExpense(c));
            SelectFundCommand = new Command<FundViewModel>(async c => await SelectFund(c));
            AddExpenseCommand = new Command(async () => await AddExpense(expenseViewModel));
            RefreshDataCommand = new Command(async () => await RefreshData());
            ViewFundCommand = new Command(async () => await ViewFunds(fundViewModel));
            DeleteFundCommand = new Command<FundViewModel>(async c => await DeleteFund(c));
            DeleteExpenseCommand = new Command<ExpenseViewModel>(async c => await DeleteExpense(c));

            MessagingCenter.Subscribe<ExpenseDetailViewModel, Expense>(this, Events.ExpenseAdded, OnExpenseAdded);
            MessagingCenter.Subscribe<ExpenseDetailViewModel, Expense>(this, Events.ExpenseUpdated, OnExpenseUpdated);
            MessagingCenter.Subscribe<FundDetailViewModel, Fund>(this, Events.FundAdded, OnFundAdded);
            MessagingCenter.Subscribe<FundDetailViewModel, Fund>(this, Events.FundUpdated, OnFundUpdated);
        }
        private void OnExpenseAdded(ExpenseDetailViewModel source, Expense expense)
        {
            Expenses.Add(new ExpenseViewModel(expense));
        }
        private void OnExpenseUpdated(ExpenseDetailViewModel source, Expense expense)
        {
            var ExpenseInList = Expenses.Single(c => c.Id == expense.Id);

            ExpenseInList.Description = expense.Description;
            ExpenseInList.Amount = expense.Amount;
            ExpenseInList.PostDate = expense.PostDate;
            ExpenseInList.Id = expense.Id;
            ExpenseInList.UserId = expense.UserId;
        }
        private void OnFundUpdated(FundDetailViewModel source, Fund fund)
        {
            var FundInList = Funds.Single(c => c.Id == fund.Id);

            FundInList.Id = fund.Id;
            FundInList.Amount = fund.Amount;
            FundInList.Source = fund.Source;
            FundInList.LastUpdateDate = fund.LastUpdateDate;
            FundInList.UserId = fund.UserId;
        }
        private void OnFundAdded(FundDetailViewModel source, Fund fund)
        {
            Funds.Add(new FundViewModel(fund));
        }
        public async Task RefreshData()
        {
            IsRefreshing = true;
            Constants.isFundLoaded = false;
            Constants.isExpenseLoaded = false;
            await LoadFundData();
            await LoadExpenseData();
            MyBalanceValue = MyFundValue - MyExpenseValue;
            IsRefreshing = false;
        }
        public async Task LoadExpenseData()
        {
            if (Constants.isExpenseLoaded) goto ReflectValue;
            Constants.isExpenseLoaded = true;
            Expenses.Clear();
            var expenses = await _expenseStore.GetExpensesByUserId(Constants.curUserId);
            foreach (var exp in expenses)
                Expenses.Add(new ExpenseViewModel(exp));

ReflectValue:
            if (Expenses.Count != 0)
            {
                MyExpenseValue = Expenses.Where(x => x.UserId == Constants.curUserId).Sum(x => x.Amount);
            }
            Constants.myTotalExpense = (MyExpenseValue != 0) ? MyExpenseValue : 0;
            if(Constants.myTotalFund != 0)
            {
                MyBalanceValue = MyFundValue - MyExpenseValue;
            }
        }
        private async Task LoadFundData()
        {
            if (Constants.isFundLoaded) goto ReflectValue;
            Constants.isFundLoaded = true;
            Funds.Clear();
            var funds = await _fundStore.GetFundsByUserId(Constants.curUserId);
            foreach (Fund fund in funds)
                Funds.Add(new FundViewModel(fund));

            ReflectValue:
            if (Funds.Count != 0)
            {
                MyFundValue = Funds.Where(x => x.UserId == Constants.curUserId).Sum(x => x.Amount);
            }
            Constants.myTotalFund = (MyFundValue != 0) ? MyFundValue : 0;
            if(Constants.myTotalExpense != 0)
            {
                MyBalanceValue = MyFundValue - MyExpenseValue;
            }
            
        }
        private async Task SelectExpense(ExpenseViewModel expense)
        {
            if (expense == null) return;

            SelectedExpense = null;
            await _pageService.PushAsync(new AddExpensePage(expense));
        }
        private async Task SelectFund(FundViewModel fund)
        {
            if (fund == null) return;
            SelectedFund = null;
            await _pageService.PushAsync(new AddFundPage(fund));
        }
        private async Task AddExpense(ExpenseViewModel expense)
        {
            await _pageService.PushAsync(new AddExpensePage(expense));
        }
        private async Task AddFund(FundViewModel fund)
        {
            await _pageService.PushAsync(new AddFundPage(fund));
        }
        private async Task ViewFunds(FundViewModel fund)
        {
            await _pageService.PushAsync(new FundsListPage(fund)); ;
        }

        private async Task DeleteFund(FundViewModel fundViewModel)
        {
            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {fundViewModel.Source} : {fundViewModel.Amount}?", "YES", "NO"))
            {
                Funds.Remove(fundViewModel);

                var fund = await _fundStore.GetFund(fundViewModel.Id);
                await _fundStore.DeleteFund(fund);
            }
        }
        private async Task DeleteExpense(ExpenseViewModel expenseViewModel)
        {
            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {expenseViewModel.Description} : {expenseViewModel.Amount}?", "YES", "NO"))
            {
                Expenses.Remove(expenseViewModel);

                var expense = await _expenseStore.GetExpense(expenseViewModel.Id);
                await _expenseStore.DeleteExpense(expense);
            }
        }
    }
}
