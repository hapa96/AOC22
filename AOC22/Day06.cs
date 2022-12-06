
namespace AOC22
{
    public class Day06 : BaseDay
    {
        public void FindUniqueSubstring(string str, int lengthOfString)
        {
            for (int i = 0; i < str.Length; i++)
            {
                var numberOfDistinctChars = str.Substring(i, lengthOfString).Distinct().Count();
                if (numberOfDistinctChars == lengthOfString)
                {
                    Console.WriteLine(i + lengthOfString);
                    return;
                }
            }
        }

        protected override void DoRunPart01()
        {
            FindUniqueSubstring(InputData[0], 4);
        }

        protected override void DoRunPart02()
        {
            FindUniqueSubstring(InputData[0], 14);
        }
    }
}
