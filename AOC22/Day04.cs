using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;

namespace AOC22;

public class Day04 : BaseDay
{
    protected override void DoRunPart01()
    {
        var fullyContainedRange = 0;
        string pattern = @"(\d+)-(\d+),(\d+)-(\d+)";
        foreach (var line in InputData)
        {
            var match = Regex.Match(line, pattern);

            var firstStart = int.Parse(match.Groups[1].Value);
            var firstEnd = int.Parse(match.Groups[2].Value);
            var secondStart = int.Parse(match.Groups[3].Value);
            var secondEnd = int.Parse(match.Groups[4].Value);

            //Check if one area is fully contained in the other one
            if ((firstStart <= secondStart && firstEnd >= secondEnd) ||
                (secondStart <= firstStart && secondEnd >= firstEnd))
            {
                fullyContainedRange++;
            }
        }

        Console.WriteLine($"Solution Part 01: {fullyContainedRange}");
    }

    protected override void DoRunPart02()
    {
        var partiallyOverlappingAreas = 0;
        string pattern = @"(\d+)-(\d+),(\d+)-(\d+)";
        foreach (var line in InputData)
        {
            var match = Regex.Match(line, pattern);

            var firstStart = int.Parse(match.Groups[1].Value);
            var firstEnd = int.Parse(match.Groups[2].Value);
            var secondStart = int.Parse(match.Groups[3].Value);
            var secondEnd = int.Parse(match.Groups[4].Value);

            //Generate List with all the values and check intersection
            var firstList = Enumerable.Range(firstStart, (firstEnd-firstStart)+1).ToList();
            var secondList = Enumerable.Range(secondStart, (secondEnd-secondStart)+1).ToList();

            if (firstList.Intersect(secondList).Any())
            {
                partiallyOverlappingAreas++;
            }
        }
        Console.WriteLine($"Solution Part 02: {partiallyOverlappingAreas}");
    }
}