
using System.Text.RegularExpressions;
namespace AOC22
{
    public class Day09 : BaseDay
    {
        private const string InputPattern = @"([A-Z]) (\d+)";

        public List<(string direction, int value)> MapInput()
        {
            var list = new List<(string direction, int value)>();
            foreach (var line in InputData)
            {
                var match = Regex.Match(line, InputPattern);
                if (match.Success)
                {
                    list.Add((match.Groups[1].Value, int.Parse(match.Groups[2].Value)));
                }
            }

            return list;
        }

        protected override void DoRunPart01()
        {
            var map = MapInput();
            var numberOfVisitedTailPositions = CalculateVisitedPositions(map,2);
            Console.WriteLine(numberOfVisitedTailPositions);
        }

        protected override void DoRunPart02()
        {
            var map = MapInput();
            var numberOfVisitedTailPositions = CalculateVisitedPositions(map, 10);
            Console.WriteLine(numberOfVisitedTailPositions);
        }

        public int CalculateVisitedPositions(List<(string direction, int value)> moveCommands, int numberOfKnots)
        {
            var positionList = Enumerable.Repeat((x: 0, y: 0), numberOfKnots).ToList();
            var visitedPositionSet = new HashSet<(int x, int y)>();

            foreach (var moveCommand in moveCommands)
            {
                for (int i = 0; i < moveCommand.value; i++)
                {
                    //Move Head
                    positionList[0] = moveCommand.direction switch
                    {
                        "U" => (positionList[0].x, positionList[0].y + 1),
                        "D" => (positionList[0].x, positionList[0].y - 1),
                        "L" => (positionList[0].x - 1, positionList[0].y),
                        "R" => (positionList[0].x + 1, positionList[0].y),
                        _ => positionList[0]
                    };

                    //Move Tails
                    for (int j = 0; j < numberOfKnots -1; j++)
                    {
                        var firstKnot = positionList[j];
                        var secondKnot = positionList[j + 1];

                        var diffX = Math.Abs(firstKnot.x - secondKnot.x);
                        var diffY = Math.Abs(firstKnot.y - secondKnot.y);

                        if (diffX < 2 && diffY < 2)
                        {
                            // If Knots are touching, no movement necessary
                            continue;
                        }

                        if (diffX > 1 && diffY == 0)
                        {
                            secondKnot.x += firstKnot.x - secondKnot.x > 0 ? 1 : -1;
                        }
                        else if (diffY > 1 && diffX == 0)
                        {
                            secondKnot.y += firstKnot.y - secondKnot.y > 0 ? 1 : -1;
                        }
                        else
                        {
                            secondKnot.x += firstKnot.x - secondKnot.x > 0 ? 1 : -1;
                            secondKnot.y += firstKnot.y - secondKnot.y > 0 ? 1 : -1;
                        }
                        positionList[j + 1] = secondKnot;
                    }

                    visitedPositionSet.Add(positionList[^1]);
                }

            }
            return visitedPositionSet.Count;
        }

    }
}
