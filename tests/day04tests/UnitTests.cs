using Shouldly;

namespace day04tests;

public class UnitTests
{
    [Fact]
    public void Test0()
    {
        string[] lines = [
            "....",
            "....",
            "....",
            "....",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var wordSearchSolver = new WordSearchSolver(input);
        var occurances = wordSearchSolver.CountAppearances("XMAS");
        occurances.ShouldBe(0);
    }

    [Fact]
    public void Test1()
    {
        string[] lines = [
            "..X...",
            ".SAMX.",
            ".A..A.",
            "XMAS.S",
            ".X....",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var wordSearchSolver = new WordSearchSolver(input);
        var occurances = wordSearchSolver.CountAppearances("XMAS");
        occurances.ShouldBe(4);
    }

    [Fact]
    public void Test2()
    {
        string[] lines = [
            "MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var wordSearchSolver = new WordSearchSolver(input);
        var occurances = wordSearchSolver.CountAppearances("XMAS");
        occurances.ShouldBe(18);
    }

    [Fact]
    public void Test3()
    {
        string[] lines = [
            "X...",
            ".M..",
            "..A.",
            "...S",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var wordSearchSolver = new WordSearchSolver(input);
        var occurances = wordSearchSolver.CountAppearances("XMAS");
        occurances.ShouldBe(1);
    }

    [Fact]
    public void Test4()
    {
        string[] lines = [
            "S...",
            ".A..",
            "..M.",
            "...X",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var wordSearchSolver = new WordSearchSolver(input);
        var occurances = wordSearchSolver.CountAppearances("XMAS");
        occurances.ShouldBe(1);
    }

    [Fact]
    public void Test5()
    {
        string[] lines = [
            "...X",
            "..M.",
            ".A..",
            "S...",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var wordSearchSolver = new WordSearchSolver(input);
        var occurances = wordSearchSolver.CountAppearances("XMAS");
        occurances.ShouldBe(1);
    }

    [Fact]
    public void Test6()
    {
        string[] lines = [
            "...S",
            "..A.",
            ".M..",
            "X...",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var wordSearchSolver = new WordSearchSolver(input);
        var occurances = wordSearchSolver.CountAppearances("XMAS");
        occurances.ShouldBe(1);
    }

    [Fact]
    public void Test7()
    {
        string[] lines = [
            "...S",
            "..A.",
            ".M..",
            "XMAS",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var wordSearchSolver = new WordSearchSolver(input);
        var occurances = wordSearchSolver.CountAppearances("XMAS");
        occurances.ShouldBe(2);
    }
}
