using AdventOfCode2020.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day4 : Day
    {
        private const int DESIRED_SUM = 2020;
        private readonly string[] RequiredFields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

        public override async Task PerformCalculations(string inputFile)
        {
            List<string> listInput = await FileHelper.GetStringListFromFile(inputFile);
            Dictionary<string, string>[] input = PrepareInput(listInput);

            Dictionary<string, Func<string, bool>> rules = new Dictionary<string, Func<string, bool>>
            {
                ["byr"] = (s) => ValidateYear(s, 1920, 2002),
                ["iyr"] = (s) => ValidateYear(s, 2010, 2020),
                ["eyr"] = (s) => ValidateYear(s, 2020, 2030),
                ["hgt"] = (s) => ValidateHeight(s),
                ["hcl"] = (s) => ValidateColor(s),
                ["ecl"] = (s) => ValidateEye(s),
                ["pid"] = (s) => ValidatePid(s)
            };

            CalculateAndLogTime(() =>
            {
                long result = CalculateResult1(input);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result}");
            });

            CalculateAndLogTime(() =>
            {
                long result = CalculateResult2(input, rules);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result}");
            });

            Console.WriteLine();
        }

        private static long CalculateResult1(Dictionary<string, string>[] input)
        {
            int result = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                if (IsPassportValid(input[i]))
                {
                    ++result;
                }
            }

            return result;
        }

        private static long CalculateResult2(Dictionary<string, string>[] input, Dictionary<string, Func<string, bool>> rules)
        {
            int result = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                if (ValidateWithRules(input[i], rules))
                {
                    ++result;
                }
            }

            return result;
        }

        private static bool ValidateWithRules(Dictionary<string, string> dictionary, Dictionary<string, Func<string, bool>> rules)
        {
            foreach (var item in rules)
            {
                if (!dictionary.ContainsKey(item.Key) || !item.Value.Invoke(dictionary[item.Key]))
                    return false;
            }

            return true;
        }

        private static bool ValidateYear(string s, int min, int max)
        {
            if (s.Length != 4 || !int.TryParse(s, out int val))
                return false;

            return val >= min && val <= max;
        }

        private static bool ValidateHeight(string s)
        {
            string unit = s.Substring(s.Length - 2);

            if (!int.TryParse(s.Substring(0, s.Length - 2), out int val))
                return false;

            switch (unit)
            {
                case "cm":
                    return val >= 150 && val <= 193;
                case "in":
                    return val >= 59 && val <= 76;
                default:
                    return false;
            }
        }

        private static bool ValidateColor(string s)
        {
            string validChars = "0123456789abcdef";

            if (s[0] != '#' || s.Length != 7)
                return false;

            for(int i = 1; i < s.Length; ++i)
            {
                if (!validChars.Contains(s[i]))
                    return false;
            }

            return true;
        }

        private static bool ValidateEye(string s)
        {
            string[] validColors = new string[]
            {
                "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
            };

            return validColors.Contains(s);
        }

        private static bool ValidatePid(string s)
        {
            string validChars = "0123456789";

            if (s.Length != 9)
                return false;

            for (int i = 1; i < s.Length; ++i)
            {
                if (!validChars.Contains(s[i]))
                    return false;
            }

            return true;
        }

        private static bool IsPassportValid(Dictionary<string, string> dictionary)
        {
            for(int i = 0; i < RequiredFields.Length; ++i)
            {
                if (!dictionary.ContainsKey(RequiredFields[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private static Dictionary<string, string>[] PrepareInput(List<string> listInput)
        {
            List<Dictionary<string, string>> input = new List<Dictionary<string, string>>();
            Dictionary<string, string> dict = new Dictionary<string, string>();

            for (int i = 0; i < listInput.Count; ++i)
            {
                if (string.IsNullOrWhiteSpace(listInput[i]))
                {
                    input.Add(dict);
                    dict = new Dictionary<string, string>();
                    continue;
                }

                string[] keysValues = listInput[i].Split(' ');

                AddKeyValuesToDict(dict, keysValues);
            }

            if(dict.Count > 0)
            {
                input.Add(dict);
            }

            return input.ToArray();
        }

        private static void AddKeyValuesToDict(Dictionary<string, string> dict, string[] keysValues)
        {
            for (int i = 0; i < keysValues.Length; ++i)
            {
                string[] parts = keysValues[i].Split(':');

                dict.Add(parts[0], parts[1]);
            }
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
