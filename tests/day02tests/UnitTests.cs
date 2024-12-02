using Shouldly;

namespace day02tests;

public class UnitTest1
{
    [Theory]
    [InlineData(new int[] { 7, 6, 4, 2, 1 }, true)]
    [InlineData(new int[] { 1, 2, 7, 8, 9 }, false)]
    [InlineData(new int[] { 9, 7, 6, 2, 1 }, false)]
    [InlineData(new int[] { 1, 3, 2, 4, 5 }, false)]
    [InlineData(new int[] { 8, 6, 4, 4, 1 }, false)]
    [InlineData(new int[] { 1, 3, 6, 7, 9 }, true)]
    [InlineData(new int[] { 43, 46, 47, 48, 50, 52, 56 }, false)]
    public void ShouldReturnSafeOrNot(int[] report, bool shouldBeSafe)
    {
        var analyzer = new ReportAnalyzer(report);
        analyzer.IsSafe.ShouldBe(shouldBeSafe);
    }

    [Theory]
    [InlineData(new int[] { 7, 6, 4, 2, 1 }, true)]
    [InlineData(new int[] { 1, 2, 7, 8, 9 }, false)]
    [InlineData(new int[] { 9, 7, 6, 2, 1 }, false)]
    [InlineData(new int[] { 1, 3, 2, 4, 5 }, true)]
    [InlineData(new int[] { 8, 6, 4, 4, 1 }, true)]
    [InlineData(new int[] { 1, 3, 6, 7, 9 }, true)]
    public void ShouldReturnSafeOrNotUsingProblemDampener(int[] report, bool shouldBeSafe)
    {
        var analyzer = new ReportAnalyzer(report);
        analyzer.IsSafeWithProblemDampener.ShouldBe(shouldBeSafe);
    }

}
