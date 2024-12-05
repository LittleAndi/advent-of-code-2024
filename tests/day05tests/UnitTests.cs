using Shouldly;

namespace day05tests;

public class UnitTests
{
    [Fact]
    public void Test1()
    {
        string[] lines = [
            "47|53",
            "97|13",
            "97|61",
            "97|47",
            "75|29",
            "61|13",
            "75|53",
            "29|13",
            "97|29",
            "53|29",
            "61|53",
            "97|53",
            "61|29",
            "47|13",
            "75|47",
            "97|75",
            "47|61",
            "75|61",
            "47|29",
            "75|13",
            "53|13",
            "",
            "75,47,61,53,29",
            "97,61,53,29,13",
            "75,29,13",
            "75,97,47,61,53",
            "61,13,29",
            "97,13,75,29,47",
        ];

        var sleighLaunchSafetyManual = new SleighLaunchSafetyManual(lines);
        var validUpdates = sleighLaunchSafetyManual.GetValidUpdates().ToArray();
        validUpdates.Count().ShouldBe(3);
        validUpdates[0].SequenceEqual([75, 47, 61, 53, 29]).ShouldBeTrue();
        validUpdates[1].SequenceEqual([97, 61, 53, 29, 13]).ShouldBeTrue();
        validUpdates[2].SequenceEqual([75, 29, 13]).ShouldBeTrue();
        sleighLaunchSafetyManual.GetSumOfValidUpdates().ShouldBe(143);
    }

    [Fact]
    public void Test2()
    {
        string[] lines = [
            "47|53",
            "97|13",
            "97|61",
            "97|47",
            "75|29",
            "61|13",
            "75|53",
            "29|13",
            "97|29",
            "53|29",
            "61|53",
            "97|53",
            "61|29",
            "47|13",
            "75|47",
            "97|75",
            "47|61",
            "75|61",
            "47|29",
            "75|13",
            "53|13",
            "",
            "75,47,61,53,29",
            "97,61,53,29,13",
            "75,29,13",
            "75,97,47,61,53",
            "61,13,29",
            "97,13,75,29,47",
        ];

        var sleighLaunchSafetyManual = new SleighLaunchSafetyManual(lines);
        var invalidUpdates = sleighLaunchSafetyManual.GetInvalidUpdates().ToArray();
        invalidUpdates.Count().ShouldBe(3);
        invalidUpdates[0].ShouldBe([75, 97, 47, 61, 53]);
        invalidUpdates[1].ShouldBe([61, 13, 29]);
        invalidUpdates[2].ShouldBe([97, 13, 75, 29, 47]);
        sleighLaunchSafetyManual.GetSumOfInvalidUpdates().ShouldBe(123);
    }

    [Theory]
    [InlineData(new int[] { 75, 97, 47, 61, 53 }, new int[] { 97, 75, 47, 61, 53 })]
    [InlineData(new int[] { 61, 13, 29 }, new int[] { 61, 29, 13 })]
    [InlineData(new int[] { 97, 13, 75, 29, 47 }, new int[] { 97, 75, 47, 29, 13 })]
    public void ShouldSortWithRules(int[] unsorted, int[] expectedResult)
    {
        string[] rulesInput = [
            "47|53",
            "97|13",
            "97|61",
            "97|47",
            "75|29",
            "61|13",
            "75|53",
            "29|13",
            "97|29",
            "53|29",
            "61|53",
            "97|53",
            "61|29",
            "47|13",
            "75|47",
            "97|75",
            "47|61",
            "75|61",
            "47|29",
            "75|13",
            "53|13",
        ];

        var rules = rulesInput.Select(x => x.Split('|').Select(x => Convert.ToInt32(x)).ToArray());
        int[] sorted = unsorted.SortWithRules(rules);
        sorted.ShouldBe(expectedResult);
    }
}
