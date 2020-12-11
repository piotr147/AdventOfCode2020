using AdventOfCode2020.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day3 : Day
    {
        private const int DESIRED_SUM = 2020;

        public override async Task PerformCalculations(string inputFile)
        {
            List<string> listInput = await FileHelper.GetStringListFromFile(inputFile);
            bool[,] input = PrepareInput(listInput);

            Move[] input1 = new Move[] { new Move(3, 1) };
            Move[] input2 = new Move[]
            {
                new Move(1, 1),
                new Move(3, 1),
                new Move(5, 1),
                new Move(7, 1),
                new Move(1, 2)
            };

            CalculateAndLogTime(() =>
            {
                long result = CalculateResult1(input, input1);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result}");
            });

            CalculateAndLogTime(() =>
            {
                long result = CalculateResult1(input, input2);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result}");
            });

            Console.WriteLine();
        }

        private long CalculateResult1(bool[,] input, Move[] moves)
        {
            int maxRow = input.GetLength(0);
            int maxCol = input.GetLength(1);
            long result = 1;

            for (int i = 0; i < moves.Length; ++i)
            {
                int trees = 0;
                int col = 0;

                for (int row = 0; row < maxRow; row += moves[i].rowMove)
                {
                    if (input[row, col])
                    {
                        ++trees;
                    }

                    col = (col + moves[i].colMove) % maxCol;
                }

                result *= trees;
            }

            return result;
        }

        private int CalculateResult2(int[] input)
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

        private bool[,] PrepareInput(List<string> listInput)
        {
            bool[,] input = new bool[listInput.Count, listInput[0].Length];

            for (int i = 0; i < listInput.Count; ++i)
            {
                for (int j = 0; j < listInput[0].Length; ++j)
                {
                    input[i, j] = listInput[i][j] == '#';
                }
            }

            return input;
        }

        private struct Move
        {
            public int colMove;
            public int rowMove;

            public Move(int c, int r)
            {
                colMove = c;
                rowMove = r;
            }
        }
    }
}
