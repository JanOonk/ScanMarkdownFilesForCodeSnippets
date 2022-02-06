﻿using ScanMarkdownFiles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles.Models
{
    public abstract class CodeBlock
    {
        public int StartLineNr { get; protected set; }
        public int EndLineNr { get; protected set; }
        public int NrOfLines => EndLineNr - StartLineNr + 1;
        public string Contents { get; protected set; } = "";
    }

    public class InlineCodeBlock : CodeBlock
    {
        public InlineCodeBlock(string contents, int startLineNr)
        {
            Contents = contents;
            StartLineNr = startLineNr;
            EndLineNr = StartLineNr;
        }

        public string ToString(bool hideInlineCodeBlocks, bool hideFencedCodeBlocks)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"Inline code block on line {StartLineNr}");

            if (!hideInlineCodeBlocks)
            {
                stringBuilder.Append($" with contents: {Contents}");
            }

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return ToString(true, true);
        }
    }


    public class FencedCodeBlock : CodeBlock
    {
        public string Type { get; private set; } = "";
        public string Arguments { get; private set; } = "";

        public FencedCodeBlock(List<string> lines, int startLineNr, string type, string arguments)
        {
            Contents = String.Join(Environment.NewLine, lines);
            StartLineNr = startLineNr;
            EndLineNr = startLineNr + lines.Count - 1 + 2;
            Type = type.ToLower();
            Arguments = arguments;
        }

        public string ToString(bool hideInlineCodeBlocks, bool hideFencedCodeBlocks)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"Fenced code block on lines {StartLineNr}-{EndLineNr} with type '{(Type == "" ? "<no type>" : Type)}'");

            if (Arguments.Length > 0) stringBuilder.Append($" with argument(s) '{Arguments}'");

            if (!hideFencedCodeBlocks)
            {
                stringBuilder.Append($"\n{"".IndentWithLevel(1)}Contents ({NrOfLines - 2} line{(NrOfLines - 2 > 1 ? "s" : "")}):\n");
                stringBuilder.Append($"{Contents.IndentWithLevel(2)}");
            }

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return ToString(true, true);
        }
    }
}
