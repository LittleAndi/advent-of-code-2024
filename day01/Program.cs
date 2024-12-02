var lines = File.ReadAllLines("input.txt");

var leftList = lines.Select(x => Convert.ToInt32(x.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0]));
var rightList = lines.Select(x => Convert.ToInt32(x.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]));

var dc = new DistanceCalculator(leftList, rightList);

System.Console.WriteLine($"Total distance between lists: {dc.Distance}");
System.Console.WriteLine($"Similarity score: {dc.SimilarityScore}");

public class DistanceCalculator(IEnumerable<int> LeftList, IEnumerable<int> RightList)
{
    public int Distance
    {
        get
        {
            var orderedLeftList = LeftList.Order().ToList();
            var orderedRightList = RightList.Order().ToList();

            int totalDistance = 0;

            for (int i = 0; i < orderedLeftList.Count; i++)
            {
                totalDistance += Math.Abs(orderedLeftList[i] - orderedRightList[i]);
            }

            return totalDistance;
        }
    }

    public int SimilarityScore
    {
        get
        {
            var totalScore = 0;

            foreach (var item in LeftList)
            {
                totalScore += item * RightList.Count(x => x == item);
            }

            return totalScore;
        }
    }
}