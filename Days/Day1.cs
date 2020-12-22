using AdventOfCode2020.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day1: Day
    {
        private const int DESIRED_SUM = 2020;

        public override async Task PerformCalculations(string inputFile)
        {
            List<int> listInput = await FileHelper.GetIntListFromFile(inputFile);
            int[] input = listInput.ToArray();

            CalculateAndLogTime(() =>
            {
                int result = CalculateResult1(input);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result}");
            });

            CalculateAndLogTime(() =>
            {
                int result = CalculateResult2(input);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result}");
            });

            Console.WriteLine();
        }

        private static int CalculateResult1(int[] input)
        {
            for (int i = 0; i < input.Length; ++i)
            {
                int numberToFind = DESIRED_SUM - input[i];
                
                for(int j = i + 1; j < input.Length; ++j)
                {
                    if (input[j] == numberToFind)
                        return input[i] * input[j];
                }
            }

            return -1;
        }

        private static int CalculateResult2(int[] input)
        {
            for (int i = 0; i < input.Length; ++i)
            {
                int sumToFind = DESIRED_SUM - input[i];

                for (int j = i + 1; j < input.Length; ++j)
                {
                    if (input[j] > sumToFind)
                        continue;

                    int numberToFind = sumToFind - input[j];

                    for (int k = j + 1; k < input.Length; ++k)
                    {
                        if (input[k] == numberToFind)
                            return input[i] * input[j] * input[k];
                    }
                }
            }

            return -1;
        }
    }
}
