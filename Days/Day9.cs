using AdventOfCode2020.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day9 : Day
    {
        public override async Task PerformCalculations(string inputFile)
        {
            List<string> listInput = await FileHelper.GetStringListFromFile(inputFile);
            long[] input = PrepareInput(listInput);
            long result1 = 0;

            CalculateAndLogTime(() =>
            {
                result1 = FindFirstImproperNumber(input, 25);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result1}");
            });

            CalculateAndLogTime(() =>
            {
                long result2 = FindEncryptionWeakness(input, result1);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result2}");
            });

            Console.WriteLine();
        }

        private static long FindEncryptionWeakness(long[] input, long sum)
        {
            long currentSum = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                for (int j = i; j < input.Length; ++j)
                {
                    currentSum += input[j];

                    if (currentSum == sum)
                    {
                        IEnumerable<long> foundSet = input.Skip(i).SkipLast(input.Length - j - 1);

                        return foundSet.Min() + foundSet.Max();
                    }

                    if(currentSum > sum)
                    {
                        break;
                    }
                }

                currentSum = 0;
            }

            return -1;
        }

        private static long FindFirstImproperNumber(long[] input, int preamble)
        {
            List<long> preambleList = new List<long>(input.Take(preamble));
            long numberToCheck = input[preamble++];

            while (IsNumberValid(preambleList, numberToCheck))
            {
                preambleList.RemoveAt(0);
                preambleList.Add(numberToCheck);
                numberToCheck = input[preamble++];
            }

            return numberToCheck;
        }

        private static bool IsNumberValid(List<long> preambleList, long numberToCheck)
        {
            
            for(int i = 0; i < preambleList.Count; ++i)
            {
                for (int j = i + 1; j < preambleList.Count; ++j)
                {
                    if (preambleList[i] + preambleList[j] == numberToCheck)
                        return true;
                }
            }

            return false;
        }

        private static long[] PrepareInput(List<string> listInput) =>
            listInput.Select(i => long.Parse(i)).ToArray();
    }
}
