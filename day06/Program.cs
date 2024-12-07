var lines = File.ReadAllLines("input.txt")
    .Select(l => l.ToCharArray())
    .ToArray();

var map = new Map(lines);
var positionsCoveredByGuardWalkingOut = map.GetPositionsCoveredByGuardWalkingOut();

System.Console.WriteLine($"Positions covered by guard walking out: {positionsCoveredByGuardWalkingOut}");

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

    public (int x, int y) GuardStartPosition => (startX, startY);
}