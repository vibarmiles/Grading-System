using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Repositories
{
    public class AssistantTeacher : BaseUserRepository, IUser
    {
        public AssistantTeacher(string connectionString) : base(connectionString, "AssistantTeachersView", "[UserID]") { }
    }
}
