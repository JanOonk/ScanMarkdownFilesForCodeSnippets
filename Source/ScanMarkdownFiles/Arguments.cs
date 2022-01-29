using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles
{
    public class Arguments
    {
        public string RootFolder { get; set; } = "";
        public string FilePattern { get; set; } = "*.md";
        public bool OnlyToplevel { get; set; } = false;
        public bool HideMultiLineCodeBlocks { get; set; } = false;
        public bool HideSingleLineCodeBlocks { get; set; } = false;
    }
}
