using AdventOfCode2020.Days;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Program
    {
        private static string _inputsPath = "./Inputs";
        static async Task Main(string[] args)
        {
            SetInputsPath(args);

            while (true)
            {
                Console.Write("Enter day number or 0 to exit: ");

                if (!int.TryParse(Console.ReadLine(), out int day))
                {
                    Console.WriteLine("Valid number is expected");
                    continue;
                }

                if (day == 0)
                    return;

                if (!_days.ContainsKey(day))
                {
                    Console.WriteLine("Day number is not correct or has not been resolved yet");
                    continue;
                }

                await TrySolveDay(day);
            }
        }

        private static void SetInputsPath(string[] args)
        {
            if (args.Length > 0)
                _inputsPath = args[0];
        }

        private async static Task TrySolveDay(int day)
        {
            try
            {
                await _days[day].PerformCalculations($"{_inputsPath}/Day{day}.txt");
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e) when (e is DirectoryNotFoundException || e is FileNotFoundException)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static Dictionary<int, Day> _days = new Dictionary<int, Day>
        {
            [1] = new Day1(),
            [2] = new Day2(),
            [3] = new Day3(),
            [4] = new Day4(),
            [5] = new Day5()
        };
    }
}
