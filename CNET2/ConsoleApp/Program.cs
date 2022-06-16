using Data;
using Microsoft.EntityFrameworkCore;
using Model;

void TestParalelism()
{
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
    Console.WriteLine();

    var ukol1 = numbers.Count(x => x > 0);
    Console.WriteLine($"Ukol 1: {ukol1}");

    // Ukol 2
    var ukol2 = numbers.OrderBy(x => x).Skip(1).SkipLast(1).Average();
    Console.WriteLine($"Ukol 2: {ukol2}");

    // Ukol 3
    var ukol3s = numbers.Where(x => x % 2 == 0).Count();
    var ukol3l = numbers.Where(x => x % 2 != 0).Count();
    Console.WriteLine($"Ukol 3: suda: {ukol3s} licha: {ukol3l}");

    // Ukol 4
    var numbers2 = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
    var strings = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    Console.WriteLine($"Ukol 4: {string.Join(", ", numbers2.Select(x => strings[x]))}");
}

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

// Interface test Interfacu
static void GreetClient(IGreetable client)
{
    Console.WriteLine(client.SayHello());
}

static void GreetClients(List<IGreetable> clients)
{
    foreach (var client in clients)
    {
        Console.WriteLine(client.SayHello());
    }
}

var client1 = new Client
{
    Name = "Pavel",
};

var client2 = new VipClient
{
    Name = "Franta",
    Status = "Super Zákazník",
};

var client3 = new Client
{
    Name = "Petr",
};

//GreetClient(client1);GreetClient(client2);GreetClient(client3);

var seznam = new List<IGreetable>()
{
    client1, client2, client3
};

GreetClients(seznam);


// Test de-serializace
var dataSet = Serialization.LoadFromXML(@"C:\Users\StudentEN\source\repos\Malina\PersonDataset\dataset-utf.xml");

var contractCount = dataSet.Where(x => x.Contracts.Any()).Count();
Console.WriteLine($"Osob se smlouvou: {contractCount}");

var bydliVBrne = dataSet.Where(x => x.HomeAddress.City.ToLowerInvariant() == "brno");
Console.WriteLine($"Osob z Brna: {bydliVBrne.Count()}");
var i = 0;
foreach (var item in bydliVBrne)
{
    i++;
    Console.WriteLine($"{i} {item.FullName}");
}

// Nemladší a nejstarší - jméno a kolik mají let
var nejmladsi = dataSet.OrderByDescending(x => x.DateOfBirth).FirstOrDefault();
var nejstarsi = dataSet.OrderBy(x => x.DateOfBirth).FirstOrDefault();
Console.WriteLine($"Nejmladší: {nejmladsi?.FullName} - {nejmladsi?.Age()}");
Console.WriteLine($"Nejstarší: {nejstarsi?.FullName} - {nejstarsi?.Age()}");

// Vracíme jako anonymní typ
var jmVek = dataSet.Select(x => new { x.FullName, Age = x.Age() });
foreach (var jmV in jmVek)
{
    Console.WriteLine($"{jmV.FullName} - {jmV.Age}");
}

// Vracíme jako tuple
var jmVekTuple = dataSet.Select(x => (x.FullName, Age: x.Age()));

// Group By
Console.WriteLine(Environment.NewLine);
var grByMesto = dataSet.GroupBy(x => x.HomeAddress.City);

foreach (var grm in grByMesto)
{
    Console.WriteLine($"{grm.Key} - {grm.Count()}{Environment.NewLine}");

    foreach (var person in grm)
    {
        Console.WriteLine($"{person.FullName}");
    }

    Console.WriteLine(Environment.NewLine);
}

// SelectMany - "zplosteni" pod kolekce
var xxx = dataSet.SelectMany(x => x.Contracts);
Console.WriteLine($"{xxx.Count()}");

// Aggregate - vraci nase moznost implementace agregacni funkce

// TEST: Kdo uzavřel poslední smlouvu
//var lastSmlouva = dataSet.Select(x => new { x.FullName, Contract = x.Contracts.OrderByDescending(a => a.Signed).FirstOrDefault() }).OrderByDescending(x => x.Contract?.Signed).First();
//Console.WriteLine($"{lastSmlouva.FullName} - {lastSmlouva.Contract?.Number} / {lastSmlouva.Contract?.Signed.ToString("dd.MM.yyyy")}");

var lastSmlouva = dataSet.Where(x => x.Contracts.Any()).OrderByDescending(x => x.Contracts.OrderByDescending(a => a.Signed).First().Signed).First();
Console.WriteLine($"{lastSmlouva.FullName} - {lastSmlouva.Contracts?.First().Number} / {lastSmlouva.Contracts?.First().Signed.ToString("dd.MM.yyyy")}");

// EF Core Example
// Načtení DB kontextu + Načtení datasetu do db

// using se dá použít i bez označení platnosti
// pak se jedná o aktuální closure (fce, ...)
using var db = new PeopleContext();

//db.Persons.AddRange(dataSet);
//db.SaveChanges();

// Pridani noveho radku
var address = new Address
{
    Street = "ulice 123",
    City = "Mestecko",
};

var j = 1;

foreach (var person in db.Persons.Include("Contracts"))
{
    var contract = person.Contracts.FirstOrDefault();

    if (contract == null)
    {
        continue;
    }

    contract.Company = new Company
    {
        Name = $"Testovací company {j++}",
        Address = address,
    };
}

db.SaveChanges();

Console.WriteLine(db.Persons.Count());
Console.WriteLine(db.Addresses.Count());
Console.WriteLine(db.Contracts.Count());


//---------------------------------------------------------------

Console.ReadLine();