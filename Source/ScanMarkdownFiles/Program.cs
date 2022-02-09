using CommandLine;
using ScanMarkdownFiles;
using ScanMarkdownFiles.CommandLineArgs;
using ScanMarkdownFiles.Extensions;
using ScanMarkdownFiles.Models;
using System.Diagnostics;

Arguments arguments;

//Hardcoded example arguments for testing/development purposes (will be overwritten when commandline arguments are used)
arguments = new Arguments()
{
    RootFolder = @"c:\temp",
    FilePattern = "*.md",
    IncludeSubFolders = true,
    ReplaceTypes = new string[] { "shell=bash", "yaml=yml" },
    ShowFencedCodeBlocks = true,
    ShowInlineCodeBlocks = true
};

Parser.Default.ParseArguments<Arguments>(args)
    .WithParsed(RunOptions)
    .WithNotParsed(HandleParseError);

void RunOptions(Arguments opts)
{
    //do some extra checks
    if (!Directory.Exists(opts.RootFolder))
    {
        Console.WriteLine($"Folder '{opts.RootFolder}' does NOT exist!");
        Environment.Exit(1);
    }

    if (opts.ReplaceTypes.Any(replaceType => replaceType.Count(c => c == '=') != 1))
    {
        Console.WriteLine($"Error in --replacetype parameter: '{String.Join(" ", opts.ReplaceTypes)}'.\nEach replace type must be formatted like: <searchType>=<replaceType>\nWhen using multiple replaceTypes use space to separate.");
        Environment.Exit(1);
    }
    else arguments = opts;
}

void HandleParseError(IEnumerable<Error> errs)
{
    Environment.Exit(1);
}

Stopwatch stopWatch = Stopwatch.StartNew();

List<string> fileNames = Directory
.GetFiles(arguments.RootFolder, arguments.FilePattern, (arguments.IncludeSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
.ToList();

Console.WriteLine($"Found {fileNames.Count} files matching '{arguments.FilePattern}' in '{arguments.RootFolder}':\n");

List<MarkDownFile> markDownFiles = new List<MarkDownFile>();
foreach (var filename in fileNames)
{
    string contents = File.ReadAllText(filename);

    List<CodeBlock> codeBlocks = MarkDownExtractor.ExtractCodeBlocks(contents);

    MarkDownFile markDownFile = new MarkDownFile(filename.Remove(0, arguments.RootFolder.Length), codeBlocks);
    markDownFiles.Add(markDownFile);

    Console.WriteLine(markDownFile.ToString(arguments.ShowInlineCodeBlocks, arguments.ShowFencedCodeBlocks));
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

if (arguments.ReplaceTypes.Any())
{
    Console.WriteLine("\nReplacing codeblock types:");

    int totalReplacements = 0;
    int totalFilesChanged = 0;
    foreach (string searchReplaceType in arguments.ReplaceTypes)
    {
        string[] types = searchReplaceType.Split('=', StringSplitOptions.None);
        string searchType = types[0].ToLower();
        string replaceType = types[1].ToLower();

        Console.WriteLine($"'{searchType}' with '{replaceType}'".IndentWithLevel(1));

        int totalReplacementsForType = 0;

        for (int i = 0; i < markDownFiles.Count; i++)
        {
            MarkDownFile markDownFile = markDownFiles[i];
            List<FencedCodeBlock> fencedCodeBlocks = markDownFile.FencedCodeBlocks.Where(fencedCodeBlock => fencedCodeBlock.Type == searchType).ToList();

            if (fencedCodeBlocks.Count > 0)
            {
                string fullFilename = arguments.RootFolder + markDownFile.Filename;

                List<string> lines = File.ReadAllLines(fullFilename).ToList();

                foreach (FencedCodeBlock fencedCodeBlock in fencedCodeBlocks)
                {
                    lines[fencedCodeBlock.StartLineNr - 1] = lines[fencedCodeBlock.StartLineNr - 1]
                        .Remove(fencedCodeBlock.StartColumnNr - 1 + 3, fencedCodeBlock.Type.Length)
                        .Insert(fencedCodeBlock.StartColumnNr - 1 + 3, replaceType);

                }

                File.WriteAllLines(fullFilename, lines);

                Console.WriteLine($"{fencedCodeBlocks.Count,4}x in {markDownFile.Filename}".IndentWithLevel(2));

                totalReplacementsForType += fencedCodeBlocks.Count;
                totalFilesChanged++;
            }
        }

        Console.WriteLine($"Total replacements: {totalReplacementsForType}x".IndentWithLevel(2));
        totalReplacements += totalReplacementsForType;
    }

    Console.WriteLine($"Overall total replacements: {totalReplacements}x".IndentWithLevel(1));
    Console.WriteLine($"Total files changed: {totalFilesChanged}x".IndentWithLevel(1));
}

stopWatch.Stop();

Console.WriteLine($"\nFinished in {stopWatch.Elapsed}");
