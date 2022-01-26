using ScanMarkdownFiles;

//string root = "/temp/documentation";
//string root = "/temp/goshimmer-docs-update-docs-annotations/documentation";
//string root = "/temp/goshimmer-docs-update-docs-annotations";
string root = "/temp/goshimmer-docs-update-docs-annotations v2";
//string pattern = "send_tr*.md";
string pattern = "*.md";
List<string> fileNames = Directory
    .GetFiles(root, pattern, SearchOption.AllDirectories)
    .ToList();

Console.WriteLine($"Found {fileNames.Count()} files matching {pattern} in {root}");

List<MarkDownFile> markDownFiles = new List<MarkDownFile>();
foreach (var filename in fileNames)
{
    List<string> lines = File.ReadAllLines(filename).ToList();

    int count = lines.Count(line => line.Contains("```"));
    List<string> codeSnippets = lines
        .Select(line => line.Trim())
        .Where(line => line.StartsWith("```"))
        .ToList();

    MarkDownFile markDownFile = new MarkDownFile(filename.Remove(0, root.Length), codeSnippets);
    Console.WriteLine(markDownFile);

    if (markDownFile.CodeSnippetsCount * 2 != count)
    {
        throw new Exception("One or more lines having ``` which are NOT at the beginning of the line!");
    }

    markDownFile.Validate();

    markDownFiles.Add(markDownFile);
}

var stats = markDownFiles
    .SelectMany(markDownFile => markDownFile.CountPerCodeSnippet)
    .GroupBy(countPerCodeSnippet => countPerCodeSnippet.Key)
    .Select(codeSnippet => new
    {
        CodeSnippetType = codeSnippet.Key,
        TotalCount = codeSnippet.Sum(count => count.Value),
        NumFilesOccurences = codeSnippet.Count(x => x.Value > 0)
    })
    .ToList();

Console.WriteLine("Overall stats:");
Console.WriteLine($" Unique codesnippet types found: {stats.Count}");
string statsAsString = " " + String.Join("\n  ", stats
    .Select(stat => $"{stat.TotalCount, 4}x found in {stat.NumFilesOccurences, 3} file(s): {stat.CodeSnippetType}"));

Console.WriteLine($" {statsAsString}");

