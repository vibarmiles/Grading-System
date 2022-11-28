using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Grading_System
{
    internal class InputValidator
    {
        public static string CheckStringTextBox(string input)
        {
            if (input != null && Regex.IsMatch(input, @"^[a-zA-Z]+$")) {
                return input;
            }

            return null;
        }

        public static int CheckIntTextBox(string input)
        {
            if (input != null && Regex.IsMatch(input, @"^[0-9]{1,10}+$"))
            {
                return Int32.Parse(input);
            }

            return 0;
        }
    }
}
