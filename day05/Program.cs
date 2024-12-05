using System.Data;

var lines = File.ReadAllLines("input.txt");

var sleighLaunchSafetyManual = new SleighLaunchSafetyManual(lines);
var sumOfValidUpdates = sleighLaunchSafetyManual.GetSumOfValidUpdates();

System.Console.WriteLine($"Sum of valid updates: {sumOfValidUpdates}");

var sumOfInvalidUpdates = sleighLaunchSafetyManual.GetSumOfInvalidUpdates();

System.Console.WriteLine($"Sum of corrected invalid updates: {sumOfInvalidUpdates}");

public class SleighLaunchSafetyManual
{
    private readonly IEnumerable<int[]> rules;
    private readonly IEnumerable<int[]> updates;

    public SleighLaunchSafetyManual(string[] input)
    {
        rules = input.Where(x => x.Contains('|')).Select(x => x.Split('|').Select(x => Convert.ToInt32(x)).ToArray());
        updates = input.Where(x => x.Contains(',')).Select(x => x.Split(',').Select(x => Convert.ToInt32(x)).ToArray());
    }

    public IEnumerable<int[]> GetValidUpdates()
    {
        var validUpdates = new List<int[]>();

        foreach (var update in updates)
        {
            bool valid = true;

            foreach (var rule in rules)
            {
                if (update.Contains(rule[0]) && update.Contains(rule[1]))
                {
                    // We should test the rule
                    var updatePair = update.Where(x => x == rule[0] || x == rule[1]);
                    if (!updatePair.SequenceEqual(rule))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            if (valid) validUpdates.Add(update);
        }

        return validUpdates;
    }

    public int GetSumOfValidUpdates()
    {
        var validUpdates = GetValidUpdates();
        return validUpdates.Select(x => x[x.Length / 2]).Sum();
    }

    public IEnumerable<int[]> GetInvalidUpdates()
    {
        var invalidUpdates = new List<int[]>();

        foreach (var update in updates)
        {
            bool valid = true;

            foreach (var rule in rules)
            {
                if (update.Contains(rule[0]) && update.Contains(rule[1]))
                {
                    // We should test the rule
                    var updatePair = update.Where(x => x == rule[0] || x == rule[1]);
                    if (!updatePair.SequenceEqual(rule))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            if (!valid) invalidUpdates.Add(update);
        }

        return invalidUpdates;
    }

    public int GetSumOfInvalidUpdates()
    {
        var invalidUpdates = GetInvalidUpdates();
        return invalidUpdates.Select(unsorted => unsorted.SortWithRules(rules)).Select(sorted => sorted[sorted.Length / 2]).Sum();
    }
}

public static class Extension
{
    public static int[] SortWithRules(this int[] array, IEnumerable<int[]> rules)
    {
        // Apply some kind of bubble sort (?)
        // Loop until no rules fails?
        bool rulesPassed = false;
        while (!rulesPassed)
        {
            rulesPassed = true;

            foreach (var rule in rules)
            {
                if (array.Contains(rule[0]) && array.Contains(rule[1]))
                {
                    // Both numbers are in
                    var index0 = Array.FindIndex(array, x => x == rule[0]);
                    var index1 = Array.FindIndex(array, x => x == rule[1]);

                    if (index0 > index1)
                    {
                        // Swap positions
                        array[index0] = rule[1];
                        array[index1] = rule[0];

                        // Fail the rule to start over
                        rulesPassed = false;
                        break;
                    }
                }
            }
        }

        return array;
    }
}