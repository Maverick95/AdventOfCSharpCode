using System.IO;
using System.Linq;

namespace AdventOfCSharpCode
{
    public static class DataProcessing
    {
        private static string DataPath { get; } =
            "C:\\Users\\Nick\\Documents\\Programming\\Advent of Code\\AdventOfCSharpCode\\AdventOfCSharpCode\\Data";

        public static string[] Import(int dayNumber)
        {
            string path = string.Format("{0}\\Data_Day{1}.txt", DataPath, dayNumber.ToString());

            if (File.Exists(path))
            {
                return File.ReadAllLines(path).Select(x => x.Trim()).Where(x => x.Length > 0).ToArray();
            }

            return null;
        }
    }
}
