using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface ITeacher : IUser
    {
        string Specialization { get; set; }
    }
}
