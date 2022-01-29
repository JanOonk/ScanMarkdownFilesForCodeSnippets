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
                Console.WriteLine("Syntax: <program> <rootFolderToScan> [filePattern = *.md] [-onlyTopLevel = no] [-hideMultiLineCodeBlocks = no] [-hideSingleLineCodeBlocks = no]");
                Console.WriteLine("  For example:");
                Console.WriteLine("    ScanMarkDownFiles /sources/myrepo");
                Console.WriteLine("      will scan all /sources/myrepo including subdirs for *.md files and show the contents of both single and multi-line code blocks");
                Console.WriteLine("");
                Console.WriteLine("    ScanMarkDownFiles /sources/myrepo -hideMultiLineCodeBlocks -hideSingleLineCodeBlocks");
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

            string rootFolder = args[0];

            if (!Directory.Exists(rootFolder))
            {
                Console.WriteLine($"Folder '{rootFolder}' does NOT exist!");
                Environment.Exit(1);
            }
            else arguments.RootFolder = rootFolder;

            if (args.Length >= 2) arguments.FilePattern = args[1];

            for (int i = 2; i < args.Length; i++)
            {
                arguments.OnlyToplevel = arguments.OnlyToplevel || args[i].Like("-onlyTopLevel");
                arguments.HideMultiLineCodeBlocks = arguments.HideMultiLineCodeBlocks || args[i].Like("-hideMultiLineCodeBlocks");
                arguments.HideSingleLineCodeBlocks = arguments.HideSingleLineCodeBlocks || args[i].Like("-hideSingleLineCodeBlocks");
            }

            return arguments;
        }
    }
}
