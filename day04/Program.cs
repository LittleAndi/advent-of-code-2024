

var lines = File.ReadAllLines("input.txt")
    .Select(l => l.ToCharArray())
    .ToArray();

var wordSearchSolver = new WordSearchSolver(lines);
var appearances = wordSearchSolver.CountAppearances("XMAS");
System.Console.WriteLine($"The word XMAS does appear {appearances} times");

public class WordSearchSolver
{
    private readonly char[][] mapX;
    private readonly char[][] mapY;
    private readonly int xSize;
    private readonly int ySize;

    public WordSearchSolver(char[][] map)
    {
        mapX = map;
        xSize = map[0].Length;
        ySize = map.Length;
        mapY = new char[xSize][];

        for (int y = 0; y < xSize; y++)
        {
            mapY[y] = new char[ySize];
            for (int x = 0; x < ySize; x++)
            {
                mapY[y][x] = mapX[x][y];
            }
        }
    }

    public int CountAppearances(string word)
    {
        int appearances = 0;
        // Traverse the character map
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // If we got a start of the word
                if (mapX[y][x].Equals(word[0]))
                {
                    appearances += Search(x, y, word);
                }
            }
        }
        return appearances;
    }

    private int Search(int x, int y, string word)
    {
        // Search around in all directions
        int appearances = 0;

        // Right
        if ((x + 4) <= xSize && mapX[y][x..(x + 4)].SequenceEqual(word)) appearances++;

        // Left
        if ((x - 3) >= 0 && (x + 1) <= xSize && mapX[y][(x - 3)..(x + 1)].Reverse().SequenceEqual(word)) appearances++;

        // Down
        if ((y + 4) <= ySize && mapY[x][y..(y + 4)].SequenceEqual(word)) appearances++;

        // Up
        if ((y - 3) >= 0 && (y + 1) <= ySize && mapY[x][(y - 3)..(y + 1)].Reverse().SequenceEqual(word)) appearances++;

        // Right up
        if (x + 3 < xSize && y - 3 >= 0)
        {
            bool match = true;
            for (int i = 0; i < word.Length; i++)
            {
                if (mapX[y - i][x + i] != word[i])
                {
                    match = false;
                    break;
                }
            }
            if (match) appearances++;
        }

        // Right down
        if (x + 3 < xSize && y + 3 < ySize)
        {
            bool match = true;
            for (int i = 0; i < word.Length; i++)
            {
                if (mapX[y + i][x + i] != word[i])
                {
                    match = false;
                    break;
                }
            }
            if (match) appearances++;
        }

        // Left down
        if (x - 3 >= 0 && y + 3 < ySize)
        {
            bool match = true;
            for (int i = 0; i < word.Length; i++)
            {
                if (mapX[y + i][x - i] != word[i])
                {
                    match = false;
                    break;
                }
            }
            if (match) appearances++;
        }

        // Left up
        if (x - 3 >= 0 && y - 3 >= 0)
        {
            bool match = true;
            for (int i = 0; i < word.Length; i++)
            {
                if (mapX[y - i][x - i] != word[i])
                {
                    match = false;
                    break;
                }
            }
            if (match) appearances++;
        }

        return appearances;
    }
}