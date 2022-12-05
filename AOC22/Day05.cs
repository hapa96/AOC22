using System.Text.RegularExpressions;

namespace AOC22
{
    public class Day05 : BaseDay
    {

        protected override void DoRunPart01()
        {
            var stackList = CreateStackListFromInput();
            List<(int quantity, int from, int to)> rearrangementList = CreateRearrangementList();
            foreach (var rearrange in rearrangementList)
            {
                for (int i = 0; i < rearrange.quantity; i++)
                {
                    stackList[rearrange.to - 1].Push( stackList[rearrange.from - 1].Pop());
                }
            }

            var res = stackList.Select(x => (x.Pop())).ToArray();
            Console.WriteLine(res);


        }

        protected override void DoRunPart02()
        {
            var stackList = CreateStackListFromInput();
            List<(int quantity, int from, int to)> rearrangementList = CreateRearrangementList();
            foreach (var rearrange in rearrangementList)
            {
                if (rearrange.quantity == 1)
                {
                    stackList[rearrange.to - 1].Push(stackList[rearrange.from - 1].Pop());
                }
                else
                {
                    List<char> charList = new List<char>();
                    for (int i = 0; i < rearrange.quantity; i++)
                    {
                        charList.Add(stackList[rearrange.from - 1].Pop());
                    }
                    for (int i = rearrange.quantity-1; i >=0; i--)
                    {
                        stackList[rearrange.to - 1].Push(charList[i]);
                    }
                }
            }

            var res = stackList.Select(x => (x.Pop())).ToArray();
            Console.WriteLine(res);

        }

        public List<(int, int, int)> CreateRearrangementList()
        {
            var list = new List<(int quantity, int from, int to)>();
            var pattern = @"move (\d+) from (\d+) to (\d+)";
            foreach (var line in InputData)
            {
                var match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    list.Add((quantity: int.Parse(match.Groups[1].Value), from: int.Parse(match.Groups[2].Value), to: int.Parse(match.Groups[3].Value)));
                }
                
            }
            return list;
        }

        public List<Stack<char>> CreateStackListFromInput()
        {
            var list = new List<string>();
            for (int i = 0; i < 9; i++)
            {
                list.Add("");
            }
            foreach (var line in InputData.Where(x => x.Contains('[')))
            {
                for (int i = 1; i < line.Length; i += 4)
                {
                    if (line[i] != ' ')
                    {
                        list[(i - 1) / 4] += line[i];
                    }

                }
            }
            return list.Select(x => new Stack<char>(x.Reverse())).ToList();
        }
    }

   
}
            