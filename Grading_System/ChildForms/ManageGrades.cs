using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.ChildForms
{
    public partial class ManageGrades : Form
    {
        public ManageGrades(string connectionString)
        {
            InitializeComponent();
            this.Load += new EventHandler((s, ev) => this.Dock = DockStyle.Fill);
        }
    }
}
