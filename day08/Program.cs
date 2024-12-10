
using System.Buffers;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using Microsoft.VisualBasic;

var lines = File.ReadAllLines("input.txt")
    .Select(l => l.ToCharArray())
    .ToArray();

var map = new Map(lines);
var antinodePositions = map.AntinodeCount();
System.Console.WriteLine($"Antinode count: {antinodePositions}");

var extendedAntinodePositions = map.ExtendedAntinodeCount();
System.Console.WriteLine($"Extended antinode count: {extendedAntinodePositions}");

public class Map
{
    private readonly char[][] map;
    private readonly HashSet<char> antennas;
    private readonly HashSet<Position> antennaPositions = [];
    private readonly Dictionary<char, IEnumerable<(Position A, Position B)>> antennaPositionCombinations = [];

    public Map(char[][] input)
    {
        map = input;

        // The uniqe antennas
        antennas = input.SelectMany(x => x).Where(x => !x.Equals('.')).ToHashSet();

        // Get all the antenna position combinations
        foreach (var antenna in antennas)
        {
            List<Position> antennaPositions = [];
            for (int y = 0; y < map.Length; y++)
            {
                var startIndex = 0;
                var antennaFoundAt = Array.FindIndex(map[y], startIndex, x => x.Equals(antenna));
                while (antennaFoundAt >= 0)
                {
                    var antennaPosition = new Position() { X = antennaFoundAt, Y = y };
                    antennaPositions.Add(antennaPosition);
                    this.antennaPositions.Add(antennaPosition);
                    startIndex = antennaFoundAt + 1;
                    antennaFoundAt = Array.FindIndex(map[y], startIndex, x => x.Equals(antenna));
                }
            }
            antennaPositionCombinations.Add(antenna, PermutePositions([.. antennaPositions]));
        }
    }

    public int AntinodeCount()
    {
        HashSet<Position> antinodePositions = [];

        foreach (var antenna in antennas)
        {
            foreach (var (A, B) in antennaPositionCombinations[antenna])
            {
                Position antinode1 = new() { X = A.X + (A.X - B.X), Y = A.Y + (A.Y - B.Y) };
                if (IsInside(antinode1)) antinodePositions.Add(antinode1);

                Position antinode2 = new() { X = B.X + (B.X - A.X), Y = B.Y + (B.Y - A.Y) };
                if (IsInside(antinode2)) antinodePositions.Add(antinode2);
            }
        }

        return antinodePositions.Count;
    }

    public int ExtendedAntinodeCount()
    {
        HashSet<Position> antinodePositions = [];

        foreach (var antenna in antennas)
        {
            foreach (var (A, B) in antennaPositionCombinations[antenna])
            {
                // One direction
                {
                    var xDiff = A.X - B.X;
                    var yDiff = A.Y - B.Y;
                    Position antinode = new() { X = A.X + xDiff, Y = A.Y + yDiff };
                    var isInside = IsInside(antinode);
                    if (isInside) antinodePositions.Add(antinode);
                    while (isInside)
                    {
                        antinode = new() { X = antinode.X + xDiff, Y = antinode.Y + yDiff };
                        isInside = IsInside(antinode);
                        if (isInside) antinodePositions.Add(antinode);
                    }
                }

                // The other direction
                {
                    var xDiff = B.X - A.X;
                    var yDiff = B.Y - A.Y;
                    Position antinode = new() { X = B.X + xDiff, Y = B.Y + yDiff };
                    var isInside = IsInside(antinode);
                    if (isInside) antinodePositions.Add(antinode);
                    while (isInside)
                    {
                        antinode = new() { X = antinode.X + xDiff, Y = antinode.Y + yDiff };
                        isInside = IsInside(antinode);
                        if (isInside) antinodePositions.Add(antinode);
                    }
                }
            }
        }

        // Add the antennas
        foreach (var antennaPosition in antennaPositions)
        {
            antinodePositions.Add(antennaPosition);
        }

        return antinodePositions.Count;
    }

    private bool IsInside(Position position)
    {
        if (position.X < 0 || position.X >= map[0].Length) return false;
        if (position.Y < 0 || position.Y >= map.Length) return false;
        return true;
    }

    private static List<(Position A, Position B)> PermutePositions(Position[] positions)
    {
        var positionPairs = new List<(Position, Position)>();

        for (int i = 0; i < positions.Length - 1; i++)
        {
            for (int j = i + 1; j < positions.Length; j++)
            {
                positionPairs.Add((positions[i], positions[j]));
            }
        }
        return positionPairs;
    }

    struct Position
    {
        public int X;
        public int Y;
    }
}

