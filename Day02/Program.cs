const string inputFile = @"../../../../input02.txt";


Console.WriteLine("Day 02 - Corruption Checksum");
Console.WriteLine("Star 1");
Console.WriteLine();

IEnumerable<IEnumerable<int>> spreadsheet = File.ReadAllLines(inputFile).Select(x => x.Split("\t").Select(int.Parse));

Console.WriteLine($"Checksum: {spreadsheet.Select(x => x.Max() - x.Min()).Sum()}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

Console.WriteLine($"Divisible Number Sum: {spreadsheet.Select(FindDivisible).Sum()}");

Console.WriteLine();
Console.ReadKey();

static int FindDivisible(IEnumerable<int> row)
{
    int count = row.Count();
    var sorted = row.OrderByDescending(x => x);

    for (int index = 0; index < count - 1; index++)
    {
        int baseValue = sorted.Skip(index).First();

        foreach (int divisor in sorted.Skip(index + 1))
        {
            if (baseValue % divisor == 0)
            {
                return baseValue / divisor;
            }
        }
    }

    throw new Exception();
}
