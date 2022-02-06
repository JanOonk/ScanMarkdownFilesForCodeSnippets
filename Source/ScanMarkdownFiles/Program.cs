using ScanMarkdownFiles;
using ScanMarkdownFiles.Models;

Arguments arguments;

//use command line for arguments
arguments = ArgumentsParser.Parse(args);

//test purposes: hardcoded arguments
//arguments = new Arguments()
//{
//    RootFolder = @"C:\temp\docs-develop v3",
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

    MarkDownFile markDownFile = new MarkDownFile(filename.Remove(0, arguments.RootFolder.Length), codeBlocks);
    markDownFiles.Add(markDownFile);

    Console.WriteLine(markDownFile.ToString(arguments.HideInlineCodeBlocks, arguments.HideFencedCodeBlocks));
}

var statsFencedCodeBlocks = markDownFiles
    .SelectMany(markDownFile => markDownFile.CountPerFencedCodeBlockType)
    .GroupBy(countPerFencedCodeBlockType => countPerFencedCodeBlockType.Key)
    .Select(fencedCodeBlock => new
    {
        FencedCodeBlockType = fencedCodeBlock.Key,
        TotalCount = fencedCodeBlock.Sum(count => count.Value),
        NumFilesOccurences = fencedCodeBlock.Count(x => x.Value > 0)
    })
    .ToList();

var totalInlineCodeBlocks = markDownFiles.Sum(markDownFile => markDownFile.InlineCodeBlocksCount);
var totalFencedCodeBlocks = markDownFiles.Sum(markDownFile => markDownFile.FencedCodeBlocksCount);

var totalFilesWithInlineCodeBlocks = markDownFiles.Count(markDownFile => markDownFile.InlineCodeBlocks.Count > 0);
var totalFilesWithFencedCodeBlocks = markDownFiles.Count(markDownFile => markDownFile.FencedCodeBlocks.Count > 0);

Console.WriteLine("\nOverall stats:");
Console.WriteLine($"  {totalInlineCodeBlocks,4} inline code block type{(totalInlineCodeBlocks > 1 ? "s" : "")} found in {totalFilesWithInlineCodeBlocks} file{(totalFilesWithInlineCodeBlocks > 1 ? "s" : "")}");
Console.WriteLine($"  {totalFencedCodeBlocks,4} fenced code block type{(totalFencedCodeBlocks > 1 ? "s" : "")} found in {totalFilesWithFencedCodeBlocks} file{(totalFilesWithFencedCodeBlocks > 1 ? "s" : "")}:");
Console.WriteLine($"    {statsFencedCodeBlocks.Count,4} unique fenced code block type{(statsFencedCodeBlocks.Count > 1 ? "s" : "")} found:");
string statsAsString = String.Join("\n      ", statsFencedCodeBlocks
    .OrderBy(statsFencedCodeBlock => statsFencedCodeBlock.FencedCodeBlockType)
    .Select(stat => $"{stat.TotalCount,4}x found in {stat.NumFilesOccurences,3} file(s): {(stat.FencedCodeBlockType != "" ? stat.FencedCodeBlockType : "<no type>")}"));

Console.WriteLine($"      {statsAsString}");
