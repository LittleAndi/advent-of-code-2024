using System.Text.RegularExpressions;

var line = File.ReadAllText("input.txt");

var computer = new Computer(line);
var sum = computer.CalculateSumOfUncorruptedMemory();
Console.WriteLine($"Sum of multiplications: {sum}");

public class Computer(string corruptedMemory)
{
    private readonly string corruptedMemory = corruptedMemory;
    private readonly Regex regex = new(@"(mul\(\d{1,3},\d{1,3}\))");
    private readonly Regex mulRegex = new(@"mul\((\d{1,3}),(\d{1,3})\)");

    public int CalculateSumOfUncorruptedMemory()
    {
        var sum = 0;
        var matches = regex.Matches(corruptedMemory);

        foreach (Match match in matches)
        {
            var mulMatch = mulRegex.Match(match.Value);
            var firstNumber = int.Parse(mulMatch.Groups[1].Value);
            var secondNumber = int.Parse(mulMatch.Groups[2].Value);

            sum += firstNumber * secondNumber;
        }

        return sum;
    }

    public object CalculateSumOfUncorruptedMemoryAccountingForDosAndDonts()
    {
        return CalculateSumOfUncorruptedMemory();
    }
}