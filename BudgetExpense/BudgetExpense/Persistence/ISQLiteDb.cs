using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetExpense.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
