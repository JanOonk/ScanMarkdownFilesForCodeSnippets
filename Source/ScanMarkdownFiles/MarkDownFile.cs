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
        public int CodeSnippetsCount => CountPerCodeSnippet.Sum(codeSnippet => codeSnippet.Value) / 2;
        public int EmptyCodeSnippetsCount => CodeSnippetsCount - CountPerCodeSnippet
            .Where(codeSnippet => codeSnippet.Key != "```")
            .Sum(codeSnippet => codeSnippet.Value);
        public List<string> CodeSnippetTypes
        {
            get
            {
                List<string> result = CountPerCodeSnippet.Keys.ToList();
                result.Sort();
                return result;
            }
        }

        public List<string> NonEmptyCodeSnippetTypes
        {
            get
            {
                List<string> result = CountPerCodeSnippet
                    .Keys
                    .Where(key => key != "```")
                    .ToList();
                result.Sort();
                return result;
            }
        }

        public int CodeSnippetTypesCount => CodeSnippetTypes.Count();
        //a Markdown codeSnippet starts with:
        // ```
        // ```json
        // ```console
        // ```shell
        //
        // and always ends with ```

        //Number of occurences for each codeSnippet type
        public Dictionary<string, int> CountPerCodeSnippet { get; } = new Dictionary<string, int>();

        public MarkDownFile(string filename)
        {
            Filename = filename;
        }

        public MarkDownFile(string filename, string codeSnippet)
        {
            Filename = filename;
            AddCodeSnippet(codeSnippet);
        }

        public MarkDownFile(string filename, List<string> codeSnippets)
        {
            Filename = filename;
            AddCodeSnippets(codeSnippets);
        }

        public void AddCodeSnippets(List<string> codeSnippets)
        {
            foreach (string codeSnippet in codeSnippets)
            {
                AddCodeSnippet(codeSnippet);
            }
        }

        public void AddCodeSnippet(string codeSnippet)
        {
            codeSnippet = codeSnippet.ToLower();
            if (CountPerCodeSnippet.ContainsKey(codeSnippet))
            {
                CountPerCodeSnippet[codeSnippet] += 1;
            }
            else
            {
                CountPerCodeSnippet.Add(codeSnippet, 1);
            }
        }

        public bool Validate()
        {
            return true;// CodeSnippetsCount != count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Filename}: {CodeSnippetsCount} codesnippets found");
            if (CodeSnippetsCount > 0)
            {
                if (EmptyCodeSnippetsCount == 0)
                {
                    sb.Append($"\n  {CodeSnippetTypesCount-1} Unique Types: {String.Join(", ", NonEmptyCodeSnippetTypes)}");
                }
                else
                {
                    sb.Append($"\n  {CodeSnippetTypesCount} Unique Types: {String.Join(", ", CodeSnippetTypes)}");
                }
                foreach (string codeSnippetType in CodeSnippetTypes)
                {
                    string key = codeSnippetType;
                    int value;
                    if (key == "```")
                    {
                        value = EmptyCodeSnippetsCount;
                        if (value == 0) continue;
                    }
                    else value = CountPerCodeSnippet[key];
                    sb.Append($"\n  {key}: {value}x");
                }
            }
            return sb.ToString();
        }
    }
}
