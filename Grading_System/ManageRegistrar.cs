using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Grading_System.Classes;

namespace Grading_System
{
    public partial class ManageRegistrar : Form
    {
        public ManageRegistrar()
        {
            InitializeComponent();
        }

        private void Add_Registrar_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }
    }
}
