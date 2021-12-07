using AoCTools;

const string inputFile = @"../../../../input20.txt";

Console.WriteLine("Day 20 - Particle Swarm");
Console.WriteLine("Star 1");
Console.WriteLine();

int id = 0;

List<Particle> particles = File.ReadAllLines(inputFile)
    .Select(x => new Particle(id++, x))
    .ToList();

long minAcceleration = particles.Select(x => x.a.TaxiCabLength).Min();

IEnumerable<Particle> lowAParticles = particles.Where(x => x.a.TaxiCabLength == minAcceleration);

long minVelocity = lowAParticles.Select(x => x.v.TaxiCabLength).Min();

IEnumerable<Particle> lowVParticles = lowAParticles.Where(x => x.v.TaxiCabLength == minVelocity);

if (lowVParticles.Count() == 1)
{
    Console.WriteLine(lowVParticles.First());
    Console.WriteLine($"The long-term closes particle is: {lowVParticles.First().id}");
}
else
{
    Console.WriteLine($"After two searches we are down to {lowVParticles.Count()} Particles");

    Console.WriteLine("Printing:");

    foreach (Particle particle in lowVParticles.Take(100))
    {
        Console.WriteLine(particle);
    }
}

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

//Now we begin Advancing
Dictionary<LongPoint3D, Particle> positions = new Dictionary<LongPoint3D, Particle>();
HashSet<Particle> destroyedParticles = new HashSet<Particle>();

int destroyedCount = 0;
int stepMax = 100;
int step = 0;

while (true)
{
    foreach (Particle particle in particles)
    {
        if (positions.ContainsKey(particle.p))
        {
            destroyedParticles.Add(particle);
            destroyedParticles.Add(positions[particle.p]);
        }
        else
        {
            positions.Add(particle.p, particle);
        }
    }

    destroyedCount += destroyedParticles.Count;

    //Kill collided particles
    particles.RemoveAll(x => destroyedParticles.Contains(x));

    destroyedParticles.Clear();
    positions.Clear();

    if (step++ > stepMax)
    {
        break;
    }

    //Advance
    particles.ForEach(x => x.Advance());

}

Console.WriteLine($"Destroyed {destroyedCount} particles after {stepMax} steps");
Console.WriteLine($"Reamining particles: {particles.Count}");

Console.WriteLine();
Console.ReadKey();


class Particle
{
    public LongPoint3D p;
    public LongPoint3D v;
    public readonly LongPoint3D a;

    public readonly int id;

    public Particle(int id, string particle)
    {
        this.id = id;

        string[] splitString = particle.Split(", ");

        p = Parse(splitString[0][2..]);
        v = Parse(splitString[1][2..]);
        a = Parse(splitString[2][2..]);
    }

    public Particle(int id, in LongPoint3D p, in LongPoint3D v, in LongPoint3D a)
    {
        this.id = id;

        this.p = p;
        this.v = v;
        this.a = a;
    }

    public void Advance()
    {
        v += a;
        p += v;
    }

    public long Distance => p.TaxiCabLength;
    public override string ToString() => $"{id,5}) p={p}, v={v}, a={a}";


    private static readonly char[] SplitCharacters = new char[] { '<', '>', ',' };
    private static LongPoint3D Parse(string vector)
    {
        string[] splitString = vector.Split(SplitCharacters, StringSplitOptions.RemoveEmptyEntries);
        return new LongPoint3D(
            x: long.Parse(splitString[0]),
            y: long.Parse(splitString[1]),
            z: long.Parse(splitString[2]));
    }
}
