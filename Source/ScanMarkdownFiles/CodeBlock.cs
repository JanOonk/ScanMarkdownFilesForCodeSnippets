using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles
{
    public class CodeBlock
    {
        public int StartLineNr { get; private set; }
        public int EndLineNr { get; private set; }
        public bool MultiLine { get; private set; }
        public string Type { get; private set; }
        public string Contents { get; private set; }
        public List<string> ContentLines { get; private set; }

        public CodeBlock(List<string> lines, int startLineNr)
        {
            StartLineNr = startLineNr;
            EndLineNr = startLineNr + lines.Count - 1;

            if (lines.Count() == 1)
            {
                Contents = lines[0];
                MultiLine = false;
                Type = "```";
                ContentLines = lines;
            }
            else
            {
                MultiLine = true;
                Type = lines[0].Trim().ToLower();

                ContentLines = lines.Skip(1).SkipLast(1).ToList();
                Contents = String.Join($"{Environment.NewLine}", ContentLines);
            }
        }

        public string ToString(bool hideSingleLineCodeBlocks, bool hideMultiLineCodeBlocks)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (!MultiLine)
            {
                stringBuilder.Append($"Single line code block on line {StartLineNr}");

                if (!hideSingleLineCodeBlocks)
                {
                    stringBuilder.Append($"  with contents: {Contents}");
                }
            }
            else
            {
                stringBuilder.Append($"Multiline code block on lines {StartLineNr}-{EndLineNr} with type '{(Type == "" ? "<no type>" : Type)}'");

                if (!hideMultiLineCodeBlocks)
                {
                    stringBuilder.Append($"\n{"".IndentWithLevel(1)}Contents ({ContentLines.Count} line{(ContentLines.Count > 1 ? "s" : "")}):\n");
                    stringBuilder.Append($"{ContentLines.MakeIndentedStringWithLevel(2)}");
                }
            }

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return ToString(true, true);
        }
    }
}
