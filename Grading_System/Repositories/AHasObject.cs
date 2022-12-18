using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grading_System.Repositories
{
    public abstract class AHasObject : AObject
    {
        protected AHasObject(string connectionString, string tableOrView, string key) : base(connectionString, tableOrView, key) { }
    
        public abstract string Name { get; set; }
    }
}
