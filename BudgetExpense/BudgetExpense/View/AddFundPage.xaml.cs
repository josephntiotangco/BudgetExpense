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
    public partial class AddFundPage : ContentPage
    {
        public AddFundPage(FundViewModel fundView)
        {
            Constants.myAdID = "ca-app-pub-6838059012127071/1659262181";
            InitializeComponent();
            var pageService = new PageService();
            var fundStore = new SQLiteFundStore(DependencyService.Get<ISQLiteDb>());
            Title = (fundView.Id == 0) ? "New Fund" : "Edit Fund";
            BindingContext = new FundDetailViewModel(fundView ?? new FundViewModel(), fundStore, pageService);

        }
    }
}