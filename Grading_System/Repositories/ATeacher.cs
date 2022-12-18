using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Repositories
{
    public abstract class ATeacher : AUser
    {
        public ATeacher(string connectionString) : base(connectionString, "TeachersView") { }

        public abstract string Specialization { get; set; }
    }
}
