using Data;
using Model;


int[] numbers = { 11, 2, 13, 44, -5, 6, 127, -99, 0, 256 };

var result = numbers.Where(x => x > 0);

printNuAr(result);

var soucet = numbers.Sum();

Console.WriteLine(soucet);

// TAKE, SKIP,   TakeWhile,  SkipWhile

// Select
var absValue = numbers.Select(x => Math.Abs(x));
printNuAr(absValue);

// Ukol 1
var ukol1 = numbers.Count(x => x > 0);
Console.WriteLine(ukol1);

// Ukol 2
var ukol2 = numbers.OrderBy(x => x).Skip(1).SkipLast(1).Average();
Console.WriteLine("Ukol2:");
Console.WriteLine(ukol2);



void printNuAr(IEnumerable<int> numbers)
{
    var str = string.Join(", ", numbers);
    Console.WriteLine(str);
}

static void FreqWords()
{
    var dir = @"C:\Users\StudentEN\source\repos\Malina\Books\";
    var files = Directory.GetFiles(dir, "*.txt");

    var results = new List<FAResult>();

    foreach (var fileName in files)
    {
        var newItem = FreqAnalysis.FreqAnalysisFromFile(fileName);

        newItem.PrintTenMostFrequentedWordsToConsole();

        results.Add(newItem);
    }
}

Console.ReadLine();
