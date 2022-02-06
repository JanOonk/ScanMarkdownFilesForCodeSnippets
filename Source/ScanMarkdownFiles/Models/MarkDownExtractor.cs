using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using ScanMarkdownFiles.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScanMarkdownFiles.Models
{
    public static class MarkDownExtractor
    {
        private static readonly MarkdownPipeline? pipeline = new MarkdownPipelineBuilder()
               .UseAdvancedExtensions()
               .UsePreciseSourceLocation()
               .Build();

        public static List<CodeBlock> ExtractCodeBlocks(string contents)
        {
            List<CodeBlock> codeBlocks = new List<CodeBlock>();

            MarkdownDocument? parsed = Markdown.Parse(contents, pipeline);

            string[] delimiters = new string[] { "~", "`" };

            foreach (Block? item in parsed.ToList())
            {
                if (item is ParagraphBlock paragraphBlock)
                {
                    Inline? inline = paragraphBlock.Inline?.FirstChild;
                    while (inline != null)
                    {
                        //Debug.WriteLine(inline.GetType());
                        if (inline is CodeInline codeInline)
                        {
                            if (delimiters.Contains(codeInline.Delimiter.ToString()) && codeInline.DelimiterCount == 3)
                            {
                                InlineCodeBlock inlineCodeBlock = new InlineCodeBlock(codeInline.Content, codeInline.Line + 1);
                                Debug.WriteLine(inlineCodeBlock.ToString(false, false));
                                codeBlocks.Add(inlineCodeBlock);
                                //Debug.WriteLine($"Inline code block found on line {codeInline.Line} at column {codeInline.Column}:\n {codeInline.Content}");
                            }
                        }
                        inline = inline.NextSibling;
                    }
                }

                if (item is Markdig.Syntax.FencedCodeBlock fencedCodeBlock)
                {
                    if (delimiters.Contains(fencedCodeBlock.FencedChar.ToString()) && fencedCodeBlock.OpeningFencedCharCount == 3 && fencedCodeBlock.ClosingFencedCharCount == 3)
                    {
                        string fcbContentsAsString = fencedCodeBlock.Lines.ToString();
                        string[] newLineDelimiters = new string[] { "\r\n", "\n" };
                        List<string> fcbContents = fcbContentsAsString.Split(newLineDelimiters, StringSplitOptions.None).ToList();
                        Models.FencedCodeBlock myFencedCodeBlock = new Models.FencedCodeBlock(fcbContents, fencedCodeBlock.Line + 1, fencedCodeBlock.Info ?? "", fencedCodeBlock.Arguments ?? "");
                        Debug.WriteLine(myFencedCodeBlock.ToString(false, false));
                        codeBlocks.Add(myFencedCodeBlock);
                        //Debug.WriteLine($"Fenced code block with type '{fencedCodeBlock.Info}{arguments}' found on line {fencedCodeBlock.Line} at column {fencedCodeBlock.Column}:\n {fencedCodeBlock.Lines.ToString()}");
                    }
                }
            }

            return codeBlocks;
        }
    }
}
