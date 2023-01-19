using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface IHasObject : IObject
    {
        string Name { get; set; }
        double WrittenWork { get; set; }
        double PerformanceTask { get; set; }
        double QuarterlyAssessment { get; set; }
    }
}
