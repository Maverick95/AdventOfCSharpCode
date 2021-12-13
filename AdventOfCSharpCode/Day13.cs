using AdventOfCSharpCodeHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCSharpCode2020
{
    namespace Day13
    {
        public class Day13_Processor : IDayProcessor
        {
            public string Part1(IEnumerable<string> dp)
            {
                var dp_list = dp.ToList();
                string d1 = dp_list[0], d2 = dp_list[1];

                if (int.TryParse(d1, out var _earliest))
                {
                    var _buses = d2.Split(',')
                        .Select(x =>
                        {
                            if (int.TryParse(x, out var x_return))
                            {
                                return x_return;
                            }
                            else if (x == "x")
                            {
                                return 0;
                            }
                            else
                            {
                                throw new ArgumentException("Your input data is rubbish.");
                            }
                        })
                        .Where(x => x > 0)
                        .ToArray();

                    var first = true;
                    int next_time = 0, next_bus = 0;

                    foreach (var b in _buses)
                    {
                        int b_remainder = b - (_earliest % b);

                        if (first)
                        {
                            next_time = b_remainder; next_bus = b;
                            first = false;
                        }
                        else if (b_remainder < next_time)
                        {
                            next_time = b_remainder; next_bus = b;
                        }
                    }

                    return $"Next bus = {next_bus} : Next time = {next_time} : Multiplied = {next_bus * next_time}";
                }

                throw new ArgumentException("Your input data is rubbish.");
            }

            public string Part2(IEnumerable<string> dp)
            {
                return "Something";
            }
        }

        public class Day13
        {
            public static void Main(string[] args)
            {
                var data = new FileDataProcessor(2020, 13);
                var day = new Day13_Processor();

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
