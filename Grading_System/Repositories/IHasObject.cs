using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Repositories
{
    public interface IHasObject : IObject
    {
        string Name { get; set; }
    }
}
