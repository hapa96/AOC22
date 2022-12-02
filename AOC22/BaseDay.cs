using System.Diagnostics;

namespace AOC22
{
    public abstract class BaseDay
    {
    protected List<string> InputData { get; set; }
    private readonly Stopwatch _timer = new();

        protected BaseDay()
        {
            InputData = File.ReadAllLines($"Input/{GetType().Name}").ToList();
        }

        public void RunPart01()
        {
            Console.WriteLine("**************************************************");
            _timer.Start();
            DoRunPart01();
            _timer.Stop();
            Console.WriteLine($"Time for Part01:  {_timer.ElapsedMilliseconds} ms");
            Console.WriteLine("************************************************** \n");

        }

        protected abstract void DoRunPart01();

        public void RunPart02()
        {
            Console.WriteLine("**************************************************");
            _timer.Start();
            DoRunPart02();
            _timer.Stop();
            Console.WriteLine($"Time for Part02:  {_timer.ElapsedMilliseconds} ms");
            Console.WriteLine("************************************************** \n");

        }

        protected abstract void DoRunPart02();

    }
}
