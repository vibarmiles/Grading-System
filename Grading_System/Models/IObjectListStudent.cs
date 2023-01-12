using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface IObjectListStudent
    {
        IDictionary<int[], string> GetStudentList(int id, int teacherId);
    }
}
