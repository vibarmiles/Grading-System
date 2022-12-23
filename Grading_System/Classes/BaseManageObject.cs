using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Grading_System.Repositories;

namespace Grading_System.Classes
{
    public class BaseManageObject : Form
    {
        private readonly IBaseRepository baseRepository;
        private Button btnUpdate;
        private Button btnAdd;
        private DataGridView tblList;
        private Panel panel;
        private string id;

        public BaseManageObject(IBaseRepository baseRepository) : base()
        {
            this.baseRepository = baseRepository;
        }

        protected BaseManageObject() : base() { }
        public Button BtnUpdate { set => btnUpdate = value; }
        public Button BtnAdd { set => btnAdd = value; }
        public DataGridView TblList { set => tblList = value; }
        public Panel Panel { set => panel = value; }
        public string Id { get => id; }

        private void SetTableFormat()
        {
            DataTable dt = baseRepository.View();
            DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();

            edit.Name = "Edit";
            edit.Text = "Edit";
            edit.UseColumnTextForButtonValue = true;
            delete.Name = "Delete";
            delete.Text = "Delete";
            delete.UseColumnTextForButtonValue = true;

            btnUpdate.Hide();
            tblList.Columns.Clear();
            tblList.CellClick -= new DataGridViewCellEventHandler(Edit_Click);
            tblList.Columns.Add(edit);
            tblList.DataSource = dt;
            tblList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            for (int i = 0; i < tblList.Columns.Count; i++)
            {
                if (i != 2)
                {
                    tblList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }

            tblList.Columns.Add(delete);
            tblList.Columns[tblList.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tblList.CellClick += new DataGridViewCellEventHandler(Edit_Click);
        }

        private void Edit_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.id = tblList.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            } catch (ArgumentOutOfRangeException) { }

            if (ForDataGridView.ButtonColumn_Clicked(tblList, e, "Edit"))
            {
                btnUpdate.Show();
                btnAdd.Text = "Cancel";
                btnAdd.Click -= Add;
                btnAdd.Click += Cancel;
                Edit();
            }

            if (ForDataGridView.ButtonColumn_Clicked(tblList, e, "Delete"))
            {
                DialogResult result = InputValidator.ContinueDelete();

                if (result is DialogResult.Yes)
                {
                    Delete();
                }
            }
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
            btnUpdate.Hide();
            btnAdd.Text = "Add";
            btnAdd.Click += Add;
            btnAdd.Click -= Cancel;
            ClearTextBox();
        }

        protected virtual void ViewTable()
        {
            SetTableFormat();
        }

        protected virtual void Add(object sender, EventArgs e)
        {

        }

        protected virtual void Edit()
        {

        }

        protected virtual void Update(object sender, EventArgs e)
        {

        }

        protected virtual void Delete()
        {

        }
    }
}
