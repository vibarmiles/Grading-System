using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Grading_System.Classes;
using Grading_System.Models;

namespace Grading_System.ChildForms
{
    public partial class ManageSubject : BaseManageObject
    {
        private readonly IHasObject subject;

        public ManageSubject(string connectionString) : base(new Subject(connectionString))
        {
            InitializeComponent();
            subject = new Subject(connectionString);
            TblList = tblList;
            Panel = panel1;
            this.Load += new EventHandler((sender, e) => this.Dock = DockStyle.Fill);
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
            tblList.Columns[1].HeaderText = "Subject Description";
            tblList.Columns[2].DefaultCellStyle.Format = "N2";
            tblList.Columns[3].DefaultCellStyle.Format = "N2";
            tblList.Columns[4].DefaultCellStyle.Format = "N2"; //Kakain muna ako...
        }

        private void Add(object sender, EventArgs e)
        {
            string sub = txtSubject.Text;

            if (sub != "")
            {
                dt.Rows.Add(null, sub);
            }
            else
            {
                MessageBox.Show("Invalid/Missing Input!");
            }
        }

        private void Update(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save Changes?", "Update Database", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            while (ids.Count > 0)
            {
                Console.WriteLine("Deleted!");
                subject.Delete(ids.Dequeue());
            }

            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                string name = row["SubjectName"].ToString();
                double ww = Convert.ToDouble(row["WrittenWork"]);
                double pt = Convert.ToDouble(row["PerformanceTask"]);
                double qa = Convert.ToDouble(row["QuarterlyAssessment"]);

                if (name == "" || ww < 0 || pt < 0 || qa < 0 || ww > 100 || pt > 100 || qa > 100 || (ww + pt + qa) != 100)
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

                string name = row["SubjectName"].ToString();
                double ww = Convert.ToDouble(row["WrittenWork"]);
                double pt = Convert.ToDouble(row["PerformanceTask"]);
                double qa = Convert.ToDouble(row["QuarterlyAssessment"]);

                if (name != "" && ww > 0 && pt > 0 && qa > 0 && ww <= 100 && pt <= 100 && qa <= 100 && (ww + pt + qa) == 100)
                {
                    subject.Name = name;
                    subject.WrittenWork = ww;
                    subject.PerformanceTask = pt;
                    subject.QuarterlyAssessment = qa;

                    string id = row["ID"].ToString();
                    Console.WriteLine(id);

                    if (id == "")
                    {
                        Console.WriteLine("Add");
                        subject.Add();
                    }
                    else
                    {
                        Console.WriteLine("Update");
                        subject.Update(id);
                    }
                }
            }

            MessageBox.Show("Successfully Updated!");
            ViewTable();
            Cancel(sender, e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewTable();
            ids.Clear();
        }
    }
}
