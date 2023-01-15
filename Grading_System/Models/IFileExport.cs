using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface IFileExport
    {
        string Name { get; set; }

        void Export(string filename);
    }
}
