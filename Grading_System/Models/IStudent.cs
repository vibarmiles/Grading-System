using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface IStudent : IObject
    {
        string FName { get; set; }
        string MName { get; set; }
        string LName { get; set; }
        string Gender { get; set; }
        DateTime EnrollmentDate { get; set; }
        string LRN { get; set; }
        string SectionName { get; }
        int SectionID { get; set; }
    }
}
