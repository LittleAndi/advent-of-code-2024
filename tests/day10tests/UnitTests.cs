using System.Runtime.InteropServices.Marshalling;
using Shouldly;

namespace day10tests;

public class UnitTests
{
    [Fact]
    public void Test1()
    {
        string[] lines = [
            "...0...",
            "...1...",
            "...2...",
            "6543456",
            "7.....7",
            "8.....8",
            "9.....9",
        ];
        var input = lines.Select(l => l.ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();
        var topographicMap = new TopographicMap(input);
        var trailheadScores = topographicMap.CalculateTrailheadScores();
        trailheadScores.Count.ShouldBe(1);
        trailheadScores.Sum().ShouldBe(2);
    }

    [Fact]
    public void Test2()
    {
        string[] lines = [
            "89010123",
            "78121874",
            "87430965",
            "96549874",
            "45678903",
            "32019012",
            "01329801",
            "10456732",
        ];
        var input = lines.Select(l => l.ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();
        var topographicMap = new TopographicMap(input);
        var trailheadScores = topographicMap.CalculateTrailheadScores();
        trailheadScores.Count.ShouldBe(9);
        trailheadScores.Sum().ShouldBe(36);
    }

    [Fact]
    public void Test3()
    {
        string[] lines = [
            ".....0.",
            "..4321.",
            "..5..2.",
            "..6543.",
            "..7..4.",
            "..8765.",
            "..9....",
        ];

        var input = lines.Select(l => l.ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();
        var topographicMap = new TopographicMap(input);
        var trailheadRatings = topographicMap.CalculateTrailheadRatings();
        trailheadRatings.Count.ShouldBe(1);
        trailheadRatings.Sum().ShouldBe(3);
    }

    [Fact]
    public void Test4()
    {
        string[] lines = [
            "..90..9",
            "...1.98",
            "...2..7",
            "6543456",
            "765.987",
            "876....",
            "987....",
        ];

        var input = lines.Select(l => l.ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();
        var topographicMap = new TopographicMap(input);
        var trailheadRatings = topographicMap.CalculateTrailheadRatings();
        trailheadRatings.Count.ShouldBe(1);
        trailheadRatings.Sum().ShouldBe(13);
    }

    [Fact]
    public void Test5()
    {
        string[] lines = [
            "012345",
            "123456",
            "234567",
            "345678",
            "4.6789",
            "56789.",
        ];

        var input = lines.Select(l => l.ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();
        var topographicMap = new TopographicMap(input);
        var trailheadRatings = topographicMap.CalculateTrailheadRatings();
        trailheadRatings.Count.ShouldBe(1);
        trailheadRatings.Sum().ShouldBe(227);
    }

    [Fact]
    public void Test6()
    {
        string[] lines = [
            "89010123",
            "78121874",
            "87430965",
            "96549874",
            "45678903",
            "32019012",
            "01329801",
            "10456732",
        ];

        var input = lines.Select(l => l.ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();
        var topographicMap = new TopographicMap(input);
        var trailheadRatings = topographicMap.CalculateTrailheadRatings();
        trailheadRatings.Count.ShouldBe(9);
        trailheadRatings.Sum().ShouldBe(81);
    }
}
