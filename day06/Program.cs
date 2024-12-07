using System.Runtime.InteropServices;

var lines = File.ReadAllLines("input.txt")
    .Select(l => l.ToCharArray())
    .ToArray();

var map = new Map(lines);
var positionsCoveredByGuardWalkingOut = map.GetPositionsCoveredByGuardWalkingOut();
System.Console.WriteLine($"Positions covered by guard walking out: {positionsCoveredByGuardWalkingOut}");

var obstaclePositionsThatMakesTheGuardLoop = map.CountObstaclePositionsThatMakesTheGuardLoop();
System.Console.WriteLine($"Obstacle positions that makes the guard loop: {obstaclePositionsThatMakesTheGuardLoop}");

public class Map
{
    private readonly char[][] map;
    private readonly int startX;
    private readonly int startY;

    public Map(char[][] input)
    {
        map = input;
        startX = -1;
        startY = 0;
        bool startFound = false;
        while (!startFound)
        {
            startX = Array.FindIndex(map[startY], c => c.Equals('^'));
            if (startX >= 0) break;
            startY++;
        }
    }

    public int GetPositionsCoveredByGuardWalkingOut()
    {
        bool inside = true;
        char currentDirection = map[startY][startX];
        int posX = startX;
        int posY = startY;
        HashSet<(int x, int y)> visited = [(posX, posY)];

        while (inside)
        {
            // Walk around
            switch (currentDirection)
            {
                case '^':
                    if (posY - 1 < 0) { inside = false; break; }
                    if (map[posY - 1][posX].Equals('#'))
                    {
                        // Rotate (no move)
                        currentDirection = '>';
                    }
                    else
                    {
                        // Move and save
                        posY--;
                        visited.Add((posX, posY));
                    }
                    break;
                case '>':
                    if (posX + 1 >= map[posY].Length) { inside = false; break; }
                    if (map[posY][posX + 1].Equals('#'))
                    {
                        // Rotate (no move)
                        currentDirection = 'v';
                    }
                    else
                    {
                        // Move and save
                        posX++;
                        visited.Add((posX, posY));
                    }
                    break;
                case 'v':
                    if (posY + 1 >= map.Length) { inside = false; break; }
                    if (map[posY + 1][posX].Equals('#'))
                    {
                        // Rotate (no move)
                        currentDirection = '<';
                    }
                    else
                    {
                        // Move and save
                        posY++;
                        visited.Add((posX, posY));
                    }
                    break;
                case '<':
                    if (posX - 1 < 0) { inside = false; break; }
                    if (map[posY][posX - 1].Equals('#'))
                    {
                        // Rotate (no move)
                        currentDirection = '^';
                    }
                    else
                    {
                        // Move and save
                        posX--;
                        visited.Add((posX, posY));
                    }
                    break;
            }
        }

        return visited.Count;
    }

    public int CountObstaclePositionsThatMakesTheGuardLoop()
    {
        int obstaclePositionsThatMakesTheGuardLoop = 0;

        // We're going to walk around like normal, but also save the turns,
        // because we think that is along those lines where we'll add new obstructions
        var turns = GetTurns();

        HashSet<(int x, int y)> obstaclePositionsRecorded = [];

        // Now try obstacles between the turns, remember 1st turn is the 2nd record in turns
        // The last "turn" is the exit position
        for (int i = 1; i < turns.Count; i++)
        {
            switch (turns[i].Value)
            {
                case '^':
                    // Try obstacles from previous turn up to this turn
                    for (int y = turns[i - 1].Key.y - 1; y >= turns[i].Key.y; y--)
                    {
                        // Copy map and insert new obstacle, always skip start position
                        if (turns[i].Key.x == startX && y == startY) continue;
                        var mapWithObstacle = CopyAndInsertObstacle(map, (turns[i].Key.x, y));
                        var isGuardLooping = IsGuardLoopingOnModifiedMap(mapWithObstacle, turns.Count);
                        if (isGuardLooping)
                        {
                            obstaclePositionsThatMakesTheGuardLoop++;
                            obstaclePositionsRecorded.Add((turns[i].Key.x, y));
                        }
                    }
                    break;
                case '>':
                    // Try obstacles from previous turn up to this turn
                    for (int x = turns[i - 1].Key.x + 1; x <= turns[i].Key.x; x++)
                    {
                        // Copy map and insert new obstacle, always skip start position
                        if (x == startX && turns[i].Key.y == startY) continue;
                        var mapWithObstacle = CopyAndInsertObstacle(map, (x, turns[i].Key.y));
                        var isGuardLooping = IsGuardLoopingOnModifiedMap(mapWithObstacle, turns.Count);
                        if (isGuardLooping)
                        {
                            obstaclePositionsThatMakesTheGuardLoop++;
                            obstaclePositionsRecorded.Add((x, turns[i].Key.y));
                        }
                    }
                    break;
                case 'v':
                    // Try obstacles from previous turn up to this turn
                    for (int y = turns[i - 1].Key.y + 1; y <= turns[i].Key.y; y++)
                    {
                        // Copy map and insert new obstacle, always skip start position
                        if (turns[i].Key.x == startX && y == startY) continue;
                        var mapWithObstacle = CopyAndInsertObstacle(map, (turns[i].Key.x, y));
                        var isGuardLooping = IsGuardLoopingOnModifiedMap(mapWithObstacle, turns.Count);
                        if (isGuardLooping)
                        {
                            obstaclePositionsThatMakesTheGuardLoop++;
                            obstaclePositionsRecorded.Add((turns[i].Key.x, y));
                        }
                    }
                    break;
                case '<':
                    // Try obstacles from previous turn up to this turn
                    for (int x = turns[i - 1].Key.x - 1; x >= turns[i].Key.x; x--)
                    {
                        // Copy map and insert new obstacle, always skip start position
                        if (x == startX && turns[i].Key.y == startY) continue;
                        var mapWithObstacle = CopyAndInsertObstacle(map, (x, turns[i].Key.y));
                        var isGuardLooping = IsGuardLoopingOnModifiedMap(mapWithObstacle, turns.Count);
                        if (isGuardLooping)
                        {
                            obstaclePositionsThatMakesTheGuardLoop++;
                            obstaclePositionsRecorded.Add((x, turns[i].Key.y));
                        }
                    }
                    break;
            }
        }

        return obstaclePositionsRecorded.Count;
    }

    private static char[][] CopyAndInsertObstacle(char[][] map, (int x, int y) obstacle)
    {
        char[][] mapWithObstacle = new char[map.Length][];
        for (int y = 0; y < map.Length; y++)
        {
            mapWithObstacle[y] = new char[map[y].Length];
            Array.Copy(map[y], mapWithObstacle[y], map[y].Length);
        }
        mapWithObstacle[obstacle.y][obstacle.x] = '#';
        return mapWithObstacle;
    }

    private List<KeyValuePair<(int x, int y), char>> GetTurns()
    {
        // We're going to walk around like normal, but also save the turns,
        // because we think that is along those lines where we'll add new obstructions
        bool inside = true;
        char currentDirection = map[startY][startX];
        int posX = startX;
        int posY = startY;
        HashSet<(int x, int y)> visited = [(posX, posY)];
        List<KeyValuePair<(int x, int y), char>> turns = [new((posX, posY), currentDirection)];

        while (inside)
        {
            // Walk around
            switch (currentDirection)
            {
                case '^':
                    if (posY - 1 < 0) { inside = false; break; }
                    if (map[posY - 1][posX].Equals('#'))
                    {
                        // Rotate (no move)
                        turns.Add(new((posX, posY), currentDirection));
                        currentDirection = '>';
                    }
                    else
                    {
                        // Move and save
                        posY--;
                        visited.Add((posX, posY));
                    }
                    break;
                case '>':
                    if (posX + 1 >= map[posY].Length) { inside = false; break; }
                    if (map[posY][posX + 1].Equals('#'))
                    {
                        // Rotate (no move)
                        turns.Add(new((posX, posY), currentDirection));
                        currentDirection = 'v';
                    }
                    else
                    {
                        // Move and save
                        posX++;
                        visited.Add((posX, posY));
                    }
                    break;
                case 'v':
                    if (posY + 1 >= map.Length) { inside = false; break; }
                    if (map[posY + 1][posX].Equals('#'))
                    {
                        // Rotate (no move)
                        turns.Add(new((posX, posY), currentDirection));
                        currentDirection = '<';
                    }
                    else
                    {
                        // Move and save
                        posY++;
                        visited.Add((posX, posY));
                    }
                    break;
                case '<':
                    if (posX - 1 < 0) { inside = false; break; }
                    if (map[posY][posX - 1].Equals('#'))
                    {
                        // Rotate (no move)
                        turns.Add(new((posX, posY), currentDirection));
                        currentDirection = '^';
                    }
                    else
                    {
                        // Move and save
                        posX--;
                        visited.Add((posX, posY));
                    }
                    break;
            }
        }

        turns.Add(new((posX, posY), currentDirection));
        return turns;
    }

    private bool IsGuardLoopingOnModifiedMap(char[][] modifiedMap, int maxTurns)
    {
        bool inside = true;
        char currentDirection = modifiedMap[startY][startX];
        int posX = startX;
        int posY = startY;
        int turns = 0;
        HashSet<(int x, int y)> visited = [(posX, posY)];

        while (inside && turns < maxTurns * 2)
        {
            // Walk around
            switch (currentDirection)
            {
                case '^':
                    if (posY - 1 < 0) { inside = false; break; }
                    if (modifiedMap[posY - 1][posX].Equals('#'))
                    {
                        // Rotate (no move)
                        currentDirection = '>';
                        turns++;
                    }
                    else
                    {
                        // Move and save
                        posY--;
                        visited.Add((posX, posY));
                    }
                    break;
                case '>':
                    if (posX + 1 >= modifiedMap[posY].Length) { inside = false; break; }
                    if (modifiedMap[posY][posX + 1].Equals('#'))
                    {
                        // Rotate (no move)
                        currentDirection = 'v';
                        turns++;
                    }
                    else
                    {
                        // Move and save
                        posX++;
                        visited.Add((posX, posY));
                    }
                    break;
                case 'v':
                    if (posY + 1 >= modifiedMap.Length) { inside = false; break; }
                    if (modifiedMap[posY + 1][posX].Equals('#'))
                    {
                        // Rotate (no move)
                        currentDirection = '<';
                        turns++;
                    }
                    else
                    {
                        // Move and save
                        posY++;
                        visited.Add((posX, posY));
                    }
                    break;
                case '<':
                    if (posX - 1 < 0) { inside = false; break; }
                    if (modifiedMap[posY][posX - 1].Equals('#'))
                    {
                        // Rotate (no move)
                        currentDirection = '^';
                        turns++;
                    }
                    else
                    {
                        // Move and save
                        posX--;
                        visited.Add((posX, posY));
                    }
                    break;
                default:
                    break;
            }
        }

        return inside; // still inside => we're looping
    }

    public (int x, int y) GuardStartPosition => (startX, startY);
}