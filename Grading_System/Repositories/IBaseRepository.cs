using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Grading_System.Repositories
{
    public interface IBaseRepository
    {
        DataTable View();
    }
}
