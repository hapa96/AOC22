
namespace AOC22
{
    public class Day02 : BaseDay
    {
        protected override void DoRunPart01()
        {
            var totalScore = 0;
            foreach (var line in InputData)
            {
                var opponentWeapon = line[0];
                var myWeapon = line[2];
                int outcome = (opponentWeapon, myWeapon) switch
                {
                    ('A', 'X') => 4,
                    ('A', 'Y') => 8,
                    ('A', 'Z') => 3,
                    ('B', 'X') => 1,
                    ('B', 'Y') => 5,
                    ('B', 'Z') => 9,
                    ('C', 'X') => 7,
                    ('C', 'Y') => 2,
                    ('C', 'Z') => 6,
                    _ => throw new ArgumentOutOfRangeException()
                };
                totalScore += outcome;
            }

            Console.WriteLine($"TotalScore: {totalScore}");
        }

        protected override void DoRunPart02()
        {
            var totalScore = 0;
            foreach (var line in InputData)
            {
                var opponentWeapon = line[0];
                var myWeapon = line[2];
                int outcome = (opponentWeapon, myWeapon) switch
                {
                    ('A', 'X') => 3,
                    ('A', 'Y') => 4,
                    ('A', 'Z') => 8,
                    ('B', 'X') => 1,
                    ('B', 'Y') => 5,
                    ('B', 'Z') => 9,
                    ('C', 'X') => 2,
                    ('C', 'Y') => 6,
                    ('C', 'Z') => 7,
                    _ => throw new ArgumentOutOfRangeException()
                };
                totalScore += outcome;
            }

            Console.WriteLine($"TotalScore: {totalScore}");
        }
    }
}
