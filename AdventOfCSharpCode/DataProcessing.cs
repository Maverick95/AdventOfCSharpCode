using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCSharpCode
{
    public interface iDataProcessor
    {
        public string Next { get; }

        public bool isNext { get; }

        public void Reset();
    }

    public class DataProcessor : iDataProcessor
    {
        private static string DataPath { get; } =
            "C:\\Users\\nick.emmerson\\Work Unit Only\\VS Projects\\Advent of Code\\Data";

        private string[] data = null;
        private int index = -1;

        public DataProcessor(int dayNumber)
        {
            string path = string.Format("{0}\\Data_Day{1}.txt", DataPath, dayNumber.ToString());

            if (File.Exists(path))
            {
                data = File.ReadAllLines(path).Select(x => x.Trim()).Where(x => x.Length > 0).ToArray();
                index = 0;
            }
        }

        public string Next
        {
            get
            {
                if (data == null || index >= data.Length)
                {
                    return null;
                }

                return data[index++];
            }
        }

        public bool isNext
        {
            get
            {
                return (data != null && index < data.Length);
            }
        }

        public void Reset()
        {
            if (data != null)
            {
                index = 0;
            }
        }
    }

    public static class DataProcessing
    {
        private static string DataPath { get; } =
            "C:\\Users\\nick.emmerson\\Work Unit Only\\VS Projects\\Advent of Code\\Data";

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
