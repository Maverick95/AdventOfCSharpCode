using AdventOfCSharpCodeHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCSharpCode2020
{
    namespace Day10
    {
        public class Day10_Processor: IDayProcessor
        {
            private Dictionary<int, int> _difference { get; init; }
            private int _voltage;

            public Day10_Processor()
            {
                _difference = new ();
                _voltage = 0;
            }

            private void Reset()
            {
                _difference.Clear();
                _voltage = 0;
            }

            private void AddOrIncrease(int v)
            {
                var _new_difference = v - _voltage;

                if (_new_difference is >= 1 and <= 3)
                {
                    if (_difference.ContainsKey(_new_difference))
                    {
                        _difference[_new_difference]++;
                    }
                    else
                    {
                        _difference.Add(_new_difference, 1);
                    }

                    _voltage = v;
                }
                else
                {
                    throw new ArgumentException("Your input data is rubbish.");
                }
            }

            public string Part1(IEnumerable<string> dp)
            {
                _difference.Clear();
                
                var data = dp.Select(d => int.Parse(d))
                    .OrderBy(d => d);

                foreach (var d in data)
                {
                    AddOrIncrease(d);
                }

                AddOrIncrease(_voltage + 3);

                int _difference_1 = 0, _difference_3 = 0;
                _difference.TryGetValue(1, out _difference_1);
                _difference.TryGetValue(3, out _difference_3);

                return $"Result! {_difference_1} * {_difference_3} = {_difference_1 * _difference_3}";
            }

            public string Part2(IEnumerable<string> dp)
            {
                return "Part 2!";
            }
        }

        public class Day10
        {
            public static void Main(string[] args)
            {
                var data = new FileDataProcessor(2020, 10);
                var day = new Day10_Processor();

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
