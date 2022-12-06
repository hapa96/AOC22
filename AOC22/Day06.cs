
namespace AOC22
{
    public class Day06 : BaseDay
    {
        public void IsUnique(string str, int n)
        {
            for (int i = 0; i < str.Length; i++)
            {
                var numberOfDistinctChars = str.Substring(i, n).Distinct().Count();
                if (numberOfDistinctChars == n)
                {
                    Console.WriteLine(i + n);
                    return;
                }
            }
        }

        protected override void DoRunPart01()
        {
            IsUnique(InputData[0], 4);
        }

        protected override void DoRunPart02()
        {
            IsUnique(InputData[0], 14);
        }
    }
}
