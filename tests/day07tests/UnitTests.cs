using Shouldly;

namespace day07tests;

public class UnitTests
{
    [Theory]
    [InlineData(190, new long[] { 10, 19 }, true)]
    [InlineData(3267, new long[] { 81, 40, 27 }, true)]
    [InlineData(83, new long[] { 17, 5 }, false)]
    [InlineData(156, new long[] { 15, 6 }, false)]
    [InlineData(7290, new long[] { 6, 8, 6, 15 }, false)]
    [InlineData(161011, new long[] { 16, 10, 13 }, false)]
    [InlineData(192, new long[] { 17, 8, 14 }, false)]
    [InlineData(21037, new long[] { 9, 7, 18, 13 }, false)]
    [InlineData(292, new long[] { 11, 6, 16, 20 }, true)]
    public void Test1(long result, long[] numbers, bool isValidWithTwoOperators)
    {
        var evaluator = new Evaluator(result, numbers);
        evaluator.IsValidWithTwoOperators().ShouldBe(isValidWithTwoOperators);
    }

    [Fact]
    public void Test2()
    {
        string[] lines = [
            "190: 10 19",
            "3267: 81 40 27",
            "83: 17 5",
            "156: 15 6",
            "7290: 6 8 6 15",
            "161011: 16 10 13",
            "192: 17 8 14",
            "21037: 9 7 18 13",
            "292: 11 6 16 20",
        ];
        var evaluators = lines.Select(line => new Evaluator(Convert.ToInt64(line.Split(':')[0]), line.Split(':')[1].TrimStart().Split(' ').Select(s => Convert.ToInt64(s)).ToArray()));
        evaluators.Where(x => x.IsValidWithTwoOperators()).Sum(x => x.Result).ShouldBe(3749);
    }

    [Theory]
    [InlineData(190, new long[] { 10, 19 }, true)]
    [InlineData(3267, new long[] { 81, 40, 27 }, true)]
    [InlineData(83, new long[] { 17, 5 }, false)]
    [InlineData(156, new long[] { 15, 6 }, true)]
    [InlineData(7290, new long[] { 6, 8, 6, 15 }, true)]
    [InlineData(161011, new long[] { 16, 10, 13 }, false)]
    [InlineData(192, new long[] { 17, 8, 14 }, true)]
    [InlineData(21037, new long[] { 9, 7, 18, 13 }, false)]
    [InlineData(292, new long[] { 11, 6, 16, 20 }, true)]
    public void Test3(long result, long[] numbers, bool isValidWithThreeOperators)
    {
        var evaluator = new Evaluator(result, numbers);
        evaluator.IsValidWithThreeOperators().ShouldBe(isValidWithThreeOperators);
    }
}
