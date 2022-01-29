using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles
{
    public static class ArgumentsParser
    {
        public static Arguments Parse(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Error in arguments!");
                Console.WriteLine("Syntax: <program> <rootFolderToScan> [filePattern = *.md] [-onlyTopLevel = no]");
                Console.WriteLine("  For example:");
                Console.WriteLine("    ScanMarkDownFiles /sources/myrepo");
                Console.WriteLine("      will scan all /sources/myrepo including subdirs for *.md files");
                Console.WriteLine("");
                Console.WriteLine("    ScanMarkDownFiles /sources/myrepo *.mdwn");
                Console.WriteLine("      will scan all /sources/myrepo including subdirs for *.mdwn files");
                Console.WriteLine("");
                Console.WriteLine("    ScanMarkDownFiles /sources/myrepo *.md -onlyTopLevel");
                Console.WriteLine("      will only scan the top level directory /sources/myrepo for *.md files");
                Environment.Exit(1);
            }

            Arguments arguments = new Arguments();

            arguments.RootFolder = args[0];

            if (args.Length >= 2) arguments.FilePattern = args[1];
            if (args.Length == 3) arguments.OnlyToplevel = (args[2].ToLower().Trim() == "-onlytoplevel");

            return arguments;
        }
    }
}
