using AdventOfCode2020.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day6 : Day
    {
        public override async Task PerformCalculations(string inputFile)
        {
            List<string> listInput = await FileHelper.GetStringListFromFile(inputFile);
            string[][] input = PrepareInput(listInput);

            CalculateAndLogTime(() =>
            {
                int result1 = CalculateResult(input, CountSumAnyone);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result1}");
            });

            CalculateAndLogTime(() =>
            {
                int result2 = CalculateResult(input, CountSumEveryone);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result2}");
            });

            Console.WriteLine();
        }

        private int CalculateResult(string[][] input, Func<string[], int> calculateSum) =>
            input.Sum(i => calculateSum.Invoke(i));

        private int CountSumAnyone(string[] groupAnswers)
        {
            int groupSum = 0;
            string answers = string.Empty;
            
            for(int i = 0; i < groupAnswers.Length; ++i)
            {
                for(int j = 0; j < groupAnswers[i].Length; ++j)
                {
                    if (answers.Contains(groupAnswers[i][j]))
                        continue;

                    answers += groupAnswers[i][j];
                    ++groupSum;
                }
            }

            return groupSum;
        }

        private int CountSumEveryone(string[] groupAnswers)
        {
            string answers = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < groupAnswers.Length; ++i)
            {
                for (int j = 0; j < answers.Length; ++j)
                {
                    if (groupAnswers[i].Contains(answers[j]))
                        continue;

                    answers = answers.Remove(j, 1);
                    --j;
                }
            }

            return answers.Length;
        }

        private string[][] PrepareInput(List<string> listInput)
        {
            List<List<string>> result = new List<List<string>>();
            List<string> newList = new List<string>();

            for (int i = 0; i < listInput.Count; ++i)
            {
                if (string.IsNullOrWhiteSpace(listInput[i]))
                {
                    result.Add(newList);
                    newList = new List<string>();
                    continue;
                }

                newList.Add(listInput[i]);
            }

            if (newList.Count > 0)
            {
                result.Add(newList);
            }

            return result.Select(l => l.ToArray()).ToArray();
        }
    }
}
