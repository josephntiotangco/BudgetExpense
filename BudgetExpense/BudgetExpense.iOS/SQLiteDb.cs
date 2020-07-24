using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BudgetExpense.iOS;
using BudgetExpense.Persistence;
using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace BudgetExpense.iOS
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "budgetExpense.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}