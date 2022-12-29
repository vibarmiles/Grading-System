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
            BtnUpdate = btnUpdate;
            BtnAdd = btnAdd;
            TblList = tblList;
            Panel = panel1;
            this.Load += new EventHandler((sender, e) => 
            {
                this.Dock = DockStyle.Fill;
                cbAdvisers_DropDown(sender, e);
            });
            ViewTable();
        }

        protected override void ViewTable()
        {
            base.ViewTable();
            tblList.Columns[1].HeaderText = "Section ID";
            tblList.Columns[1].Name = "ID";
            tblList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        protected override void Add(object sender, EventArgs e)
        {
            string name = txtSectionName.Text;
            string adviser = cbAdvisers.Text;
            int yearlvl = InputValidator.CheckIntTextBox(txtYearLevel.Text);

            if (name != "" && adviser != "" && yearlvl > 0)
            {
                section.Name = name;
                section.AdviserId = advisers.FirstOrDefault(x => x.Value == adviser).Key;
                section.YearLevel = yearlvl;
                section.Add();
                Cancel(sender, e);
                ViewTable();
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Edit()
        {
            section.GetValues(Id);
            txtSectionName.Text = section.Name;
            cbAdvisers.Text = section.AdviserName;
            txtYearLevel.Text = section.YearLevel.ToString();
        }

        protected override void Update(object sender, EventArgs e)
        {
            string name = txtSectionName.Text;
            string adviser = cbAdvisers.Text;
            int yearlvl = InputValidator.CheckIntTextBox(txtYearLevel.Text);

            if (name != "" && adviser != "" && yearlvl > 0)
            {
                section.Name = name;
                section.AdviserId = advisers.FirstOrDefault(x => x.Value == adviser).Key;
                section.YearLevel = yearlvl;
                section.Update(Id);
                Cancel(sender, e);
                ViewTable();
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        protected override void Delete()
        {
            section.Delete(Id);
            ViewTable();
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
    }
}
