using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Repositories
{
    public interface ITeacher : IUser
    {
        string Specialization { get; set; }
    }
}
