// Todo:
// - add tests

using ScanMarkdownFiles;

Arguments arguments;

//use command line for arguments
arguments = ArgumentsParser.Parse(args);

//test arguments
//arguments = new Arguments()
//{
//    RootFolder = "/temp/goshimmer-docs-update-docs-annotations v2",
//    FilePattern = "*.md",
//    OnlyToplevel = false
//};

List<string> fileNames = Directory
.GetFiles(arguments.RootFolder, arguments.FilePattern, (arguments.OnlyToplevel ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories))
.ToList();

Console.WriteLine($"Found {fileNames.Count} files matching '{arguments.FilePattern}' in '{arguments.RootFolder}':\n");

List<MarkDownFile> markDownFiles = new List<MarkDownFile>();
foreach (var filename in fileNames)
{
    string contents = File.ReadAllText(filename);
    
    List<CodeBlock> codeBlocks = MarkDownExtractor.ExtractCodeBlocks(contents);

    MarkDownFile markDownFile = new MarkDownFile(filename.Remove(0, arguments.RootFolder.Length) , codeBlocks);
    markDownFiles.Add(markDownFile);

    Console.WriteLine(markDownFile);
}

var statsMultiLineCodeBlocks = markDownFiles
    .SelectMany(markDownFile => markDownFile.CountPerMultiLineCodeBlockType)
    .GroupBy(countPerCodeSnippet => countPerCodeSnippet.Key)
    .Select(codeSnippet => new
    {
        CodeSnippetType = codeSnippet.Key,
        TotalCount = codeSnippet.Sum(count => count.Value),
        NumFilesOccurences = codeSnippet.Count(x => x.Value > 0)
    })
    .ToList();

var totalSingleLineCodeBlocks = markDownFiles.Sum(markDownFile => markDownFile.SingleLineCodeBlocksCount);
var totalMultiLineCodeBlocks = markDownFiles.Sum(markDownFile => markDownFile.MultiLineCodeBlocksCount);

var totalFilesWithSingleLineCodeBlocks = markDownFiles.Count(markDownFile => markDownFile.SingleLineCodeBlocks.Count() > 0);
var totalFilesWithMultiLineCodeBlocks = markDownFiles.Count(markDownFile => markDownFile.MultiLineCodeBlocks.Count() > 0);

Console.WriteLine("\nOverall stats:");
Console.WriteLine($"  {totalSingleLineCodeBlocks,4} single code block type{(totalSingleLineCodeBlocks > 1 ? "s" : "")} found in {totalFilesWithSingleLineCodeBlocks} file{(totalFilesWithSingleLineCodeBlocks > 1 ? "s" : "")}");
Console.WriteLine($"  {totalMultiLineCodeBlocks,4} multi-line code block type{(totalMultiLineCodeBlocks > 1 ? "s" : "")} found in {totalFilesWithMultiLineCodeBlocks} file{(totalFilesWithMultiLineCodeBlocks > 1 ? "s" : "")}:");
Console.WriteLine($"    {statsMultiLineCodeBlocks.Count,4} unique (multi-line) code block type{(statsMultiLineCodeBlocks.Count > 1 ? "s" : "")} found:");
string statsAsString = String.Join("\n      ", statsMultiLineCodeBlocks
    .Select(stat => $"{stat.TotalCount,4}x found in {stat.NumFilesOccurences,3} file(s): {(stat.CodeSnippetType != "" ? stat.CodeSnippetType : "<no type>")}"));

Console.WriteLine($"      {statsAsString}");
