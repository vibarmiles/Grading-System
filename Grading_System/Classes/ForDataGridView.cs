using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows.Forms;

namespace Grading_System.Classes
{
    internal class ForDataGridView
    {
        public static bool ButtonColumn_Clicked(DataGridView dgv, DataGridViewCellEventArgs e, string name)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dgv.Columns[e.ColumnIndex].Name == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
