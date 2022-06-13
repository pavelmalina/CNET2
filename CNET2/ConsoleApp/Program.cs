using Data;
using Model;

var fAResult = new FAResult
{
    Source = "file",
    SourceType = SourceType.File,
};

var dir = @"C:\Users\StudentEN\source\repos\Malina\Books\";
var files = Directory.GetFiles(dir, "*.txt");

var results = new List<FAResult>();

foreach (var fileName in files)
{
    var newItem = FreqAnalysis.FreqAnalysisFromFile(fileName);

    newItem.PrintTenMostFrequentedWords();

    results.Add(newItem);
}


Console.ReadLine();