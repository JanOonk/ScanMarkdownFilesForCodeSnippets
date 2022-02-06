using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles.Extensions
{
    public static class StringExtension
    {
        public const string Indentation = "  ";

        public static string Indent(this string line, string prefix)
        {
            return prefix + line;
        }

        public static bool Like(this string s1, string s2)
        {
            //case insensitive compare
            return s1.ToLower().CompareTo(s2.ToLower()) == 0;
        }

        public static string IndentWithLevel(this string line, int level, string prefix = Indentation)
        {
            //a string could contain multiple lines so Split it to be sure
            List<string> splitted = line
                .Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None) //supports both Unix and non-Unix newlines
                .ToList();

            return splitted.MakeIndentedStringWithLevel(level, prefix);
        }

        public static int LineIndexFromPos(this string input, int posIndex)
        {
            int lineNumber = 0;
            
            for (int i = 0; i <= posIndex; i++)
            {
                if (input[i] == '\n') lineNumber++;
            }

            return lineNumber;
        }
    }
}
