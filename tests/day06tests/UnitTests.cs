using Shouldly;

namespace day06tests;

public class UnitTests
{
    [Fact]
    public void Test1()
    {
        string[] lines = [
            "....#.....",
            ".........#",
            "..........",
            "..#.......",
            ".......#..",
            "..........",
            ".#..^.....",
            "........#.",
            "#.........",
            "......#...",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var map = new Map(input);
        map.GuardStartPosition.ShouldBe((4, 6));
    }

    [Fact]
    public void Test2()
    {
        string[] lines = [
            "....#.....",
            ".........#",
            "..........",
            "..#.......",
            ".......#..",
            "..........",
            ".#..^.....",
            "........#.",
            "#.........",
            "......#...",
        ];
        var input = lines.Select(line => line.ToCharArray()).ToArray();
        var map = new Map(input);
        map.GetPositionsCoveredByGuardWalkingOut().ShouldBe(41);
    }

}
