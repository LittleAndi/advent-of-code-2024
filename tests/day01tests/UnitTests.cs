namespace day01tests;

public class UnitTest1
{
    [Fact]
    public void ShouldReturnZeroDistance()
    {
        var dc = new DistanceCalculator([], []);
        dc.Distance.ShouldBe(0);
    }

    [Fact]
    public void ShouldReturnDistance()
    {
        var dc = new DistanceCalculator([3, 4, 2, 1, 3, 3], [4, 3, 5, 3, 9, 3]);
        dc.Distance.ShouldBe(11);
    }

    [Fact]
    public void ShouldReturnSimilarityScore()
    {
        var dc = new DistanceCalculator([3, 4, 2, 1, 3, 3], [4, 3, 5, 3, 9, 3]);
        dc.SimilarityScore.ShouldBe(31);
    }

}
