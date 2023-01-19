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
        private readonly IGradesExcel grades;
        private readonly IGrades grade;

        public ExcelFile(string connectionString, int sectionId, int teacherId, int subjectId)
        {
            this.connectionString = connectionString;
            this.sectionId = sectionId;
            this.teacherId = teacherId;
            this.subjectId = subjectId;
            grades = new Grades(connectionString);
            grade = new Grades(connectionString);
        }
        public string Name { get => name; set => name = value; }

        public void Export(string filename)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wkbook = excel.Workbooks.Add();

            System.Data.DataTable dt = grades.ExportExcel(sectionId, teacherId, subjectId);

            for (int i = 1; i <= 4; i++)
            {
                Excel._Worksheet worksheet = (Excel.Worksheet)excel.Worksheets.Add();
                worksheet.Name = "Quarter " + i;
                worksheet.Cells[1, "A"] = "WW:";
                worksheet.Cells[1, "B"] = Convert.ToDouble(dt.Rows[0]["WrittenWork"]) / 100;
                worksheet.Cells[2, "A"] = "PT:";
                worksheet.Cells[2, "B"] = Convert.ToDouble(dt.Rows[0]["PerformanceTask"]) / 100;
                worksheet.Cells[3, "A"] = "QA:";
                worksheet.Cells[3, "B"] = Convert.ToDouble(dt.Rows[0]["QuarterlyAssessment"]) / 100;
                worksheet.get_Range("A1:A3").Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                worksheet.get_Range("B1:B3").Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                worksheet.get_Range("A5:A7").Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                worksheet.get_Range("A7:I7").Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                worksheet.get_Range("5:6").Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                worksheet.get_Range("5:5").Cells.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 2d;
                worksheet.get_Range("5:5").Cells.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                worksheet.get_Range("6:6").Cells.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                worksheet.get_Range("D:I").Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 2d;
                worksheet.get_Range("D:I").Cells.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 2d;
                worksheet.get_Range("1:4").Font.Color = Excel.XlRgbColor.rgbLightGray;
                worksheet.get_Range("1:4").Interior.Color = Excel.XlRgbColor.rgbLightGray;

                worksheet.Cells[5, "D"] = "Total";
                Excel.Range range = worksheet.get_Range("D5", "I5");
                range.Merge(true);

                worksheet.Cells[5, "J"] = "Category:";
                worksheet.Cells[6, "D"] = "WW";
                worksheet.Cells[6, "E"] = "Percentage";
                worksheet.Cells[6, "F"] = "PT";
                worksheet.Cells[6, "G"] = "Percentage";
                worksheet.Cells[6, "H"] = "QA";
                worksheet.Cells[6, "I"] = "Percentage";
                worksheet.Cells[6, "J"] = "Activity";

                worksheet.Cells[7, "A"] = "ID";
                worksheet.Cells[7, "B"] = "Name";
                worksheet.Cells[7, "C"] = "Average";
                worksheet.Cells[7, "D"] = string.Format("=SUMIF(5:5, \"=WW\", 7:7)");
                worksheet.Cells[7, "E"] = 100;
                worksheet.Cells[7, "F"] = string.Format("=SUMIF(5:5, \"=PT\", 7:7)");
                worksheet.Cells[7, "G"] = 100;
                worksheet.Cells[7, "H"] = string.Format("=SUMIF(5:5, \"=QA\", 7:7)");
                worksheet.Cells[7, "I"] = 100;
                worksheet.Cells[7, "J"] = "Total Points:";

                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    worksheet.Cells[a + 8, "A"] = dt.Rows[a]["StudentID"];
                    worksheet.get_Range("A" + (a + 8)).Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    worksheet.Cells[a + 8, "B"] = dt.Rows[a]["Name"];
                    worksheet.Cells[a + 8, "C"] = string.Format("=ROUND((E" + (a + 8) + "*B1)+(G" + (a + 8) + "*B2)+(I" + (a + 8) + "*B3),2)");
                    worksheet.get_Range("C" + (a + 8)).Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    worksheet.Cells[a + 8, "D"] = string.Format("=SUMIF(5:5, \"=WW\", " + (a + 8) + ":" + (a + 8)  + ")");
                    worksheet.Cells[a + 8, "E"] = string.Format("=ROUND(D" + (a + 8) + "/D7*100,2)");
                    worksheet.Cells[a + 8, "F"] = string.Format("=SUMIF(5:5, \"=PT\", " + (a + 8) + ":" + (a + 8)  + ")");
                    worksheet.Cells[a + 8, "G"] = string.Format("=ROUND(F" + (a + 8) + "/F7*100,2)");
                    worksheet.Cells[a + 8, "H"] = string.Format("=SUMIF(5:5, \"=QA\", " + (a + 8) + ":" + (a + 8)  + ")");
                    worksheet.Cells[a + 8, "I"] = string.Format("=ROUND(H" + (a + 8) + "/H7*100,2)");
                    worksheet.get_Range("D" + (a + 8) + ":I" + (a + 8)).Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }

                worksheet.get_Range("A:J").Columns.AutoFit();
            }

            Excel._Worksheet averages = (Excel.Worksheet)excel.Worksheets.Add();
            averages.Name = "Averages";
            averages.Cells[1, "A"] = "ID";
            averages.Cells[1, "B"] = "Name";
            averages.Cells[1, "C"] = "First Quarter";
            averages.Cells[1, "D"] = "Second Quarter";
            averages.Cells[1, "E"] = "Third Quarter";
            averages.Cells[1, "F"] = "Fourth Quarter";

            for (int a = 0; a < dt.Rows.Count; a++)
            {
                averages.Cells[a + 2, "A"] = dt.Rows[a]["StudentID"];
                averages.Cells[a + 2, "B"] = dt.Rows[a]["Name"];
                averages.Cells[a + 2, "C"] = string.Format("='Quarter 1'!C" + (a + 8));
                averages.Cells[a + 2, "D"] = string.Format("='Quarter 2'!C" + (a + 8));
                averages.Cells[a + 2, "E"] = string.Format("='Quarter 3'!C" + (a + 8));
                averages.Cells[a + 2, "F"] = string.Format("='Quarter 4'!C" + (a + 8));
            }

            averages.get_Range("A:F").Columns.AutoFit();

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

        public System.Data.DataTable Import(string filename)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            IGrades grades = new Grades(connectionString);

            try
            {
                using (OleDbConnection conn = new OleDbConnection(filename))
                {
                    conn.Open();

                    System.Data.DataTable excelTable = conn.GetSchema("Tables");
                    string query = "SELECT * FROM [Averages$]";

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
                    double prelim = Convert.ToDouble(row["First Quarter"]);
                    double midterm = Convert.ToDouble(row["Second Quarter"]);
                    double prefinal = Convert.ToDouble(row["Third Quarter"]);
                    double final = Convert.ToDouble(row["Fourth Quarter"]);

                    if (prelim > 100 || midterm > 100 || prefinal > 100 || final > 100 || prelim < 0 || midterm < 0 || prefinal < 0 || final < 0)
                    {
                        throw new Exception("One of the Input is Invalid.\nImporting Canceled.");
                    }
                }

                System.Data.DataTable dt2 = this.grade.GetSectionGrades(sectionId, teacherId, subjectId);

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    double prelim = Convert.ToDouble(dt.Rows[i]["First Quarter"]);
                    double midterm = Convert.ToDouble(dt.Rows[i]["Second Quarter"]);
                    double prefinal = Convert.ToDouble(dt.Rows[i]["Third Quarter"]);
                    double final = Convert.ToDouble(dt.Rows[i]["Fourth Quarter"]);

                    DataRow dr = dt2.Rows[i];
                    dr["1"] = prelim;
                    dr["2"] = midterm;
                    dr["3"] = prefinal;
                    dr["4"] = final;
                    dr["Average"] = (prelim + midterm + final + prefinal)/4;
                    dr["Grade"] = null;
                }

                return dt2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
