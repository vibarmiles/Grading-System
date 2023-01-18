using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Grading_System.Models
{
    internal class WordFile : IFileExport
    {
        private int format;
        private string name;
        private int studentId;
        IGradesWord grades;

        public WordFile(string connectionString, int studentId, int format)
        {
            this.studentId = studentId;
            this.format = format;
            grades = new Grades(connectionString);
        }

        public string Name { get => name; set => name = value; }

        public void Export(string filename)
        {
            System.Data.DataTable dt = grades.ExportWord(studentId);

            Word.Application word = new Word.Application();
            Word.Document document = word.Documents.Add();
            document.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            Word.Range range = document.Range();
            Word.Table table = document.Tables.Add(range, dt.Rows.Count + 4, 7);

            table.Cell(1, 2).Merge(table.Cell(1, 5));
            table.Cell(2, 2).Merge(table.Cell(2, 5));
            table.Cell(3, 2).Merge(table.Cell(3, 7));

            table.Rows[1].Cells[1].Range.Text = "Name:";
            table.Rows[2].Cells[1].Range.Text = "Adviser:";
            table.Rows[1].Cells[3].Range.Text = "Year Level:";
            table.Rows[2].Cells[3].Range.Text = "Section:";
            table.Rows[3].Cells[1].Range.Text = "School Year";

            table.Rows[1].Cells[2].Range.Text = dt.Rows[0]["Name"].ToString();
            table.Rows[2].Cells[2].Range.Text = dt.Rows[0]["Adviser"].ToString();
            table.Rows[1].Cells[4].Range.Text = dt.Rows[0]["YearLevel"].ToString();
            table.Rows[2].Cells[4].Range.Text = dt.Rows[0]["SectionName"].ToString();
            table.Rows[3].Cells[2].Range.Text = dt.Rows[0]["SchoolYear"].ToString();

            dt.Columns.Remove("Name");
            dt.Columns.Remove("Adviser");
            dt.Columns.Remove("YearLevel");
            dt.Columns.Remove("SectionName");
            dt.Columns.Remove("SchoolYear");

            int iteration = 1;
            foreach(DataColumn column in dt.Columns)
            {
                if (iteration == 1)
                {
                    table.Rows[4].Cells[iteration].Range.Text = "Subject";
                    iteration++;
                    continue;
                }
                table.Rows[4].Cells[iteration].Range.Text = column.ColumnName;
                iteration++;
            }

            iteration = 5;
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["Average"].ToString());
                table.Rows[iteration].Cells[1].Range.Text = row["SubjectName"].ToString();
                table.Rows[iteration].Cells[2].Range.Text = row["1"].ToString();
                table.Rows[iteration].Cells[3].Range.Text = row["2"].ToString();
                table.Rows[iteration].Cells[4].Range.Text = row["3"].ToString();
                table.Rows[iteration].Cells[5].Range.Text = row["4"].ToString();
                table.Rows[iteration].Cells[6].Range.Text = row["Average"].ToString();
                table.Rows[iteration].Cells[7].Range.Text = row["Grade"].ToString();
                iteration++;
            }

            table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
            table.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth150pt;
            table.Borders.OutsideColor = WdColor.wdColorBlack;
            table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
            table.Borders.InsideLineWidth = WdLineWidth.wdLineWidth150pt;
            table.Borders.InsideColor = WdColor.wdColorBlack;
            table.Rows[4].Range.Font.Bold = 1;
            table.Cell(1, 3).Range.Bold = 1;
            table.Cell(2, 3).Range.Bold = 1;

            for (int i = 1; i <= table.Rows.Count; i++)
            {
                table.Cell(i, 1).Range.Font.Bold = 1;
            }

            try
            {
                if (format == 1)
                {
                    document.SaveAs2(filename);
                }
                else if (format == 2)
                {
                    document.SaveAs2(filename, WdSaveFormat.wdFormatPDF);
                }

                MessageBox.Show("Successfully Exported!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            word.Quit();
        }
    }
}