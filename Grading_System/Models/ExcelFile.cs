using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Grading_System.Models
{


    public class ExcelFile : IFileExport
    {
        private string name;
        private string position;
        private int id;
        private int teacherId;
        private int subjectId;
        IGrades grades;

        public ExcelFile(string name, string connectionString, int id, string position, int teacherId, int subjectId)
        {
            this.name = name;
            this.position = position;
            this.id = id;
            this.teacherId = teacherId;
            this.subjectId = subjectId;
            grades = new Grades(connectionString);
        }

        public void Export(string filename)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                Excel.Application excel = new Excel.Application();
                Excel.Workbook wkbook = excel.Workbooks.Add();
                Excel._Worksheet worksheet = (Excel.Worksheet)excel.Worksheets.Add();
                worksheet.Name = name;

                System.Data.DataTable dt = grades.View(id, position, teacherId, subjectId);

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
                wkbook.SaveAs(filename);
                excel.Quit();
            }
        }
    }
}
