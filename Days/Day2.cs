using AdventOfCode2020.Utilities;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day2 : Day
    {
        private const int DESIRED_SUM = 2020;

        public override async Task PerformCalculations(string inputFile)
        {
            List<string> listInput = await FileHelper.GetStringListFromFile(inputFile);
            PasswordWithPolicy[] input = PrepareInput(listInput);

            CalculateAndLogTime(() =>
            {
                int result1 = CalculateResult(input, IsCorrect);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result1}");
            });

            CalculateAndLogTime(() =>
            {
                int result2 = CalculateResult(input, IsCorrect2);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result2}");
            });

            Console.WriteLine();
        }

        private int CalculateResult(PasswordWithPolicy[] input, Func<PasswordWithPolicy, bool> Correct)
        {
            int corrects = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                if (Correct(input[i]))
                {
                    ++corrects;
                }
            }

            return corrects;
        }

        private bool IsCorrect(PasswordWithPolicy policy)
        {
            int letterOccurences = 0;

            for (int i = 0; i < policy.password.Length; ++i)
            {
                if (policy.password[i] == policy.letter)
                {
                    ++letterOccurences;

                    if (letterOccurences > policy.num2)
                        return false;
                }
            }

            return letterOccurences >= policy.num1;
        }

        private bool IsCorrect2(PasswordWithPolicy policy) =>
            policy.password[policy.num1 - 1] == policy.letter
                ^ policy.password[policy.num2 - 1] == policy.letter;

        private PasswordWithPolicy[] PrepareInput(List<string> listInput)
        {
            var result = new PasswordWithPolicy[listInput.Count];

            for (int i = 0; i < listInput.Count; ++i)
            {
                int colonIndex = listInput[i].IndexOf(':');
                int dashIndex = listInput[i].IndexOf('-');

                result[i] = new PasswordWithPolicy(
                    int.Parse(listInput[i].Substring(0, dashIndex)),
                    int.Parse(listInput[i].Substring(dashIndex + 1, colonIndex - dashIndex - 2)),
                    listInput[i][colonIndex - 1],
                    listInput[i].Substring(colonIndex + 2));
            }

            return result;
        }

        private struct PasswordWithPolicy
        {
            public int num1;
            public int num2;
            public char letter;
            public string password;

            public PasswordWithPolicy(int n1, int n2, char l, string p)
            {
                num1 = n1;
                num2 = n2;
                letter = l;
                password = p;
            }
        }

    }
}
