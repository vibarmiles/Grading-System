using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Grading_System.Classes
{
    internal class InputValidator
    {
        public static string CheckStringTextBox(string input)
        {
            if (input != null && Regex.IsMatch(input, @"^[a-zA-Z\s]+$")) {
                return input;
            }

            return null;
        }

        public static double CheckIntTextBox(string input)
        {
            if (input != null && Regex.IsMatch(input, @"^[0-9\.]{1,10}+$"))
            {
                return Double.Parse(input);
            }

            return 0;
        }

        public static DialogResult ContinueDelete()
        {
            string message = "Are you sure?";
            string caption = "Delete Row";
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
        }
    }
}
