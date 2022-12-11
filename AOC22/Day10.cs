
using System.Text.RegularExpressions;
using static AOC22.Day10;

namespace AOC22
{
    public  class Day10 : BaseDay
    {
        private const string Pattern = @"addx (-?\d+)";
        private const string LitPixel = "#";
        private const string DarkPixel = ".";
        private const int CrtWidth = 40;

        protected override void DoRunPart01()
        {
            var sum = 0;
            var cycle = 0;
            var instructionList = CreateMap();

            var startInstruction = new Instruction(0, 1);
            instructionList.Add(startInstruction);
            
            for (int i = 0; i < 240; i++)
            {
                cycle++;

                if (cycle is 20 or 60 or 100 or 140 or 180 or 220)
                {
                    var value = instructionList.Where(x => x.Cycle == 0).Sum(x => x.Value);
                    sum += (cycle * value);
                }
                instructionList.FirstOrDefault(x => x.Cycle != 0)!.Cycle = instructionList.FirstOrDefault(x => x.Cycle != 0)!.Cycle - 1;
            }

            Console.WriteLine(sum);
        }

        public List<Instruction> CreateMap()
        {
            var map = new List<Instruction>();
            foreach (var line in InputData)
            {
                var match = Regex.Match(line, Pattern);
                map.Add(match.Success ? new Instruction(2, int.Parse(match.Groups[1].Value)) : new Instruction(1, 0));
            }

            return map;
        }

        public int CalculateRegister(List<Instruction> instructionList, int cycle)
        {
            instructionList.FirstOrDefault(x => x.Cycle != 0)!.Cycle = instructionList.FirstOrDefault(x => x.Cycle != 0)!.Cycle - 1;
            return instructionList.Where(x => x.Cycle == 0).Sum(x => x.Value);
        }

        protected override void DoRunPart02()
        {
            var startInstruction = new Instruction(0, 1);
            var instructionList = CreateMap();
            instructionList.Add(startInstruction);
            var crtDisplay = new CrtDisplay();
            for (int i = 0; i < 240; i++)
            {
                crtDisplay.DrawPixel(i);
                var registerValue = CalculateRegister(instructionList, i);
                crtDisplay.SpritePosition = registerValue;
            }
            crtDisplay.DrawContent();
        }



        public class CrtDisplay
        {
            public string[] Content = new string[240];
            public int SpritePosition { get; set; }


            public CrtDisplay()
            {
                SpritePosition = 1;
            }
            public void DrawContent()
            {
                var result =  string.Join("\n", Content.Chunk(CrtWidth).Select(x => string.Join("", x)));
                Console.WriteLine($"\n{result} \n");
            }

            public void DrawPixel(int cycle)
            {
                var cycleValue = cycle % CrtWidth;
                if (Math.Abs(SpritePosition - cycleValue) < 2 )
                {
                    
                    Content[cycle] = LitPixel;
                }
                else
                {
                    Content[cycle] = DarkPixel;
                }
            }
        }

        public class Instruction
        {
            public int Cycle { get; set; }
            public int Value { get; set; }

            public Instruction(int cycle, int value)
            {
                Cycle = cycle;
                Value = value;
            }
        }


    }
}
