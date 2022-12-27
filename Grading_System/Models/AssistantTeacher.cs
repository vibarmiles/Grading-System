using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Models
{
    public class AssistantTeacher : BaseUserModel, IUser
    {
        public AssistantTeacher(string connectionString) : base(connectionString, "AssistantTeachersView", "[UserID]") { }
    }
}
