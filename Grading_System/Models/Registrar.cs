using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public class Registrar : BaseUserModel, IUser
    {
        public Registrar(string connectionString) : base(connectionString, "RegistrarsView", "[UserID]") { }
    }
}
