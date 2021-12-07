using AoCTools;

Console.WriteLine("Day 03 - Spiral Memory");
Console.WriteLine("Star 1");
Console.WriteLine();

const string inputFile = @"../../../../input03.txt";

int inputValue = int.Parse(File.ReadAllText(inputFile));

int layer = (int)Math.Ceiling((Math.Sqrt(inputValue) - 1) / 2);

int diff = inputValue - (1 + 2 * (layer - 1)) * (1 + 2 * (layer - 1));

diff %= 2 * layer;

int steps;
if (diff >= layer)
{
    //Rising edge
    steps = diff - layer + layer;
}
else
{
    //Falling Edge
    steps = 2 * layer - diff;
}

Console.WriteLine($"{inputValue} is {steps} steps away.");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

//Implementing this for real

Dictionary<Point2D, int> data = new Dictionary<Point2D, int>()
{
    { (0, 0), 1 }
};

int index = 1;

while (true)
{
    if (GetValue(index) > inputValue)
    {
        break;
    }

    index++;
}

Console.WriteLine($"First value written larger than {inputValue} is {GetValue(index)}.");
Console.WriteLine();

Console.ReadKey();

int GetValue(int index)
{
    (int x0, int y0) = GetCoordinate(index);
    if (data.ContainsKey((x0, y0)))
    {
        return data[(x0, y0)];
    }

    int newValue = 0;
    for (int x = x0 - 1; x <= x0 + 1; x++)
    {
        for (int y = y0 - 1; y <= y0 + 1; y++)
        {
            if (data.ContainsKey((x, y)))
            {
                newValue += data[(x, y)];
            }
        }
    }

    data.Add((x0, y0), newValue);

    return newValue;
}


Point2D GetCoordinate(int index)
{
    if (index <= 0)
    {
        throw new Exception();
    }

    if (index == 1)
    {
        return (0, 0);
    }

    int layer = (int)Math.Ceiling((Math.Sqrt(index) - 1) / 2);

    int diff = index - (1 + 2 * (layer - 1)) * (1 + 2 * (layer - 1));

    int edgeLength = 2 * layer;

    //Cycle max back around to 0
    diff %= 4 * edgeLength;

    if (diff < edgeLength)
    {
        //Right
        return (layer, -layer + diff);
    }
    else if (diff < 2 * edgeLength)
    {
        //Top
        diff -= edgeLength;
        return (layer - diff, layer);
    }
    else if (diff < 3 * edgeLength)
    {
        //Left
        diff -= 2 * edgeLength;
        return (-layer, layer - diff);
    }
    else
    {
        //bottom
        diff -= 3 * edgeLength;
        return (-layer + diff, -layer);
    }
}
