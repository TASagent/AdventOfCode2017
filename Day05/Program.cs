const string inputFile = @"../../../../input05.txt";

Console.WriteLine("Day 05 - A Maze of Twisty Trampolines, All Alike");
Console.WriteLine("Star 1");
Console.WriteLine();

List<int> instructions = File.ReadAllLines(inputFile).Select(int.Parse).ToList();

int position = 0;
int steps = 0;

while (position >= 0 && position < instructions.Count)
{
    position += instructions[position]++;
    steps++;
}

Console.WriteLine($"Took {steps} steps to exit.");


Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

//Now, if the jump is 3 or more, decrement it by 1

//Reset instructions
instructions = File.ReadAllLines(inputFile).Select(int.Parse).ToList();

position = 0;
steps = 0;

while (position >= 0 && position < instructions.Count)
{
    if (instructions[position] >= 3)
    {
        position += instructions[position]--;
    }
    else
    {
        position += instructions[position]++;
    }

    steps++;
}


Console.WriteLine($"Took {steps} steps to exit.");

Console.WriteLine();
Console.ReadKey();
