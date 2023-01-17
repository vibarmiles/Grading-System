using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Grading_System.Models
{
    public interface IUser : IObject
    {
        string Fname { set; get; }
        string Mname { set; get; }
        string Lname { set; get; }
        string Username { get; }
        string Gender { set; get; }
        string Position { set; get; }
    }
}
