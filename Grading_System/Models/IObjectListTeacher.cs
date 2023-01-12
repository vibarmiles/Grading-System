using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface IObjectListTeacher
    {
        IDictionary<int, string> GetTeacherList(int id);
    }
}
