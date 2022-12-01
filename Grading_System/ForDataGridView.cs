using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grading_System
{
    internal class ForDataGridView
    {
        public static DataGridViewButtonColumn AddButton(string name)
        {
            DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
            edit.Name = name;
            edit.Text = name;
            edit.UseColumnTextForButtonValue = true;
            return edit;
        }

        public static bool ButtonColumn_Clicked(DataGridView dgv, DataGridViewCellEventArgs e, string name)
        {
            if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dgv.Columns[e.ColumnIndex].Name == name && e.RowIndex >= 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
