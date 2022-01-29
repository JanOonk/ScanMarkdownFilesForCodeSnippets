using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScanMarkdownFiles
{
    public static class MarkDownExtractor
    {
        public static List<CodeBlock> ExtractCodeBlocks(string contents)
        {
            string regEx = "```(.*?)```"; //captures both single and multiline code blocks

            MatchCollection linkMatches;
            linkMatches = Regex.Matches(contents, regEx, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            List<CodeBlock> codeBlocks = new List<CodeBlock>();

            for (int i = 0; i < linkMatches.Count; i++)
            {
                Match match = linkMatches[i];
                List<string> codeBlocksLines = match.Groups[1].Value.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();

                int lineNr = LineNrFromIndex(contents, match.Index);
                CodeBlock codeBlock = new CodeBlock(codeBlocksLines, lineNr);
                codeBlocks.Add(codeBlock);
            }

            return codeBlocks;
        }

        private static int LineNrFromIndex(string input, int index)
        {
            int lineNumber = 1;
            for (int i = 0; i < index; i++)
            {
                if (input[i] == '\n') lineNumber++;
            }
            return lineNumber;
        }
    }
}
