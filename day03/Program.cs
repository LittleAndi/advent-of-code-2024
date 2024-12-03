using System.Text.RegularExpressions;

var line = File.ReadAllText("input.txt");

var computer = new Computer(line);
var sumOfUncorruptedMemory = computer.CalculateSumOfUncorruptedMemory();
Console.WriteLine($"Sum of multiplications: {sumOfUncorruptedMemory}");

var sumOfUncorruptedMemoryAccountingForDosAndDonts = computer.CalculateSumOfUncorruptedMemoryAccountingForDosAndDonts();
System.Console.WriteLine($"Sum of multiplications accounting for Do() and Don't(): {sumOfUncorruptedMemoryAccountingForDosAndDonts}");


public partial class Computer(string corruptedMemory)
{
    private readonly string corruptedMemory = corruptedMemory;

    [GeneratedRegex(@"(mul\(\d{1,3},\d{1,3}\))")]
    private partial Regex MultiplierStatementRegex();
    [GeneratedRegex(@"(mul\(\d{1,3},\d{1,3}\))|(don't\(\))|(do\(\))")]
    private partial Regex AllStatementsRegex();
    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private partial Regex MultiplierRegex();

    public int CalculateSumOfUncorruptedMemory()
    {
        var sum = 0;
        var matches = MultiplierStatementRegex().Matches(corruptedMemory);

        foreach (Match match in matches)
        {
            var mulMatch = MultiplierRegex().Match(match.Value);
            var firstNumber = int.Parse(mulMatch.Groups[1].Value);
            var secondNumber = int.Parse(mulMatch.Groups[2].Value);

            sum += firstNumber * secondNumber;
        }

        return sum;
    }

    public int CalculateSumOfUncorruptedMemoryAccountingForDosAndDonts()
    {
        var sum = 0;
        var matches = AllStatementsRegex().Matches(corruptedMemory);
        var enabled = true;

        foreach (Match match in matches)
        {
            switch (match.Value)
            {
                case string x when x.StartsWith("mul") && enabled:
                    var mulMatch = MultiplierRegex().Match(match.Value);
                    var firstNumber = int.Parse(mulMatch.Groups[1].Value);
                    var secondNumber = int.Parse(mulMatch.Groups[2].Value);
                    sum += firstNumber * secondNumber;
                    break;
                case string x when x.Equals("do()"):
                    enabled = true;
                    break;
                case string x when x.Equals("don't()"):
                    enabled = false;
                    break;
            }
        }

        return sum;
    }
}