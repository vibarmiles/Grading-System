using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grading_System.Models;

namespace Grading_System.Classes
{
    public class Login
    {
        private readonly IAccount account;
        private string username;
        private string password;

        public Login(IAccount account)
        {
            this.account = account;
        }

        public string Username { set => username = value; }
        public string Password { set => password = value; }

        public IDictionary<int, string> GetAccount()
        {
            return account.VerifyAccount(username, password);
        }
    }
}
