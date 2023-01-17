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
        private string position;
        private int id;

        public Login(IAccount account)
        {
            this.account = account;
        }

        public string Username { set => username = value; }
        public string Password { set => password = value; }
        public string Position { set => position = value; }
        public int Id { set => id = value; }

        public IDictionary<int, string> GetAccount()
        {
            return account.VerifyAccount(username, password);
        }

        public void ChangeProfile()
        {
            account.ChangeProfile(position, id, username, password);
        }
    }
}
