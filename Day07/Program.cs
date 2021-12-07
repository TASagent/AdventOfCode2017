const string inputFile = @"../../../../input07.txt";

Console.WriteLine("Day 07 - Recursive Circus");
Console.WriteLine("Star 1");
Console.WriteLine();

IEnumerable<ProgramNode> allPrograms = File.ReadAllLines(inputFile).Select(x => new ProgramNode(x)).ToArray();
Dictionary<string, ProgramNode> programDictionary = new Dictionary<string, ProgramNode>();

foreach (ProgramNode node in allPrograms)
{
    programDictionary.Add(node.name, node);
}

foreach (ProgramNode node in allPrograms)
{
    node.PopulateChildren(programDictionary);
}

foreach (ProgramNode node in allPrograms.Where(x => x.Parent == null))
{
    Console.WriteLine($"Root Node: {node.name}");
}

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

foreach (ProgramNode node in allPrograms.Where(x => !x.Balanced))
{
    Console.WriteLine($"Unbalanced Node: {node}");
}

Console.WriteLine();

//The highest unbalanced node will be the one with the lighest deepweight

ProgramNode blamedNode = allPrograms.Where(x => !x.Balanced).OrderBy(x => x.DeepWeight).First();
Console.WriteLine($"Deepest Unbalanced Node: {blamedNode.ToBlameString()}");

ProgramNode problemNode = null;
int targetWeight = 0;

foreach (ProgramNode child in blamedNode.Children)
{
    if (blamedNode.Children.Count(x => x.DeepWeight == child.DeepWeight) == 1)
    {
        problemNode = child;
    }
    else if (targetWeight == 0)
    {
        targetWeight = child.DeepWeight;
    }
}

if (problemNode is null)
{
    throw new Exception($"Failed to find target");
}

Console.WriteLine($"The bad node: {problemNode}");
Console.WriteLine($"New bad node weight: {problemNode.weight + targetWeight - problemNode.DeepWeight}");

Console.WriteLine();
Console.ReadKey();


class ProgramNode
{
    public readonly string name;
    public readonly int weight;
    public List<ProgramNode> Children { get; } = new List<ProgramNode>();
    public ProgramNode Parent { get; private set; } = null;
    private readonly string[] childNames;

    public int DeepWeight => weight + Children.Select(x => x.DeepWeight).Sum();
    public bool Balanced => Children.Select(x => x.DeepWeight).Distinct().Count() <= 1;

    public static readonly char[] separators = new char[]
    {
        ' ',
        '-',
        '>',
        '(',
        ')',
        ','
    };

    public ProgramNode(string line)
    {
        IEnumerable<string> splitLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        name = splitLine.First();
        weight = int.Parse(splitLine.Skip(1).First());
        childNames = splitLine.Skip(2).ToArray();
    }

    public void PopulateChildren(Dictionary<string, ProgramNode> programDictionary)
    {
        foreach (string child in childNames)
        {
            Children.Add(programDictionary[child]);
            programDictionary[child].Parent = this;
        }
    }

    public override string ToString() =>
        $"{name} ({weight}){(Children.Count > 0 ? " -> " : "")}{string.Join(", ", Children.Select(x => x.name))}";

    public string ToBlameString() =>
        string.Join(", ", Children.Select(x => $"{x.name} ({x.DeepWeight})"));
}
