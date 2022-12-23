using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.Repositories
{
    public interface IObject
    {
        void Add();
        void GetValues(string id);
        void Update(string id);
        void Delete(string id);
    }
}
