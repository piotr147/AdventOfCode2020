using AdventOfCode2020.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day5 : Day
    {
        public override async Task PerformCalculations(string inputFile)
        {
            List<string> listInput = await FileHelper.GetStringListFromFile(inputFile);
            string[] input = listInput.ToArray();

            CalculateAndLogTime(() =>
            {
                int result1 = CalculateResult1(input);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result1}");
            });

            CalculateAndLogTime(() =>
            {
                int result2 = CalculateResult2(input);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result2}");
            });

            Console.WriteLine();
        }

        private static int CalculateResult1(string[] input)
        {
            int max = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                int seatNr = CalculateSeat(input[i]);
                max = seatNr > max ? seatNr : max;
            }

            return max;
        }

        private static int CalculateResult2(string[] input)
        {
            bool[] seats = new bool[1024];
            bool frontPassed = false;

            for (int i = 0; i < input.Length; ++i)
            {
                seats[CalculateSeat(input[i])] = true;
            }

            for (int i = 0; i < seats.Length; ++i)
            {
                if(!frontPassed && seats[i])
                {
                    frontPassed = true;
                }

                if(frontPassed && !seats[i])
                {
                    return i;
                }
            }

            return -1;
        }

        private static int CalculateSeat(string seat)
        {
            int row = CalculateRow(seat.Substring(0, 7));
            int col = CalculateCol(seat.Substring(7, 3));

            return row * 8 + col;
        }

        private static int CalculateRow(string r)
        {
            string bin = r.Replace('F', '0').Replace('B', '1');

            return Convert.ToInt32(bin, 2);
        }

        private static int CalculateCol(string r)
        {
            string bin = r.Replace('L', '0').Replace('R', '1');

            return Convert.ToInt32(bin, 2);
        }
    }
}
