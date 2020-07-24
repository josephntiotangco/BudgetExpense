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
    public class FundDetailViewModel
    {
        private readonly IFundStore _fundStore;
        private readonly IPageService _pageService;

        public Fund Fund { get; private set; }
        public ICommand SaveFundCommand { get; private set; }
        public FundDetailViewModel(FundViewModel viewModel, IFundStore fundStore, IPageService pageService)
        {
            if (viewModel == null) throw new ArgumentException(nameof(viewModel));
            _fundStore = fundStore;
            _pageService = pageService;

            SaveFundCommand = new Command(async () => await SaveFund());

            Fund = new Fund
            {
                Id = viewModel.Id,
                Amount = viewModel.Amount,
                Source = viewModel.Source,
                LastUpdateDate = Convert.ToDateTime(Constants.phoneDate),
                UserId = Constants.curUserId
            };
        }

        private async Task SaveFund()
        {
            if (string.IsNullOrWhiteSpace(Fund.Source) && Fund.Amount == 0)
            {
                await _pageService.DisplayAlert("FUND", "Please complete fund details.", "OK");
                return;
            }

            if (Fund.Id == 0)
            {
                await _fundStore.AddFund(Fund);
                MessagingCenter.Send(this, Events.FundAdded, Fund);
                await _pageService.PopAsync();
            }
            else
            {
                await _fundStore.UpdateFund(Fund);
                MessagingCenter.Send(this, Events.FundUpdated, Fund);
                await _pageService.PopAsync();
            }
        }
    }
}
