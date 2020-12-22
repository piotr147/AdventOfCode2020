using AdventOfCode2020.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day10 : Day
    {
        private const int MAX_DIFF_ALLOWED = 3;

        public override async Task PerformCalculations(string inputFile)
        {
            List<int> listInput = await FileHelper.GetIntListFromFile(inputFile);
            int[] input = listInput.ToArray();

            CalculateAndLogTime(() =>
            {
                int result1 = CalculateResult1(input);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result1}");
            });

            CalculateAndLogTime(() =>
            {
                long result2 = CalculateResult2(input);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result2}");
            });

            Console.WriteLine();
        }

        private static long CalculateResult2(int[] input)
        {
            int[] sorted = input.OrderBy(i => i).ToArray();
            sorted = AppendChargingOutletAndDevice(sorted);

            long[] ways = new long[sorted.Length];
            ways[0] = 1;

            for (int i = 0; i < ways.Length - 1; ++i)
            {
                for(int j = i + 1; j < ways.Length && sorted[j] - sorted[i] <= MAX_DIFF_ALLOWED; ++j)
                {
                    ways[j] += ways[i];
                }
            }

            return ways[ways.Length - 1];
        }

        private static int RemoveAdapterAndCountAllOptions(int[] sorted, int index)
        {
            int[] reducedChain = CopyChainToNewReducedChain(sorted, index);

            if (!ChainIsValid(reducedChain))
            {
                return 0;
            }

            int childOptions = 0;

            for (int i = index + 1; i < reducedChain.Length - 1; ++i)
            {
                childOptions += RemoveAdapterAndCountAllOptions(reducedChain, i);
            }

            //PrintChain(reducedChain);
            return childOptions + 1;
        }

        private static void PrintChain(int[] reducedChain)
        {
            Console.Write("[ ");
            foreach(var i in reducedChain)
            {
                Console.Write($"{i}, ");

            }

            Console.Write(" ]\n");
        }

        private static int[] CopyChainToNewReducedChain(int[] sorted, int index)
        {
            int[] copy = new int[sorted.Length - 1];

            for (int i = 0; i < index; ++i)
            {
                copy[i] = sorted[i];
            }

            for (int i = index + 1; i < sorted.Length; ++i)
            {
                copy[i - 1] = sorted[i];
            }

            return copy;
        }

        private static int CalculateResult1(int[] input)
        {
            int[] sorted = input.OrderBy(i => i).ToArray();
            sorted = AppendChargingOutletAndDevice(sorted);

            int ones = FindNumberOfJoltageDifferencesEqualTo(sorted, 1);
            int threes = FindNumberOfJoltageDifferencesEqualTo(sorted, 3);

            return ones * threes;
        }

        private static int FindNumberOfJoltageDifferencesEqualTo(int[] sortedInput, int diff)
        {
            int numberOfDiffs = 0;

            for (int i = 1; i < sortedInput.Length; ++i)
            {
                if (sortedInput[i] - sortedInput[i - 1] == diff)
                {
                    ++numberOfDiffs;
                }
            }

            return numberOfDiffs;
        }

        private static int[] AppendChargingOutletAndDevice(int[] sortedInput)
        {
            int[] result = new int[sortedInput.Length + 2];

            sortedInput.CopyTo(result, 1);
            result[result.Length - 1] = result[result.Length - 2] + MAX_DIFF_ALLOWED;

            return result;
        }

        private static bool ChainIsValid(int[] chain)
        {
            for(int i = 1; i < chain.Length; ++i)
            {
                if (chain[i] - chain[i - 1] > MAX_DIFF_ALLOWED)
                    return false;
            }

            return true;
        }

        private static bool CanAdaptersBeConnected(int adapter1, int adapter2) =>
            adapter1 - adapter2 <= MAX_DIFF_ALLOWED || adapter2 - adapter1 <= MAX_DIFF_ALLOWED;
    }
}
