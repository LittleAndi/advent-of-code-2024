var lines = File.ReadAllLines("input.txt");

var reports = lines.Select(x => x.Split(' ').ToArray().Select(v => Convert.ToInt32(v)).ToArray());
var safeReportCount = reports.Select(x => new ReportAnalyzer(x)).Count(x => x.IsSafe);

System.Console.WriteLine($"Safe reports: {safeReportCount}");

var safeReportCountWithProblemDampener = reports.Select(x => new ReportAnalyzer(x)).Count(x => x.IsSafeWithProblemDampener);

System.Console.WriteLine($"Safe reports with problem dampener: {safeReportCountWithProblemDampener}");

public class ReportAnalyzer(int[] report)
{
    private readonly int[] report = report;

    public bool IsSafe
    {
        get
        {
            int sign = Math.Sign(report[1] - report[0]);
            int i = 1;
            while (i < report.Length)
            {
                var value = report[i] - report[i - 1];
                if (Math.Sign(value) != sign) return false;
                if (Math.Abs(value) < 1 || Math.Abs(value) > 3) return false;
                i++;
            }
            return true;
        }
    }

    public bool IsSafeWithProblemDampener
    {
        get
        {
            if (IsSafe) return true;

            // Try removing
            for (int i = 0; i < report.Length; i++)
            {
                // i is the position to cut
                var tempReport = report.Where((number, index) => index != i).ToArray();
                if (new ReportAnalyzer(tempReport).IsSafe) return true;
            }
            return false;
        }
    }
}