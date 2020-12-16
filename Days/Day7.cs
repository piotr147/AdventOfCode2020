using AdventOfCode2020.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day7 : Day
    {
        private const string BAG = "bag";
        private const string BAGS_CONTAIN = "bags contain";
        private const string EMPTY_BAG = "no other bags";


        public override async Task PerformCalculations(string inputFile)
        {
            List<string> listInput = await FileHelper.GetStringListFromFile(inputFile);
            List<Bag> input = PrepareInput(listInput);

            CalculateAndLogTime(() =>
            {
                int result1 = CalculateResult(input, "shiny gold");
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result1}");
            });

            CalculateAndLogTime(() =>
            {
                int result2 = CalculateResult2(input, "shiny gold");
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result2}");
            });

            Console.WriteLine();
        }

        private int CalculateResult2(List<Bag> input, string col)
        {
            Bag bag = input.Find(b => b.Color == col);
            
            return BagsCount(bag, input) - 1;
        }

        private int CalculateResult(List<Bag> input, string color)
        {
            int bagsContainingColor = 0;

            foreach (var bag in input)
            {
                if (BagContains(bag, color, input))
                    ++bagsContainingColor;
            }

            return bagsContainingColor;
        }

        private int BagsCount(Bag bag, List<Bag> input)
        {
            int bagsInside = 1;

            foreach (var childCol in bag.Children)
            {
                Bag child = input.Find(b => b.Color == childCol.Key);
                bagsInside += childCol.Value * BagsCount(child, input);
            }

            return bagsInside;
        }

        private bool BagContains(Bag bag, string color, List<Bag> input)
        {
            if (bag.ContainsColor(color))
                return true;

            if (bag.Children.Count == 0)
                return false;

            bool contains = false;

            foreach (var childCol in bag.Children)
            {
                Bag child = input.Find(b => b.Color == childCol.Key);

                if (BagContains(child, color, input))
                    return true;
            }

            return false;
        }

        private List<Bag> PrepareInput(List<string> listInput)
        {
            List<Bag> result = new List<Bag>();

            listInput.ForEach(i =>
            {
                Bag bag = ReadBag(i);
                result.Add(bag);
            });

            return result;
        }

        private Bag ReadBag(string line)
        {
            int bagsWordIndex = line.IndexOf(BAGS_CONTAIN);
            string color = line.Substring(0, bagsWordIndex - 1);
            string childrenText = line.Substring(bagsWordIndex + BAGS_CONTAIN.Length + 1);
            Bag bag = new Bag(color);

            AddChildren(bag, childrenText);

            return bag;
        }

        private void AddChildren(Bag bag, string childrenText)
        {
            if (childrenText.Contains(EMPTY_BAG))
                return;

            string[] childTexts = childrenText.Replace(".", "").Split(',');
            
            foreach(var child in childTexts)
            {
                (string color, int number) = ReadChild(child);
                bag.Children.Add(color, number);
            }
        }

        private (string color, int number) ReadChild(string child)
        {
            child = child.Trim();
            int firstSpaceIndex = child.IndexOf(' ');

            int number = int.Parse(child.Substring(0, firstSpaceIndex));
            int bagIndex = child.IndexOf(BAG);
            string color = child.Substring(firstSpaceIndex + 1, bagIndex - firstSpaceIndex - 2);

            return (color, number);
        }

        private class Bag
        {
            public string Color { get; }

            public Dictionary<string, int> Children { get; }

            public Bag(string col)
            {
                Color = col;
                Children = new Dictionary<string, int>();
            }

            public bool ContainsColor(string color) =>
                Children.ContainsKey(color);
        }
    }
}
