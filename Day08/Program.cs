const string inputFile = @"../../../../input08.txt";

Console.WriteLine("Day 08 - I Heard You Like Registers");
Console.WriteLine("Star 1");
Console.WriteLine();

Computer computer = new Computer();

IEnumerable<Operation> operations = File.ReadAllLines(inputFile).Select(x => new Operation(x)).ToArray();

foreach (Operation operation in operations)
{
    computer.Execute(operation);
}

Console.WriteLine($"The largest register value is: {computer.Registers.Values.Max()}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

Console.WriteLine($"The largest value ever held in a register is: {computer.continuousMax}");

Console.WriteLine();
Console.ReadKey();


enum Comparator
{
    Equal = 0,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    MAX
}

readonly struct Operation
{
    public readonly string register;
    public readonly int value;

    public readonly string testReg;
    public readonly Comparator Comparator;
    public readonly int testValue;

    public Operation(string line)
    {
        string[] splitLine = line.Split(' ');
        register = splitLine[0];
        value = int.Parse(splitLine[2]);

        if (splitLine[1] == "dec")
        {
            value *= -1;
        }

        testReg = splitLine[4];
        testValue = int.Parse(splitLine[6]);

        Comparator = splitLine[5] switch
        {
            "==" => Comparator.Equal,
            "!=" => Comparator.NotEqual,
            ">" => Comparator.GreaterThan,
            ">=" => Comparator.GreaterThanOrEqual,
            "<" => Comparator.LessThan,
            "<=" => Comparator.LessThanOrEqual,
            _ => throw new Exception($"Undefined Operation: {splitLine[5]}"),
        };
    }

    public bool TestCondition(Computer computer)
    {
        switch (Comparator)
        {
            case Comparator.Equal: return computer[testReg] == testValue;
            case Comparator.NotEqual: return computer[testReg] != testValue;
            case Comparator.GreaterThan: return computer[testReg] > testValue;
            case Comparator.GreaterThanOrEqual: return computer[testReg] >= testValue;
            case Comparator.LessThan: return computer[testReg] < testValue;
            case Comparator.LessThanOrEqual: return computer[testReg] <= testValue;

            default: throw new Exception($"Undefined Operation: {Comparator}");
        }
    }

    public int ApplyOperation(Computer computer) => computer[register] += value;

    public override string ToString() => $"IF {testReg,6}{Comparator.Print(),2}{testValue,8}   {register,6} += {value}";
}

class Computer
{
    public int continuousMax = 0;

    public readonly Dictionary<string, int> Registers = new Dictionary<string, int>();

    public Computer() { }

    public int this[string register]
    {
        get => Registers.GetValueOrDefault(register, 0);
        set => Registers[register] = value;
    }

    public void Execute(in Operation operation)
    {
        if (operation.TestCondition(this))
        {
            continuousMax = Math.Max(operation.ApplyOperation(this), continuousMax);
        }
    }

    public override string ToString() =>
        $"[{string.Join(", ", Registers.Select(x => $"{x.Key}={x.Value}"))}]";
}

static class ComparatorExt
{
    public static string Print(this Comparator comparator)
    {
        switch (comparator)
        {
            case Comparator.Equal: return "==";
            case Comparator.NotEqual: return "!=";
            case Comparator.GreaterThan: return ">";
            case Comparator.GreaterThanOrEqual: return ">=";
            case Comparator.LessThan: return "<";
            case Comparator.LessThanOrEqual: return "<=";

            default:
                throw new Exception($"Undefined Operation: {comparator}");
        }
    }
}
