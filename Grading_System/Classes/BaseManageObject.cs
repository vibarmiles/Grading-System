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
            tblList.DataSource = dt;
            tblList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            for (int i = 0; i < tblList.Columns.Count; i++)
            {
                if (i != 1)
                {
                    tblList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
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
            btnAdd.Click -= Add;
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
