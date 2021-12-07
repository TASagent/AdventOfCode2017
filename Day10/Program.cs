using System.Text;
using AoCTools;

const string inputFile = @"../../../../input10.txt";

Console.WriteLine("Day 10 - Knot Hash");
Console.WriteLine("Star 1");
Console.WriteLine();

IEnumerable<int> inputLengths = File.ReadAllText(inputFile).Split(',').Select(int.Parse).ToArray();

CircularList list = new CircularList(256);

foreach (int length in inputLengths)
{
    list.Twist(length);
}

Console.WriteLine($"Product of first two positions: {list[0] * list[1]}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

//Load data as ascii
inputLengths = Encoding.UTF8.GetBytes(File.ReadAllText(inputFile))
    .Select(x => (int)x)
    .Concat(new int[] { 17, 31, 73, 47, 23 })
    .ToArray();

//refresh list
list = new CircularList(256);

//Run 64 rounds of hashing
for (int i = 0; i < 64; i++)
{
    foreach (int length in inputLengths)
    {
        list.Twist(length);
    }
}

Console.WriteLine($"Knot hash is: {list.GetDenseHash()}");

Console.WriteLine();
Console.ReadKey();
