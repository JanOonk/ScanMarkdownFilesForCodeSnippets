using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles
{
    internal class MarkDownFile
    {
        public string Filename { get; set; }
        public List<CodeBlock> CodeBlocks { get; private set; }
        public List<CodeBlock> SingleLineCodeBlocks => CodeBlocks.Where(codeBlock => !codeBlock.MultiLine).ToList();
        public int SingleLineCodeBlocksCount => SingleLineCodeBlocks.Count;
        public List<CodeBlock> MultiLineCodeBlocks => CodeBlocks.Where(codeBlock => codeBlock.MultiLine).ToList();
        public int MultiLineCodeBlocksCount => MultiLineCodeBlocks.Count;
        public int UniqueMultiLineCodeBlockTypes => CountPerMultiLineCodeBlockType.Count();
        public Dictionary<string, int> CountPerMultiLineCodeBlockType { get; } = new Dictionary<string, int>();

        public MarkDownFile(string filename) : this(filename, new List<CodeBlock>())
        {
        }

        public MarkDownFile(string filename, CodeBlock codeBlock) : this(filename, new List<CodeBlock>() { codeBlock })
        {
        }

        public MarkDownFile(string filename, List<CodeBlock> codeBlocks)
        {
            Filename = filename;
            CodeBlocks = codeBlocks.OrderBy(codeBlock => codeBlock.StartLineNr).ToList();
            ProcessCodeBlocks(CodeBlocks);
        }

        public void ProcessCodeBlocks(List<CodeBlock> codeBlocks)
        {
            foreach (CodeBlock codeBlock in codeBlocks)
            {
                ProcessCodeBlock(codeBlock);
            }
        }

        public void ProcessCodeBlock(CodeBlock codeBlock)
        {
            if (!codeBlock.MultiLine) return;

            string codeBlockType = codeBlock.Type;
            if (CountPerMultiLineCodeBlockType.ContainsKey(codeBlockType))
            {
                CountPerMultiLineCodeBlockType[codeBlockType] += 1;
            }
            else
            {
                CountPerMultiLineCodeBlockType.Add(codeBlockType, 1);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Filename}");

            if (SingleLineCodeBlocksCount > 0)
            {
                sb.Append($"\n  {SingleLineCodeBlocksCount} single-line code block(s) found\n");
                for (int i = 0; i < SingleLineCodeBlocks.Count; i++)
                {
                    CodeBlock codeBlock = SingleLineCodeBlocks[i];
                    sb.Append($"{codeBlock.ToString().IndentWithLevel(2)}");

                    if (i < SingleLineCodeBlocks.Count - 1) sb.Append("\n");
                }
            }

            if (MultiLineCodeBlocksCount > 0) { 
                sb.Append($"\n{"".IndentWithLevel(1)}{MultiLineCodeBlocksCount} multi-line code block(s) found:\n");
                for (int i = 0; i < MultiLineCodeBlocks.Count; i++)
                {
                    CodeBlock codeBlock = MultiLineCodeBlocks[i];
                    sb.Append($"{codeBlock.ToString().IndentWithLevel(2)}");

                    if (i < MultiLineCodeBlocks.Count - 1) sb.Append("\n");
                }

                sb.Append($"\n{"".IndentWithLevel(1)}{UniqueMultiLineCodeBlockTypes} unique multi-line code block type(s) found:");

                sb.Append($"\n{"".IndentWithLevel(2)}Number of occurences per type:");
                foreach (var countPerMultiLineCodeBlockType in CountPerMultiLineCodeBlockType)
                {
                    string key = countPerMultiLineCodeBlockType.Key;
                    if (key == "") key = "<no type>";
                    int value = countPerMultiLineCodeBlockType.Value;
                    sb.Append($"\n{"".IndentWithLevel(3)}{key,10}: {value,3}x");
                }
            }

            return sb.ToString();
        }
    }
}
