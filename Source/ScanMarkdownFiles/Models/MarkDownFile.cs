using ScanMarkdownFiles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles.Models
{
    public class MarkDownFile
    {
        public string Filename { get; set; }
        public List<CodeBlock> CodeBlocks { get; private set; }
        public List<InlineCodeBlock> InlineCodeBlocks => CodeBlocks
            .Where(codeBlock => codeBlock is InlineCodeBlock)
            .Select(codeBlock => (InlineCodeBlock)codeBlock)
            .ToList();
        public int InlineCodeBlocksCount => InlineCodeBlocks.Count;
        public List<FencedCodeBlock> FencedCodeBlocks => CodeBlocks
            .Where(codeBlock => codeBlock is FencedCodeBlock)
            .Select(codeBlock => (FencedCodeBlock)codeBlock)
            .ToList();
        public int FencedCodeBlocksCount => FencedCodeBlocks.Count;
        public int UniqueFencedCodeBlockTypes => CountPerFencedCodeBlockType.Count;
        public Dictionary<string, int> CountPerFencedCodeBlockType { get; } = new Dictionary<string, int>();

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
            if (codeBlock is FencedCodeBlock fencedCodeBlock)
            {
                string codeBlockType = fencedCodeBlock.Type;
                if (CountPerFencedCodeBlockType.ContainsKey(codeBlockType))
                {
                    CountPerFencedCodeBlockType[codeBlockType] += 1;
                }
                else
                {
                    CountPerFencedCodeBlockType.Add(codeBlockType, 1);
                }
            }
        }

        public string ToString(bool showContentsOfInlineCodeBlocks, bool showContentsOfFencedCodeBlocks)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Filename}");

            if (InlineCodeBlocksCount > 0)
            {
                sb.Append($"\n  {InlineCodeBlocksCount} inline code block(s) found\n");

                for (int i = 0; i < InlineCodeBlocks.Count; i++)
                {
                    InlineCodeBlock inlineCodeBlock = InlineCodeBlocks[i];
                    sb.Append($"{inlineCodeBlock.ToString(showContentsOfInlineCodeBlocks, showContentsOfFencedCodeBlocks).IndentWithLevel(2)}");

                    if (i < InlineCodeBlocks.Count - 1) sb.Append('\n');
                }
            }

            if (FencedCodeBlocksCount > 0)
            {
                sb.Append($"\n{"".IndentWithLevel(1)}{FencedCodeBlocksCount} fenced code block(s) found:\n");

                for (int i = 0; i < FencedCodeBlocks.Count; i++)
                {
                    FencedCodeBlock fencedCodeBlock = FencedCodeBlocks[i];
                    sb.Append($"{fencedCodeBlock.ToString(showContentsOfInlineCodeBlocks, showContentsOfFencedCodeBlocks).IndentWithLevel(2)}");

                    if (i < FencedCodeBlocks.Count - 1) sb.Append('\n');
                }

                sb.Append($"\n{"".IndentWithLevel(1)}{UniqueFencedCodeBlockTypes} unique fenced code block type(s) found:");
                sb.Append($"\n{"".IndentWithLevel(2)}Number of occurences per type:");

                foreach (var countPerFencedCodeBlockType in CountPerFencedCodeBlockType)
                {
                    string key = countPerFencedCodeBlockType.Key;
                    if (key == "") key = "<no type>";
                    int value = countPerFencedCodeBlockType.Value;
                    sb.Append($"\n{"".IndentWithLevel(3)}{key,10}: {value,3}x");
                }
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString(true, true);
        }
    }
}
