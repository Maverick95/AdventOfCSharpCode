using System;
using System.IO;
using System.Linq;

namespace AdventOfCSharpCode
{
    public class DataProcessor : iDataProcessor
    {
        private static string DataPath { get; } =
            "C:\\Users\\nick.emmerson\\Work Unit Only\\VS Projects\\Advent of Code\\Data";

        private string[] _data { get; init; }
        private int _index;

        public DataProcessor(int dayNumber)
        {
            string path = string.Format("{0}\\Data_Day{1}.txt", DataPath, dayNumber.ToString());

            if (File.Exists(path))
            {
                _data = File.ReadAllLines(path).Select(x => x.Trim()).Where(x => x.Length > 0).ToArray();
                _index = 0;
            }
            else
            {
                throw new ArgumentException("Your input data is rubbish.");
            }
        }

        public string Next
        {
            get
            {
                if (_data == null || _index >= _data.Length)
                {
                    return null;
                }

                return _data[_index++];
            }
        }

        public bool isNext
        {
            get
            {
                return (_data != null && _index < _data.Length);
            }
        }

        public int Index
        {
            get
            {
                return _index;
            }
        }

        public void Reset()
        {
            if (_data != null)
            {
                _index = 0;
            }
        }
    }
}
