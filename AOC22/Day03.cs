
namespace AOC22
{
    internal class Day03 : BaseDay
    {
        protected override void DoRunPart01()
        {
            var sum = 0;
            foreach (var line in InputData)
            {
                var str1 = line[..(line.Length / 2)];
                var str2 = line[(line.Length / 2)..];
                var common = str1.Intersect(str2).FirstOrDefault();

                //lowercase -96
                //uppercase -38
                sum += common > 90 ? common - 96 : common - 38;

            }

            Console.WriteLine($"Solution Part01: {sum}");
        }

        protected override void DoRunPart02()
        {
            var sum = 0;
            for (var i = 0; i < InputData.Count; i+=3)
            {
                var commonCharsForTwoLines = InputData[i].Intersect(InputData[i+1]).ToList();
                var commonChar = commonCharsForTwoLines.Intersect(InputData[i+2]).FirstOrDefault();
                sum += commonChar > 90 ? commonChar - 96 : commonChar - 38;
            }

            Console.WriteLine($"Solution Part02: {sum}");
        }
    }
}
