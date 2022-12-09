
namespace AOC22
{
    public class Day08: BaseDay
    {
        private const int MaxWidth = 98;

        public List<(int,int,int)> ParseInputToMap()
        { 
            var map = new List<(int x, int y, int value)>();
            for (int y = 0; y < InputData.Count; y++)
            {
                var line = InputData[y];
                for (int x = 0; x < line.Length; x++)
                {
                    map.Add((x, y, int.Parse(line[x].ToString())));
                }

            }

            return map;
        }

        public bool TreeIsVisible(List<(int x, int y, int value)> treeMap, (int x,int y,int value) tree)
        {
            //check left
            var isVisibleFromLeft = treeMap.Any((map => map.x < tree.x && map.y == tree.y && map.value >= tree.value));
            if (!isVisibleFromLeft) return true;
            //check right
            var isVisibleFromRight = treeMap.Any(map => map.x > tree.x && map.y == tree.y && map.value >= tree.value);
            if (!isVisibleFromRight) return true;
            //check top
            var isVisibleFromTop = treeMap.Any(map => map.x == tree.x && map.y < tree.y && map.value >= tree.value);
            if (!isVisibleFromTop) return true;
            //check bottom
            var isVisibleFromBottom = treeMap.Any(map => map.x == tree.x && map.y > tree.y && map.value >= tree.value);
            if (!isVisibleFromBottom) return true;

            return false;
        }

        public int CalculateTreeScienceScore(List<(int x, int y, int value)> treeMap, (int x, int y, int value) treeOfInterest)
        {
            //calculate left
            var leftScore = 0;
            var i = 1;
            var currentTree = treeMap.Where(e => e.x == treeOfInterest.x - i && e.y == treeOfInterest.y && e.value < treeOfInterest.value).ToList();

            while (currentTree.Any())
            {
                leftScore++;
                i++;
                currentTree = treeMap.Where(e => e.x == treeOfInterest.x - i && e.y == treeOfInterest.y && e.value < treeOfInterest.value).ToList();
            }

            if (treeOfInterest.x > 0 && treeOfInterest.x - i >= 0) leftScore++;

            //calculate right
            var rightScore = 0; 
            i = 1;
            currentTree = treeMap.Where(e => e.x == treeOfInterest.x + i && e.y == treeOfInterest.y && e.value < treeOfInterest.value).ToList();
            while (currentTree.Any())
            {
                rightScore++;
                i++;
                currentTree = treeMap.Where(e => e.x == treeOfInterest.x + i && e.y == treeOfInterest.y && e.value < treeOfInterest.value).ToList();
            }

            if (treeOfInterest.x < MaxWidth && treeOfInterest.x + i <= MaxWidth) rightScore++;
            
            //calculate top
            var topScore = 0;
            i = 1;
            currentTree = treeMap.Where(e => e.x == treeOfInterest.x && e.y == treeOfInterest.y - i && e.value < treeOfInterest.value).ToList();
            while (currentTree.Any())
            {
                topScore++;
                i++;
                currentTree = treeMap.Where(e => e.x == treeOfInterest.x && e.y == treeOfInterest.y - i && e.value < treeOfInterest.value).ToList();
            }

            if (treeOfInterest.y > 0 && treeOfInterest.y - i >= 0) topScore++;

            //calculate bottom
            var bottomScore = 0;
            i = 1;
            currentTree = treeMap.Where(e => e.x == treeOfInterest.x && e.y == treeOfInterest.y + i && e.value < treeOfInterest.value).ToList();
            while (currentTree.Any())
            {
                bottomScore++;
                i++;
                currentTree = treeMap.Where(e => e.x == treeOfInterest.x && e.y == treeOfInterest.y + i && e.value < treeOfInterest.value).ToList();
            }

            if (treeOfInterest.y < MaxWidth && treeOfInterest.y + i <= MaxWidth) bottomScore++;


            return bottomScore * topScore * rightScore * leftScore;
        }
        protected override void DoRunPart01()
        {
            var map = ParseInputToMap();
            var results = map.Select(x => TreeIsVisible(map, x)).Count(x => x);
            Console.WriteLine(results);
        }

        protected override void DoRunPart02()
        {
            var map = ParseInputToMap();
            var highestTreeScienceScore = map.Select(x => CalculateTreeScienceScore(map, x)).Max();
            Console.WriteLine(highestTreeScienceScore);
        }
    }
}
