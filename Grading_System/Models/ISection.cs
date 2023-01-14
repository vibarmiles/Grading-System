using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface ISection : IObject
    {
        string Name { get; set; }
        int AdviserId { get; set; }
        string AdviserName { get; }
        int YearLevel { get; set; }
        int GetAdvisory(int teacherId);
    }
}
