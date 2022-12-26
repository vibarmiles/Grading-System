using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Repositories
{
    public class Registrar : BaseUserRepository, IUser
    {
        public Registrar(string connectionString) : base(connectionString, "RegistrarsView", "[UserID]") { }
    }
}
