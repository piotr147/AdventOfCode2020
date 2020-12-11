using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Utilities
{
    public static class FileHelper
    {
        public async static Task<List<int>> GetIntListFromFile(string fileName)
        {
            var result = new List<int>();

            string[] lines = await File.ReadAllLinesAsync(fileName);

            foreach (string line in lines)
            {
                result.Add(int.Parse(line));
            }

            return result;
        }

        public async static Task<List<string>> GetStringListFromFile(string fileName)
        {
            string[] lines = await File.ReadAllLinesAsync(fileName);

            return lines.ToList();
        }
    }
}