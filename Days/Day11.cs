using AdventOfCode2020.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day11 : Day
    {
        private const int PART1_NUMBER_WHEN_PEOPLE_LEAVE = 4;
        private const int PART2_NUMBER_WHEN_PEOPLE_LEAVE = 5;
        private static readonly Dictionary<char, Position> fileInputDictionary = new Dictionary<char, Position>
        {
            ['.'] = Position.Floor,
            ['L'] = Position.Empty
        };

        public override async Task PerformCalculations(string inputFile)
        {
            List<string> listInput = await FileHelper.GetStringListFromFile(inputFile);
            Position[,] input = PrepareInput(listInput);

            CalculateAndLogTime(() =>
            {
                int result1 = CalculateResult1(input, PART1_NUMBER_WHEN_PEOPLE_LEAVE, NumberOfOccupiedAdjacents);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result1}");
            });

            CalculateAndLogTime(() =>
            {
                int result2 = CalculateResult1(input, PART2_NUMBER_WHEN_PEOPLE_LEAVE, NumberOfOccupiedSeenAdjacents);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result2}");
            });

            Console.WriteLine();
        }

        private static int CalculateResult1(Position[,] input, int numberWhenPeopleLeave, Func<Position[,], int, int, int> numberOfAdjacents)
        {
            Position[,] before = input.Clone() as Position[,];
            Position[,] after = input.Clone() as Position[,];

            //Console.WriteLine("start");
            //PrintPosition(before);
            //Console.WriteLine();

            do
            {
                before = after.Clone() as Position[,];
                after = SimulateRound(before, numberWhenPeopleLeave, numberOfAdjacents);
                //Console.WriteLine("before");
                //PrintPosition(before);
                //Console.WriteLine("after");
                //PrintPosition(after);
                //Console.WriteLine();
            } while (!PositionsEqual(before, after));

            return after.Cast<Position>().Count(p => p == Position.Occupied);
        }

        private static void PrintPosition(Position[,] pos)
        {
            for (int i = 0; i < pos.GetLength(0); ++i)
            {
                for (int j = 0; j < pos.GetLength(1); ++j)
                {
                    char c = pos[i, j] == Position.Floor
                        ? '.'   
                        : pos[i, j] == Position.Empty
                            ? 'L'
                            : '#';

                    Console.Write(c);
                }
                Console.Write("\n");
            }
        }

        private static Position[,] SimulateRound(Position[,] before, int numberWhenPeopleLeave, Func<Position[,], int, int, int> numberOfAdjacents)
        {
            Position[,] after = before.Clone() as Position[,];

            for (int i = 0; i < before.GetLength(0); ++i)
            {
                for (int j = 0; j < before.GetLength(1); ++j)
                {
                    if (before[i, j] == Position.Floor)
                        continue;

                    int adjacents = numberOfAdjacents(before, i, j);

                    if(adjacents >= numberWhenPeopleLeave)
                    {
                        after[i, j] = Position.Empty;
                    }

                    if (adjacents == 0)
                    {
                        after[i, j] = Position.Occupied;
                    }
                }
            }

            return after;
        }

        private static int NumberOfOccupiedAdjacents(Position[,] before, int i, int j)
        {
            int adjacents = 0;

            for(int ii = i - 1; ii <= i + 1; ++ii)
            {
                for (int jj = j - 1; jj <= j + 1; ++jj)
                {
                    if (i == ii && j == jj)
                        continue;

                    if(ii >= 0 && ii < before.GetLength(0)
                        && jj >= 0 && jj < before.GetLength(1)
                        && before[ii, jj] == Position.Occupied)
                    {
                        ++adjacents;
                    }
                }
            }

            return adjacents;
        }

        private static int NumberOfOccupiedSeenAdjacents(Position[,] before, int i, int j)
        {
            int adjacents = 0;

            if(IsSeatOccupiedSeenInDirection(before, i, j, i => i - 1, j => j - 1))
            {
                ++adjacents;
            }

            if (IsSeatOccupiedSeenInDirection(before, i, j, i => i - 1, j => j))
            {
                ++adjacents;
            }

            if (IsSeatOccupiedSeenInDirection(before, i, j, i => i - 1, j => j + 1))
            {
                ++adjacents;
            }

            if (IsSeatOccupiedSeenInDirection(before, i, j, i => i, j => j - 1))
            {
                ++adjacents;
            }

            if (IsSeatOccupiedSeenInDirection(before, i, j, i => i, j => j + 1))
            {
                ++adjacents;
            }

            if (IsSeatOccupiedSeenInDirection(before, i, j, i => i + 1, j => j - 1))
            {
                ++adjacents;
            }

            if (IsSeatOccupiedSeenInDirection(before, i, j, i => i + 1, j => j))
            {
                ++adjacents;
            }

            if (IsSeatOccupiedSeenInDirection(before, i, j, i => i + 1, j => j + 1))
            {
                ++adjacents;
            }

            return adjacents;
        }

        private static bool IsSeatOccupiedSeenInDirection(Position[,] before, int i, int j, Func<int, int> dirI, Func<int, int> dirJ)
        {
            while(true)
            {
                i = dirI(i);
                j = dirJ(j);

                if(i < 0 || i >= before.GetLength(0)
                    || j < 0 || j >= before.GetLength(1))
                    return false;

                if (before[i, j] == Position.Empty)
                    return false;

                if (before[i, j] == Position.Occupied)
                    return true;
            }
        }

        private static Position[,] PrepareInput(List<string> listInput)
        {
            Position[,] result = new Position[listInput.Count, listInput[0].Length];

            for (int i = 0; i < listInput.Count; ++i)
            {
                for (int j = 0; j < listInput[i].Length; ++j)
                {
                    result[i, j] = fileInputDictionary[listInput[i][j]];
                }
            }

            return result;
        }

        private static bool PositionsEqual(Position[,] p1, Position[,] p2) =>
            Enumerable.SequenceEqual(p1.Cast<Position>(), p2.Cast<Position>());

        private enum Position
        {
            Floor,
            Empty,
            Occupied
        }
    }
}
