using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Grading_System.Models
{
    public class ExcelFile : IFileExport, IFileImport
    {
        private string name;
        private string connectionString;
        private int sectionId;
        private int teacherId;
        private int subjectId;
        IGradesExcel grades;

        public ExcelFile(string connectionString, int sectionId, int teacherId, int subjectId)
        {
            this.connectionString = connectionString;
            this.sectionId = sectionId;
            this.teacherId = teacherId;
            this.subjectId = subjectId;
            grades = new Grades(connectionString);
        }
        public string Name { get => name; set => name = value; }

        public void Export(string filename)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wkbook = excel.Workbooks.Add();
            Excel._Worksheet worksheet = (Excel.Worksheet)excel.Worksheets.Add();
            worksheet.Name = name;

            System.Data.DataTable dt = grades.ExportExcel(sectionId, teacherId, subjectId);

            worksheet.Cells[1, "A"] = dt.Columns[0].ColumnName;
            worksheet.Cells[1, "B"] = dt.Columns[1].ColumnName;
            worksheet.Cells[1, "C"] = dt.Columns[2].ColumnName;
            worksheet.Cells[1, "D"] = dt.Columns[3].ColumnName;
            worksheet.Cells[1, "E"] = dt.Columns[4].ColumnName;
            worksheet.Cells[1, "F"] = dt.Columns[5].ColumnName;
            worksheet.Cells[1, "G"] = dt.Columns[6].ColumnName;
            worksheet.Cells[1, "H"] = dt.Columns[7].ColumnName;
            worksheet.get_Range("A1", "H1").Font.Bold = true;

            int iteration = 1;
            foreach (DataRow row in dt.Rows)
            {
                iteration++;
                worksheet.Range["C" + iteration + ":G" + iteration].NumberFormat = "0.00";
                worksheet.Cells[iteration, "A"] = row["StudentID"].ToString();
                worksheet.Cells[iteration, "B"] = row["Name"].ToString();
                worksheet.Cells[iteration, "C"] = row["1"];
                worksheet.Cells[iteration, "D"] = row["2"];
                worksheet.Cells[iteration, "E"] = row["3"];
                worksheet.Cells[iteration, "F"] = row["4"];
                worksheet.Cells[iteration, "G"] = row["Average"];
                worksheet.Cells[iteration, "H"] = row["Grade"];
            }

            ((Excel.Range)worksheet.Columns[2]).AutoFit();

            try
            {
                wkbook.SaveAs(filename);
                MessageBox.Show("Successfully Exported!");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            excel.Quit();
        }

        public void Import(string filename)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            IGrades grades = new Grades(connectionString);

            try
            {
                using (OleDbConnection conn = new OleDbConnection(filename))
                {
                    conn.Open();

                    System.Data.DataTable excelTable = conn.GetSchema("Tables");
                    string query = "SELECT * FROM [" + excelTable.Rows[0]["TABLE_NAME"].ToString() + "]";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        Console.WriteLine(cmd.CommandText);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                        adapter.Fill(dt);
                    }

                    conn.Close();
                }

                foreach (DataRow row in dt.Rows)
                {
                    double prelim = Convert.ToDouble(row["1"]);
                    double midterm = Convert.ToDouble(row["2"]);
                    double prefinal = Convert.ToDouble(row["3"]);
                    double final = Convert.ToDouble(row["4"]);

                    if (prelim > 100 || midterm > 100 || prefinal > 100 || final > 100 || prelim < 0 || midterm < 0 || prefinal < 0 || final < 0)
                    {
                        MessageBox.Show("One of the Input is Invalid.\nImporting Canceled.");
                        return;
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    grades.Update(Convert.ToInt32(row["StudentID"]), subjectId, teacherId, 1, Convert.ToDouble(row["1"]));
                    grades.Update(Convert.ToInt32(row["StudentID"]), subjectId, teacherId, 2, Convert.ToDouble(row["2"]));
                    grades.Update(Convert.ToInt32(row["StudentID"]), subjectId, teacherId, 3, Convert.ToDouble(row["3"]));
                    grades.Update(Convert.ToInt32(row["StudentID"]), subjectId, teacherId, 4, Convert.ToDouble(row["4"]));
                }

                MessageBox.Show("Data Import Finished!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
