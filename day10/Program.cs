var lines = File.ReadAllLines("input.txt")
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Select(l => l.ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToArray())
    .ToArray();

var map = new TopographicMap(lines);
var trailheadScores = map.CalculateTrailheadScores();
var sumOfTrailheadScores = trailheadScores.Sum();
System.Console.WriteLine($"Sum of trailhead scores: {sumOfTrailheadScores}");

var trailheadRatings = map.CalculateTrailheadRatings();
var sumOfTrailheadRatings = trailheadRatings.Sum();
System.Console.WriteLine($"Sum of trainhead ratings: {sumOfTrailheadRatings}");

public class TopographicMap(int[][] input)
{
    private readonly int[][] map = input;

    public List<int> CalculateTrailheadScores()
    {
        List<int> trailheadScores = [];

        for (int y = 0; y < map.Length; y++)
        {
            var trailheadIndex = Array.FindIndex(map[y], x => x == 0);
            while (trailheadIndex >= 0)
            {
                // Depth-First Search from this start position, search for nines
                var score = DepthFirstSearch(new Position { X = trailheadIndex, Y = y });
                trailheadScores.Add(score);

                trailheadIndex = Array.FindIndex(map[y], trailheadIndex + 1, x => x == 0);
            }
        }

        return trailheadScores;
    }

    public List<int> CalculateTrailheadRatings()
    {
        List<int> trailheadRatings = [];

        for (int y = 0; y < map.Length; y++)
        {
            var trailheadIndex = Array.FindIndex(map[y], x => x == 0);
            while (trailheadIndex >= 0)
            {
                // Depth-First Search from this start position, search for nines
                var score = DepthFirstSearchAllTracks(new Position { X = trailheadIndex, Y = y });
                trailheadRatings.Add(score);

                trailheadIndex = Array.FindIndex(map[y], trailheadIndex + 1, x => x == 0);
            }
        }

        return trailheadRatings;
    }

    private int DepthFirstSearch(Position trailhead)
    {
        var score = 0;
        HashSet<Position> discovered = [];
        var stack = new Stack<Position>();
        stack.Push(trailhead);
        while (stack.Count > 0)
        {
            var position = stack.Pop();
            if (discovered.Add(position))
            {
                if (map[position.Y][position.X] == 9)
                {
                    score++;
                    continue;
                }

                // Check all directions for next level
                // Up
                if (position.Y - 1 >= 0 && map[position.Y - 1][position.X] == map[position.Y][position.X] + 1)
                {
                    stack.Push(new Position { X = position.X, Y = position.Y - 1 });
                }

                // Right
                if (position.X + 1 < map[position.Y].Length && map[position.Y][position.X + 1] == map[position.Y][position.X] + 1)
                {
                    stack.Push(new Position { X = position.X + 1, Y = position.Y });
                }

                // Down
                if (position.Y + 1 < map.Length && map[position.Y + 1][position.X] == map[position.Y][position.X] + 1)
                {
                    stack.Push(new Position { X = position.X, Y = position.Y + 1 });
                }

                // Left
                if (position.X - 1 >= 0 && map[position.Y][position.X - 1] == map[position.Y][position.X] + 1)
                {
                    stack.Push(new Position { X = position.X - 1, Y = position.Y });
                }
            }
        }

        return score;
    }

    private int DepthFirstSearchAllTracks(Position trailhead)
    {
        var score = 0;
        var stack = new Stack<Position>();
        stack.Push(trailhead);
        while (stack.Count > 0)
        {
            var position = stack.Pop();

            if (map[position.Y][position.X] == 9)
            {
                score++;
                continue;
            }

            // Check all directions for next level
            // Up
            if (position.Y - 1 >= 0 && map[position.Y - 1][position.X] == map[position.Y][position.X] + 1)
            {
                stack.Push(new Position { X = position.X, Y = position.Y - 1 });
            }

            // Right
            if (position.X + 1 < map[position.Y].Length && map[position.Y][position.X + 1] == map[position.Y][position.X] + 1)
            {
                stack.Push(new Position { X = position.X + 1, Y = position.Y });
            }

            // Down
            if (position.Y + 1 < map.Length && map[position.Y + 1][position.X] == map[position.Y][position.X] + 1)
            {
                stack.Push(new Position { X = position.X, Y = position.Y + 1 });
            }

            // Left
            if (position.X - 1 >= 0 && map[position.Y][position.X - 1] == map[position.Y][position.X] + 1)
            {
                stack.Push(new Position { X = position.X - 1, Y = position.Y });
            }
        }

        return score;
    }

    struct Position
    {
        public int X;
        public int Y;
    }
}

