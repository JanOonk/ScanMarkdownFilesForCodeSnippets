﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles
{
    public static class ListExtension
    {
        static string Indentation = "  ";

        public static List<string> Indent(this List<string> lines, string prefix)
        {
            return lines.Select(line => line.Indent(prefix)).ToList();
        }

        public static List<string> IndentWithLevel(this List<string> lines, int level)
        {
            return lines.Select(line => line.IndentWithLevel(level)).ToList();
        }

        public static string MakeIndentedString(this List<string> lines, string prefix)
        {
            return String.Join($"{Environment.NewLine}", lines.Indent(prefix));
        }

        public static string MakeIndentedStringWithLevel(this List<string> lines, int level)
        {
            return MakeIndentedString(lines, string.Concat(Enumerable.Repeat(Indentation, level)));
        }
    }
}
