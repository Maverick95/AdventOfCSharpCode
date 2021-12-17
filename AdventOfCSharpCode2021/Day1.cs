using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCSharpCodeHelpers;

namespace AdventOfCSharpCode2021
{
    namespace Day1
    {
        public class Day1_Processor : IDayProcessor
        {
            private Queue<int> _elements = new();
            private int _index;
            private int _increases;

            private void Reset()
            {
                _index = 0;
                _elements.Clear();
                _increases = 0;
            }

            private int CalculateForSlidingWindow(IEnumerable<string> dp, int window)
            {
                Reset();

                foreach (var d in dp)
                {
                    if (int.TryParse(d, out var next))
                    {
                        if (_index >= window && next > _elements.Dequeue())
                        {
                            _increases++;
                        }
                        _elements.Enqueue(next);
                        _index++;
                    }
                    else
                    {
                        throw new ArgumentException("Your input data is rubbish.");
                    }
                }

                return _increases;
            }

            public string Part1(IEnumerable<string> dp)
            {
                return $"Increases detected - {CalculateForSlidingWindow(dp, 1)}.";
            }

            public string Part2(IEnumerable<string> dp)
            {
                return $"Increases detected - {CalculateForSlidingWindow(dp, 3)}.";
            }
        }

        public class Day1
        {
            public static void Main(string[] args)
            {
                var data = new FileDataProcessor(2021, 1);
                var day = new Day1_Processor();

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
