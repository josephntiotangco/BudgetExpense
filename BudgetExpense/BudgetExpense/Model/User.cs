using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BudgetExpense.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string UserName { get; set; }
        [MaxLength(255)]
        public string PassWord { get; set; }
        [MaxLength(255)]
        public string FullName { get; set; }

        public User() { }
        public User(string userName, string passWord, string fullName="")
        {
            this.UserName = userName;
            this.PassWord = passWord;
            this.FullName = fullName;
        }
    }
}
