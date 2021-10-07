using FakeItEasy;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCSharpCode
{
    public class DataProcessor : iDataProcessor
    {
        public static iDataProcessor GenerateFakeDataProcessor(string[] data)
        {
            /* Order of arguments called in the program loop is assumed to be -
             *
             * isNext
             * Next
             * index
             * 
             * In other words the program has the rough structure -
             * 
             * Reset()
             * while (isNext)
             * {
             *      get Next
             *      get Index
             *      ...
             * }
             * 
             * Probably should formalise this sometime to use as helpful library.
             * 
             */
            
            if (data.Length == 0)
            {
                throw new ArgumentException("Need to supply some data.");
            }

            var processor = A.Fake<iDataProcessor>();

            // isNext
            A.CallTo(() => processor.isNext).Returns(false);
            A.CallTo(() => processor.isNext).Returns(true).NumberOfTimes(data.Length);

            // Next
            A.CallTo(() => processor.Next).Returns(null);
            A.CallTo(() => processor.Next).ReturnsNextFromSequence(data);

            // Index
            var data_index = new int[data.Length];
            for (var i=0; i < data.Length; i++) { data_index[i] = i + 1; }
            A.CallTo(() => processor.Index).Returns(data.Length);
            A.CallTo(() => processor.Index).ReturnsNextFromSequence(data_index);

            return processor;
        }

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
                if (_index >= _data.Length)
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
                return (_index < _data.Length);
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
            _index = 0;
        }
    }
}
