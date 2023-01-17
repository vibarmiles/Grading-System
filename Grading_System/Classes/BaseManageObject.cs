using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Grading_System.Models;

namespace Grading_System.Classes
{
    public class BaseManageObject : Form
    {
        private readonly IBaseRepository baseRepository;
        private DataGridView tblList = new DataGridView();
        private Panel panel;
        private string id;
        protected DataTable dt;
        protected Queue<string> ids = new Queue<string>();

        public BaseManageObject(IBaseRepository baseRepository) : base()
        {
            this.baseRepository = baseRepository;
        }

        protected BaseManageObject() : base() { }
        public DataGridView TblList { set => tblList = value; }
        public Panel Panel { set => panel = value; }
        public string Id { get => id; }

        private void SetTableFormat()
        {
            dt = baseRepository.View();
            dt.Columns[0].ColumnName = "ID";
            tblList.DataSource = dt;
            tblList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tblList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void ClearTextBox()
        {
            foreach (TextBox textbox in panel.Controls.OfType<TextBox>())
            {
                textbox.Clear();
            }
        }

        protected void Cancel(object sender, EventArgs e)
        {
            ClearTextBox();
        }

        protected virtual void ViewTable()
        {
            SetTableFormat();
        }
    }
}
