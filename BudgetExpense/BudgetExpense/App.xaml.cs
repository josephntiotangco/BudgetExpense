using BudgetExpense.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetExpense
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage_());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
