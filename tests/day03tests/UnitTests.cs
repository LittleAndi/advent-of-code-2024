namespace day03tests;

public class UnitTests
{
    [Fact]
    public void ShouldReturnSumOfMul()
    {
        var computer = new Computer("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))");
        var sum = computer.CalculateSumOfUncorruptedMemory();
        sum.ShouldBe(161);
    }

    [Fact]
    public void ShouldReturnSumOfMulAccountingforDosAndDonts()
    {
        var computer = new Computer("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))");
        var sum = computer.CalculateSumOfUncorruptedMemoryAccountingForDosAndDonts();
        sum.ShouldBe(48);
    }
}