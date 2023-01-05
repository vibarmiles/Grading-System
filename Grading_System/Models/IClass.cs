using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface IClass
    {
        int TeacherID { get; set; }
        int SubjectID { get; set; }
        void Add();
        void Remove();
        DataTable GetList(int id);
    }
}
