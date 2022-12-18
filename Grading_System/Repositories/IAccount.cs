using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Repositories
{
    public interface IAccount
    {
        string VerifyAccount(string username, string password);
    }
}
