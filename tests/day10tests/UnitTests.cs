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
}
