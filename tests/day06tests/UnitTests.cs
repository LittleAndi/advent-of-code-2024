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

    [Fact]
    public void Test3()
    {
        // The 6 positions with extra obstacles should be
        // 3,6
        // 6,7
        // 7,7
        // 1,8
        // 3,8
        // 7,9

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
        map.CountObstaclePositionsThatMakesTheGuardLoop().ShouldBe(6);
    }


}
