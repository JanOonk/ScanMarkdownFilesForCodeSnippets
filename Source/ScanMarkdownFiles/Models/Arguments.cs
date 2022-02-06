using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles.Models
{
    public class Arguments
    {
        public string RootFolder { get; set; } = "";
        public string FilePattern { get; set; } = "*.md";
        public bool OnlyToplevel { get; set; } = false;
        public bool HideFencedCodeBlocks { get; set; } = false;
        public bool HideInlineCodeBlocks { get; set; } = false;
    }
}
