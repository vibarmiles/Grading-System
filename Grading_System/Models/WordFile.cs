using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;

namespace Grading_System.Models
{
    internal class WordFile : IFileExport
    {
        private int format;
        private int studentId;
        IGradesWord grades;

        public WordFile(string connectionString, int studentId, int format)
        {
            this.studentId = studentId;
            this.format = format;
            grades = new Grades(connectionString);
        }

        public void Export(string filename)
        {
            System.Data.DataTable dt = grades.ExportWord(studentId);

            Word.Application word = new Word.Application();
            Word.Document document = word.Documents.Add();
            Word.Table table = document.Tables.Add(document.Range(), dt.Rows.Count + 1, 7);
            
            foreach(DataColumn column in dt.Columns)
            {
                table.Rows[1].Cells[1].Range.Text = column.ColumnName;
                table.Rows[1].Cells[2].Range.Text = column.ColumnName;
                table.Rows[1].Cells[3].Range.Text = column.ColumnName;
                table.Rows[1].Cells[4].Range.Text = column.ColumnName;
                table.Rows[1].Cells[5].Range.Text = column.ColumnName;
                table.Rows[1].Cells[6].Range.Text = column.ColumnName;
                table.Rows[1].Cells[7].Range.Text = column.ColumnName;
            }

            int iteration = 2;
            foreach (DataRow row in dt.Rows)
            {
                table.Rows[iteration].Cells[1].Range.Text = row["SubjectName"].ToString();
                table.Rows[iteration].Cells[2].Range.Text = row["1"].ToString();
                table.Rows[iteration].Cells[3].Range.Text = row["2"].ToString();
                table.Rows[iteration].Cells[4].Range.Text = row["3"].ToString();
                table.Rows[iteration].Cells[5].Range.Text = row["4"].ToString();
                table.Rows[iteration].Cells[6].Range.Text = row["Average"].ToString();
                table.Rows[iteration].Cells[7].Range.Text = row["Grade"].ToString();
                iteration++;
            }

            if (format == 2)
            {
                document.SaveAs2(filename);
            } else if (format == 3)
            {
                document.SaveAs2(filename, WdSaveFormat.wdFormatPDF);
            }

            word.Quit();
        }
    }
}
