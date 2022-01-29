using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles
{
    public static class StringExtension
    {
        public static string Indent(this string line, string prefix)
        {
            return prefix + line;
        }

        public static string IndentWithLevel(this string line, int level)
        {
            //a string could contain multiple lines so Split it to be sure
            return line
                .Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None)
                .ToList()
                .MakeIndentedStringWithLevel(level);
        }
    }
}
