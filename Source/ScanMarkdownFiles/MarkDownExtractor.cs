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
            List<CodeBlock> codeBlocks = new List<CodeBlock>();

            string[] codeBlockSyntaxes = new string[] { "```", "~~~" };

            foreach (string codeBlockSyntax in codeBlockSyntaxes)
            {
                string regEx = @$"^(?:\s*?)(?:{codeBlockSyntax})(.*?)(?:{codeBlockSyntax})(?:\s*)$"; //captures both single and multiline code blocks

                MatchCollection linkMatches;
                linkMatches = Regex.Matches(contents, regEx, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);

                for (int i = 0; i < linkMatches.Count; i++)
                {
                    Match match = linkMatches[i];
                    string matchValue = match.Groups[1].Value;

                    List<string> codeBlockLines = matchValue.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();

                    int startLineNr = LineNrFromIndex(contents, match.Index);
                    CodeBlock codeBlock = new CodeBlock(codeBlockLines, startLineNr);
                    codeBlocks.Add(codeBlock);
                }
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
