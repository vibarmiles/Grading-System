﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public interface IGrades
    {
        DataTable GetGrades(int studentId, int subjectId, int teacherId);
        DataTable View(int id, string position, int teacherId);
        void Update(int studentId, int subjectId, int teacherId, int quarter, double grade);
    }
}