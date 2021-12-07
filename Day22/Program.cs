using AoCTools;

const string inputFile = @"../../../../input22.txt";


Console.WriteLine("Day 22 - Sporifica Virus");
Console.WriteLine("Star 1");
Console.WriteLine();


Dictionary<LongPoint2D, bool> grid = new Dictionary<LongPoint2D, bool>();
Dictionary<LongPoint2D, int> grid2 = new Dictionary<LongPoint2D, int>();

string[] lines = File.ReadAllLines(inputFile);

int offset = lines.Length / -2;

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        grid.Add(new LongPoint2D(x + offset, y + offset), lines[y][x] == '#');
        grid2.Add(new LongPoint2D(x + offset, y + offset), lines[y][x] == '#' ? 2 : 0);
    }
}

LongPoint2D position = new LongPoint2D(0, 0);
LongPoint2D facing = new LongPoint2D(0, -1);

int infectionSteps = 0;

for (int step = 0; step < 10000; step++)
{
    if (GetPosition(position))
    {
        facing = facing.Right();
    }
    else
    {
        infectionSteps++;
        facing = facing.Left();
    }

    grid[position] = !grid[position];

    position += facing;

    //Dump(position);
}

Console.WriteLine($"Infection Steps: {infectionSteps}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

position = new LongPoint2D(0, 0);
facing = new LongPoint2D(0, -1);

infectionSteps = 0;

for (int step = 0; step < 10_000_000; step++)
{
    switch (GetPosition2(position))
    {
        case 0:
            facing = facing.Left();
            break;

        case 1:
            infectionSteps++;
            break;

        case 2:
            facing = facing.Right();
            break;

        case 3:
            facing = new LongPoint2D(-facing.x, -facing.y);
            break;
    }


    grid2[position] = (grid2[position] + 1) % 4;

    position += facing;
}

Console.WriteLine($"Infection Steps: {infectionSteps}");

Console.WriteLine();
Console.ReadKey();


void Dump(in LongPoint2D position)
{
    Console.WriteLine();
    Console.WriteLine();
    string line = "";
    for (int y = -10; y < 10; y++)
    {
        for (int x = -10; x < 10; x++)
        {
            if (GetPosition(new LongPoint2D(x, y)))
            {
                line += "#";
            }
            else
            {
                line += ".";
            }

            if (position == new LongPoint2D(x, y))
            {
                line += "]";
            }
            else if (position == new LongPoint2D(x + 1, y))
            {
                line += "[";
            }
            else
            {
                line += " ";
            }
        }
        line += "\n";
    }
    Console.WriteLine(line);
    Console.WriteLine();
    Console.WriteLine();
}

bool GetPosition(in LongPoint2D position)
{
    if (!grid.ContainsKey(position))
    {
        grid.Add(position, false);
    }

    return grid[position];
}

int GetPosition2(in LongPoint2D position)
{
    if (!grid2.ContainsKey(position))
    {
        grid2.Add(position, 0);
    }

    return grid2[position];
}

static class Day22Extensions
{
    public static LongPoint2D Right(in this LongPoint2D value) => new LongPoint2D(-value.y, value.x);
    public static LongPoint2D Left(in this LongPoint2D value) => new LongPoint2D(value.y, -value.x);
}
