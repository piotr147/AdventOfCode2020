using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode2020.Utilities
{
    public static class FileHelper
    {
        public async static Task<List<int>> GetListFromFile(string fileName)
        {
            var result = new List<int>();

            string[] lines = await File.ReadAllLinesAsync(fileName);

            foreach (string line in lines)
            {
                result.Add(int.Parse(line));
            }

            return result;
        }
    }
}