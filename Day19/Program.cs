using AoCTools;

const string inputFile = @"../../../../input19.txt";


Console.WriteLine("Day 19 - A Series of Tubes");
Console.WriteLine("Star 1");
Console.WriteLine();


string[] lines = File.ReadAllLines(inputFile);
int W = lines[0].Length;
int H = lines.Length;

//Find the entryPoint
Point2D position = new Point2D(
    x: lines[0].IndexOf("|"),
    y: -1);

Point2D boundsUL = (0, 0);
Point2D boundsLR = (W, H);

Dictionary<Point2D, char> grid = new Dictionary<Point2D, char>();

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        grid[(x, y)] = lines[y][x];
    }
}

Point2D velocity = (0, 1);

string collected = "";
int steps = 0;
bool running = true;

while (running)
{
    //Move
    position += velocity;

    steps++;

    if (!position.IsWithin(boundsUL, boundsLR))
    {
        //We're done
        break;
    }

    switch (grid[position])
    {
        case ' ':
            //Fell off tracks
            steps--;
            running = false;
            break;

        case '+':
            //Figure out what to do
            if (velocity.y != 1 &&
                grid.ContainsKey(position - Point2D.YAxis) &&
                grid[position - Point2D.YAxis] != ' ' &&
                grid[position - Point2D.YAxis] != '-')
            {
                velocity = -Point2D.YAxis;
                break;
            }

            if (velocity.y != -1 &&
                grid.ContainsKey(position + Point2D.YAxis) &&
                grid[position + Point2D.YAxis] != ' ' &&
                grid[position + Point2D.YAxis] != '-')
            {
                velocity = Point2D.YAxis;
                break;
            }

            if (velocity.x != 1 &&
                grid.ContainsKey(position - Point2D.XAxis) &&
                grid[position - Point2D.XAxis] != ' ' &&
                grid[position - Point2D.XAxis] != '|')
            {
                velocity = -Point2D.XAxis;
                break;
            }

            if (velocity.x != -1 &&
                grid.ContainsKey(position + Point2D.XAxis) &&
                grid[position + Point2D.XAxis] != ' ' &&
                grid[position + Point2D.XAxis] != '|')
            {
                velocity = Point2D.XAxis;
                break;
            }

            throw new Exception("Nope!");

        case '|':
        case '-':
            //Status quo
            break;

        default:
            //Got a letter
            collected += grid[position];
            if (velocity.x != 0)
            {
                grid[position] = '-';
            }
            else
            {
                grid[position] = '|';
            }
            break;
    }
}

Console.WriteLine($"Found: {collected}");


Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

Console.WriteLine($"Steps: {steps}");

Console.WriteLine();
Console.ReadKey();

