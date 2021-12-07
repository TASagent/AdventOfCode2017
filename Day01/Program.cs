Console.WriteLine("Day 1 - Inverse Captcha");
Console.WriteLine("Star 1");
Console.WriteLine();

const string inputFile = @"../../../../input01.txt";

IEnumerable<int> originalValues = File.ReadAllText(inputFile).Select(x => int.Parse($"{x}")).ToArray();
IEnumerable<int> values = originalValues.Append(originalValues.First());

int repeatSum = values.Zip(values.Skip(1), (x, y) => x == y ? x : 0).Sum();

Console.WriteLine($"The sum of repeated values: {repeatSum}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

int halflength = originalValues.Count() / 2;

IEnumerable<int> halfWayValues = originalValues.Skip(halflength).Concat(originalValues.Take(halflength));

int halfrepeatSum = originalValues.Zip(halfWayValues, (x, y) => x == y ? x : 0).Sum();

Console.WriteLine($"The sum of half-way-around repeated value: {halfrepeatSum}");
Console.WriteLine();
Console.ReadKey();