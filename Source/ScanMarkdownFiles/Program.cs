using ScanMarkdownFiles;

Arguments arguments;

//use command line for arguments
arguments = ParseArguments(args);

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

Console.WriteLine($"Found {fileNames.Count()} files matching {arguments.FilePattern} in {arguments.RootFolder}");

List<MarkDownFile> markDownFiles = new List<MarkDownFile>();
foreach (var filename in fileNames)
{
    List<string> lines = File.ReadAllLines(filename).ToList();

    int count = lines.Count(line => line.Contains("```"));
    List<string> codeSnippets = lines
        .Select(line => line.Trim())
        .Where(line => line.StartsWith("```"))
        .ToList();

    MarkDownFile markDownFile = new MarkDownFile(filename.Remove(0, arguments.RootFolder.Length), codeSnippets);
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

Console.WriteLine("\nOverall stats:");
Console.WriteLine($" Unique codesnippet types found: {stats.Count}");
string statsAsString = " " + String.Join("\n  ", stats
    .Select(stat => $"{stat.TotalCount,4}x found in {stat.NumFilesOccurences,3} file(s): {stat.CodeSnippetType}"));

Console.WriteLine($" {statsAsString}");

Arguments ParseArguments(string[] args)
{
    if (args.Length == 0)
    {
        Console.WriteLine("Error in arguments!");
        Console.WriteLine("Syntax: <program> <rootFolderToScan> [filePattern = *.md] [-onlyTopLevel = no]");
        Console.WriteLine("  For example:");
        Console.WriteLine("    ScanMarkDownFiles /sources/myrepo");
        Console.WriteLine("      will scan all /sources/myrepo including subdirs for *.md files");
        Console.WriteLine("");
        Console.WriteLine("    ScanMarkDownFiles /sources/myrepo *.mdwn");
        Console.WriteLine("      will scan all /sources/myrepo including subdirs for *.mdwn files");
        Console.WriteLine("");
        Console.WriteLine("    ScanMarkDownFiles /sources/myrepo *.md -onlyTopLevel");
        Console.WriteLine("      will only scan the top level directory /sources/myrepo for *.md files");
        Environment.Exit(1);
    }

    Arguments arguments = new Arguments();

    arguments.RootFolder = args[0];

    if (args.Length >= 2) arguments.FilePattern = args[1];
    if (args.Length == 3) arguments.OnlyToplevel = (args[2].ToLower().Trim() == "-onlytoplevel");

     return arguments;
}