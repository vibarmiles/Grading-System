using Grading_System.Classes;
using Grading_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Grading_System.ChildForms
{
    public partial class ManageSection : BaseManageObject
    {
        private readonly ISection section;
        private readonly IObjectList teacher;
        private IDictionary<int, string> advisers;

        public ManageSection(string connectionString) : base(new Section(connectionString))
        {
            InitializeComponent();
            section = new Section(connectionString);
            teacher = new Teacher(connectionString);
            TblList = tblList;
            Panel = panel1;
            this.Load += new EventHandler((sender, e) => 
            {
                this.Dock = DockStyle.Fill;
                cbAdvisers_DropDown(sender, e);
            });

            ViewTable();

            tblList.KeyDown += new KeyEventHandler((sender, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                {
                    foreach (DataGridViewRow row in tblList.SelectedRows)
                    {
                        ids.Enqueue(row.Cells["ID"].Value.ToString());
                        Console.WriteLine(row.Cells["ID"].Value.ToString());
                    }
                }
            });
        }

        protected override void ViewTable()
        {
            base.ViewTable();
            tblList.Columns[1].HeaderText = "Section";
            tblList.Columns[3].HeaderText = "Year Level";
        }

        private void Add(object sender, EventArgs e)
        {
            string name = txtSectionName.Text;
            string adviser = cbAdvisers.Text;
            int yearlvl = InputValidator.CheckIntTextBox(txtYearLevel.Text);

            if (name != "" && adviser != "" && yearlvl > 0)
            {
                dt.Rows.Add(null, name, adviser, yearlvl);
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }

            Cancel(sender, e);
        }

        private void Update(object sender, EventArgs e)
        {
            while (ids.Count > 0)
            {
                Console.WriteLine("Deleted!");
                section.Delete(ids.Dequeue());
            }

            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                string name = row["SectionName"].ToString();
                string adviser = row["Adviser"].ToString();
                int yearlvl = InputValidator.CheckIntTextBox(row["YearLevel"].ToString());

                if (name == "" || adviser == "" || yearlvl <= 0)
                {
                    MessageBox.Show("Invalid Table Input!");
                    return;
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                string name = row["SectionName"].ToString();
                string adviser = row["Adviser"].ToString();
                int yearlvl = InputValidator.CheckIntTextBox(row["YearLevel"].ToString());

                if (name != "" && adviser != "" && yearlvl > 0)
                {
                    section.Name = name;
                    section.AdviserId = advisers.FirstOrDefault(x => x.Value == adviser).Key;
                    section.YearLevel = yearlvl;

                    string id = row["ID"].ToString();
                    Console.WriteLine(id);

                    if (id == "")
                    {
                        Console.WriteLine("Add");
                        section.Add();
                    }
                    else
                    {
                        Console.WriteLine("Update");
                        section.Update(id);
                    }
                }
            }

            MessageBox.Show("Successfully Updated!");
            ViewTable();
            Cancel(sender, e);
        }

        private void cbAdvisers_DropDown(object sender, EventArgs e)
        {
            advisers = teacher.GetList();
            cbAdvisers.Items.Clear();

            foreach(string adviser in advisers.Values)
            {
                cbAdvisers.Items.Add(adviser);
            }
        }

        public void RefreshTable()
        {
            ViewTable();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewTable();
            ids.Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string name = txtSectionName.Text;
            string adviser = cbAdvisers.Text;
            int yearlvl = InputValidator.CheckIntTextBox(txtYearLevel.Text);

            if (name != "" && adviser != "" && yearlvl > 0)
            {
                DataGridViewRow row = tblList.SelectedRows[0];
                row.Cells["SectionName"].Value = name;
                row.Cells["Adviser"].Value = adviser;
                row.Cells["YearLevel"].Value = yearlvl;
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }

            Cancel(sender, e);
        }
    }
}
