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
        private static DataGridViewButtonColumn AddButton(string name)
        {
            DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
            edit.Name = name;
            edit.Text = name;
            edit.UseColumnTextForButtonValue = true;
            return edit;
        }

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

        public static DataGridView SetDataGridViewFormat(object sender, string table, string orderColumn)
        {
            DataGridView dgv = (DataGridView)sender;
            DataTable dt = Database.ViewTable(table, orderColumn);

            dgv.Columns.Clear();
            dgv.Columns.Add(ForDataGridView.AddButton("Edit"));
            dgv.DataSource = dt;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            for(int i = 0; i < dgv.Columns.Count; i++)
            {
                if(i != 2)
                {
                    dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }

            dgv.Columns.Add(ForDataGridView.AddButton("Delete"));
            dgv.Columns[dgv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            return dgv;
        }
    }
}
