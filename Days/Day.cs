using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public abstract class Day
    {
        public abstract Task PerformCalculations(string inputFile);
        
        public void CalculateAndLogTime(Action calc)
        {
            Stopwatch sw = Stopwatch.StartNew();
            calc.Invoke();

            sw.Stop();
            Console.WriteLine($"Elapsed miliseconds: {sw.ElapsedMilliseconds}");
        }
    }
}
