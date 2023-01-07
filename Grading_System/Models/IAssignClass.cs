using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    internal interface IAssignClass : IClass
    {
        int StudentID { get; set; }
    }
}
