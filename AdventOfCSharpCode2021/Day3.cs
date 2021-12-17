using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCSharpCodeHelpers;
using System.Text.RegularExpressions;

namespace AdventOfCSharpCode2021
{
    namespace Day3
    {
        public class Day3_Processor : IDayProcessor
        {
            private List<int> _countsOfBitOne;
            private int _length;

            private void Reset(int bits)
            {
                _countsOfBitOne.Clear();
                for (var i = 0; i < bits; i++)
                {
                    _countsOfBitOne.Add(0);
                }
            }

            public string Part1(IEnumerable<string> dp)
            {
                var size = 0;
                var pattern = new Regex($"^[01]{{{_length}}}$");
                Reset(_length);

                int index;

                foreach (var d in dp)
                {
                    if (pattern.IsMatch(d))
                    {
                        index = 0;
                        foreach (var b in d)
                        {
                            _countsOfBitOne[index++] += (b == '1' ? 1 : 0);
                        }
                    }

                    size++;
                }

                index = _length - 1;
                var gamma = 0; var epsilon = 0;

                foreach (var b in _countsOfBitOne)
                {
                    if (b >= size / 2)
                    {
                        gamma += (1 << index);
                    }
                    else
                    {
                        epsilon += (1 << index);
                    }

                    index--;
                }

                return $"Gamma: {gamma}, Epsilon: {epsilon}, Product: {gamma * epsilon}";
            }

            public string Part2(IEnumerable<string> dp)
            {
                return "Not implemented!";
            }

            public Day3_Processor(int length)
            {
                _length = length;
                _countsOfBitOne = new();
            }

        }

        public class Day3
        {
            public static void Main(string[] args)
            {
                var data = new FileDataProcessor(2021, 3);
                var day = new Day3_Processor(12);

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
