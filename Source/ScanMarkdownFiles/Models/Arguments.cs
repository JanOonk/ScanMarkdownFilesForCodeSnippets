using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMarkdownFiles.CommandLineArgs
{
	class Arguments
	{
		[Option(Required = true, HelpText = "Rootfolder to scan")]
		public string RootFolder { get; set; } = "";

		[Option(Default = "*.md", Required = false, HelpText = "Files to scan")]
		public string FilePattern { get; set; } = "*.md";

		[Option(Default = true, Required = false, HelpText = "Scan also subfolders of rootfolder")]
		public bool IncludeSubFolders { get; set; }

		[Option(Default = true, Required = false, HelpText = "Show inline code blocks in output")] 
		public bool ShowInlineCodeBlocks { get; set; }

		[Option(Default = true, Required = false, HelpText = "Show fenced code blocks in output")]
		public bool ShowFencedCodeBlocks { get; set; }

		[Option('r', "replacetype", Required = false, HelpText = "Replace one or more codeblock type with another type for example:\nreplace 'bash' and 'sh' with 'shell': -r bash=shell sh=shell")]
		public IEnumerable<string> ReplaceTypes { get; set; } = new List<string>();
	}
}
