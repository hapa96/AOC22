
namespace AOC22
{
    public class Day01 : BaseDay
    {

        public List<int> ParseInput()
        {
            var list = new List<int>();
            var tmp = 0;
            foreach (string line in InputData)
            {
                if (line != "")
                {
                    tmp += int.Parse(line);
                }
                else
                {
                    list.Add(tmp);
                    tmp = 0;
                }
            }
            list.Sort();
            return list;
        }





        protected override void DoRunPart01()
        {
            var list = ParseInput();
            Console.WriteLine("Part01: " + list[^1]);
        }

        protected override void DoRunPart02()
        {
            var list = ParseInput();
            Console.WriteLine("Part02: " + (list[^1] + list[^2] + list[^3]));
        }
    }
}
