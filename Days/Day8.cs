using AdventOfCode2020.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day8 : Day
    {
        private const string ACC = "acc";
        private const string NOP = "nop";
        private const string JMP = "jmp";

        public override async Task PerformCalculations(string inputFile)
        {
            List<string> listInput = await FileHelper.GetStringListFromFile(inputFile);
            Command[] input = PrepareInput(listInput);

            CalculateAndLogTime(() =>
            {
                int result1 = CmdExecutor.ExecuteToFirstLoop(input);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result1}");
            });

            CalculateAndLogTime(() =>
            {
                int result2 = CmdExecutor.FindCommandToReplace(input);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result2}");
            });

            Console.WriteLine();
        }

        private static Command[] PrepareInput(List<string> listInput)
        {
            List<Command> result = new List<Command>();

            listInput.ForEach(l =>
            {
                result.Add(ReadCommand(l));
            });

            return result.ToArray();
        }

        private static Command ReadCommand(string l)
        {
            string[] parts = l.Split(' ');
            return new Command(parts[0], int.Parse(parts[1]));
        }

        private static struct Command
        {
            public string instruction;
            public int arg;

            public Command(string i, int a)
            {
                instruction = i;
                arg = a;
            }
        }

        private static class CmdExecutor
        {
            public static int FindCommandToReplace(Command[] commands)
            {
                for (int i = 0; i < commands.Length; ++i)
                {
                    Command tmp = commands[i];

                    if (tmp.instruction == ACC)
                        continue;

                    commands[i] = ReplaceCommand(commands[i]);
                    (int acc, bool wasLooped) = ExecuteToFirstLoopOrToEnd(commands);

                    if (!wasLooped)
                    {
                        return acc;
                    }

                    commands[i] = tmp;
                }

                return -1;
            }

            private static Command ReplaceCommand(Command command) =>
                new Command(
                    command.instruction == JMP
                        ? NOP
                        : JMP,
                    command.arg);

            private static (int acc, bool wasLooped) ExecuteToFirstLoopOrToEnd(Command[] commands)
            {
                bool[] wasExecuted = new bool[commands.Length];
                int acc = 0;
                int index = 0;

                do
                {
                    wasExecuted[index] = true;
                    ExecuteCommand(commands[index], ref acc, ref index);

                    if (index >= commands.Length)
                        return (acc, false);

                } while (!wasExecuted[index]);

                return (acc, true);
            }

            public static int ExecuteToFirstLoop(Command[] commands)
            {
                bool[] wasExecuted = new bool[commands.Length];
                int acc = 0;
                int index = 0;

                do
                {
                    wasExecuted[index] = true;
                    ExecuteCommand(commands[index], ref acc, ref index);
                } while (!wasExecuted[index]);

                return acc;
            }

            private static void ExecuteCommand(Command command, ref int acc, ref int index)
            {
                switch (command.instruction)
                {
                    case ACC:
                        ExecuteAcc(command, ref acc, ref index);
                        return;
                    case NOP:
                        ExecuteNop(command, ref acc, ref index);
                        return;
                    case JMP:
                        ExecuteJmp(command, ref acc, ref index);
                        return;
                }
            }

            private static void ExecuteAcc(Command command, ref int acc, ref int index)
            {
                acc += command.arg;
                ++index;
            }

            private static void ExecuteNop(Command command, ref int acc, ref int index)
            {
                ++index;
            }

            private static void ExecuteJmp(Command command, ref int acc, ref int index)
            {
                index += command.arg;
            }
        }
    }
}
