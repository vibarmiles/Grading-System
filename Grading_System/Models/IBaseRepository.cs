﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface IBaseRepository
    {
        DataTable View();
    }
}
