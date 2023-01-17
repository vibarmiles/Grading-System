using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface IAccount
    {
        IDictionary<int, string> VerifyAccount(string username, string password);
        void ChangeProfile(string position, int id, string username, string password);
    }
}
