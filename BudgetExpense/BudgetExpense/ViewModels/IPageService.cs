using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BudgetExpense.ViewModels
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task<Page> PopAsync();
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task<string> DisplayPromptAsync(string title, string message, string accept, string cancel, string placeholder, Int32 maxLength, Keyboard keyboard, string initialValue);
        Task DisplayAlert(string title, string message, string ok);
    }
}
