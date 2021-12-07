const string inputFile = @"../../../../input04.txt";

Console.WriteLine("Day 04 - High-Entropy Passphrases");
Console.WriteLine("Star 1");
Console.WriteLine();

IEnumerable<string> passphrases = File.ReadAllLines(inputFile);

IEnumerable<string> validPhrases = passphrases
    .Where(x => x.Split(' ').Length == x.Split(' ').Distinct().Count());

Console.WriteLine($"Valid passphrases: {validPhrases.Count()}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

//No valid anagrams allowed now.  Just sort:

IEnumerable<string> validPhrases2 = passphrases
    .Where(x => x.Split(' ').Length == x.Split(' ').Select(w => new string(w.OrderBy(c => c).ToArray())).Distinct().Count());

Console.WriteLine($"Valid passphrases: {validPhrases2.Count()}");

Console.WriteLine();
Console.ReadKey();
