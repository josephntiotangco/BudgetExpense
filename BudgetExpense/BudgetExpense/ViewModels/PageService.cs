using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BudgetExpense.ViewModels
{
    public class PageService : IPageService
    {
        public async Task DisplayAlert(string title, string message, string ok)
        {
            await MainPage.DisplayAlert(title, message, ok);
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await MainPage.DisplayAlert(title, message,ok,cancel);
        }
        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }
        public async Task<string> DisplayPromptAsync(string title, string message, string accept, string cancel, string placeholder, Int32 maxLength, Keyboard keyboard, string initialValue)
        {
            return await MainPage.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength: maxLength, keyboard: keyboard, initialValue: initialValue);
        }
        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync();
        }

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
    }
}
