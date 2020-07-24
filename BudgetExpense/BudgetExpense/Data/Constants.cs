using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace BudgetExpense.Data
{
    public static class Constants
    {
        public static string curUserName;
        public static string curFullName;
        public static int curUserId;
        public static string phoneDate = DateTime.Now.ToString("MM/dd/yyyy");
        public static bool isExpenseLoaded;
        public static bool isFundLoaded;
        public static decimal myTotalExpense;
        public static decimal myTotalFund;
        public static decimal myTotalBalance = myTotalFund - myTotalExpense;
        public static string myAdID;
    }
}
