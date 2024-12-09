using Shouldly;

namespace day08tests;

public class UnitTests
{
    [Fact]
    public void Test1()
    {
        string[] lines = [
            "............",
            "........0...",
            ".....0......",
            ".......0....",
            "....0.......",
            "......A.....",
            "............",
            "............",
            "........A...",
            ".........A..",
            "............",
            "............",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var map = new Map(input);
        map.AntinodeCount().ShouldBe(14);
    }

    [Fact]
    public void Test2()
    {
        string[] lines = [
            "0..........0",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var map = new Map(input);
        map.AntinodeCount().ShouldBe(0);
    }

    [Fact]
    public void Test3()
    {
        string[] lines = [
            "............",
            "........0...",
            ".....0......",
            ".......0....",
            "....0.......",
            "......A.....",
            "............",
            "............",
            "........A...",
            ".........A..",
            "............",
            "............",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var map = new Map(input);
        map.ExtendedAntinodeCount().ShouldBe(34);
    }
}
