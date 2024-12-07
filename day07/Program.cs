using System.IO.Pipelines;

var lines = File.ReadAllLines("input.txt")
    .Select(line => new Evaluator(Convert.ToInt64(line.Split(':')[0]), line.Split(':')[1].TrimStart().Split(' ').Select(s => Convert.ToInt64(s)).ToArray()));

var totalCalibrationResultWithTwoOperators = lines.Where(x => x.IsValidWithTwoOperators()).Sum(x => x.Result);
System.Console.WriteLine($"Total calibration result with two operators: {totalCalibrationResultWithTwoOperators}");

var totalCalibrationResultWithThreeOperators = lines.Where(x => x.IsValidWithThreeOperators()).Sum(x => x.Result);
System.Console.WriteLine($"Total calibration result with three operators: {totalCalibrationResultWithThreeOperators}");

public class Evaluator(long result, long[] numbers)
{
    public long Result { get; } = result;
    private readonly long[] numbers = numbers;

    public bool IsValidWithTwoOperators()
    {
        return CalculateWithTwoOperators(numbers[0], numbers[1..]);
    }

    private bool CalculateWithTwoOperators(long current, long[] numbers)
    {
        if (numbers.Length == 1)
        {
            if (current + numbers[0] == Result) return true;
            if (current * numbers[0] == Result) return true;
            return false;
        }
        else
        {
            if (CalculateWithTwoOperators(current + numbers[0], numbers[1..])) return true;
            if (CalculateWithTwoOperators(current * numbers[0], numbers[1..])) return true;
            return false;
        }
    }

    public bool IsValidWithThreeOperators()
    {
        return CalculateWithThreeOperators(numbers[0], numbers[1..]);
    }

    private bool CalculateWithThreeOperators(long current, long[] numbers)
    {
        if (numbers.Length == 1)
        {
            if (current + numbers[0] == Result) return true;
            if (current * numbers[0] == Result) return true;
            if (Convert.ToInt64($"{current}{numbers[0]}") == Result) return true;
            return false;
        }
        else
        {
            if (CalculateWithThreeOperators(current + numbers[0], numbers[1..])) return true;
            if (CalculateWithThreeOperators(current * numbers[0], numbers[1..])) return true;
            if (CalculateWithThreeOperators(Convert.ToInt64($"{current}{numbers[0]}"), numbers[1..])) return true;
            return false;
        }
    }
}