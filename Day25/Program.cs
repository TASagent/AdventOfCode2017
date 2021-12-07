using System.Text.RegularExpressions;

const string inputFile = @"../../../../input25.txt";

Console.WriteLine("Day 25 - The Halting Problem");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] inputLines = File.ReadAllLines(inputFile);
int inputIndex = 0;

StateValue currentState = ParseStateValue(inputLines[inputIndex++][^2]);
int stepTotal = int.Parse(Regex.Match(inputLines[inputIndex++], @"\d+").Value);

State[] states = new State[6];

for (int i = 0; i < states.Length; i++)
{
    //Skip Empty Line
    inputIndex++;
    StateValue inState = ParseStateValue(inputLines[inputIndex++][^2]);

    //If the current value is 0:
    inputIndex++;

    bool zWrite = inputLines[inputIndex++][^2] == '1';
    int zShift = inputLines[inputIndex++].EndsWith("right.") ? +1 : -1;
    StateValue zState = ParseStateValue(inputLines[inputIndex++][^2]);

    //If the current value is 0:
    inputIndex++;

    bool oWrite = inputLines[inputIndex++][^2] == '1';
    int oShift = inputLines[inputIndex++].EndsWith("right.") ? +1 : -1;
    StateValue oState = ParseStateValue(inputLines[inputIndex++][^2]);

    states[i] = new State(inState, zWrite, zShift, zState, oWrite, oShift, oState);
}

Dictionary<StateValue, State> stateDict = new Dictionary<StateValue, State>();

foreach (State state in states)
{
    stateDict.Add(state.stateValue, state);
}

//Since this tape is just boolean, I'll just use a hashset.
HashSet<int> tape = new HashSet<int>();


int currentIndex = 0;

for (int step = 0; step < stepTotal; step++)
{
    if (tape.Contains(currentIndex))
    {
        //Execute o
        if (!stateDict[currentState].oWrite)
        {
            tape.Remove(currentIndex);
        }
        currentIndex += stateDict[currentState].oShift;
        currentState = stateDict[currentState].oState;
    }
    else
    {
        //Execute z
        if (stateDict[currentState].zWrite)
        {
            tape.Add(currentIndex);
        }
        currentIndex += stateDict[currentState].zShift;
        currentState = stateDict[currentState].zState;
    }
}

Console.WriteLine($"Diagnostic Checksum: {tape.Count}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

Console.WriteLine("DONE!");

Console.WriteLine();
Console.ReadKey();


static StateValue ParseStateValue(char letter) => letter switch
{
    'A' => StateValue.A,
    'B' => StateValue.B,
    'C' => StateValue.C,
    'D' => StateValue.D,
    'E' => StateValue.E,
    'F' => StateValue.F,
    _ => throw new Exception($"Unrecognized StateValue: {letter}"),
};

enum StateValue
{
    A = 0,
    B,
    C,
    D,
    E,
    F,
    MAX
}

readonly struct State
{
    public readonly StateValue stateValue;

    public readonly bool zWrite;
    public readonly int zShift;
    public readonly StateValue zState;

    public readonly bool oWrite;
    public readonly int oShift;
    public readonly StateValue oState;

    public State(
        StateValue stateValue,
        bool zWrite,
        int zShift,
        StateValue zState,
        bool oWrite,
        int oShift,
        StateValue oState)
    {
        this.stateValue = stateValue;
        this.zWrite = zWrite;
        this.zShift = zShift;
        this.zState = zState;

        this.oWrite = oWrite;
        this.oShift = oShift;
        this.oState = oState;
    }
}