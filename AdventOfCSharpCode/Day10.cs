using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCSharpCode
{
    namespace Day10
    {
        public static class Day10_Functions
        {
            private static void AddOrIncrease(Dictionary<int, int> d, int v)
            {
                if (d.ContainsKey(v))
                {
                    d[v]++;
                }
                else
                {
                    d.Add(v, 1);
                }
            }

            public static Dictionary<int, int> AggregateVoltage(int[] data)
            {
                var data_sort = data.OrderBy(x => x).ToArray();

                var data_diff = new Dictionary<int, int>();

                int ds_prev = 0;

                foreach(var ds in data_sort)
                {
                    AddOrIncrease(data_diff, ds - ds_prev);
                    ds_prev = ds;
                }

                AddOrIncrease(data_diff, 3);

                return data_diff;
            }
        }

        public class Day10
        {
            public static Dictionary<int, int> Day10_Part1()
            {
                var data = DataProcessing.Import(10);
                var data_integers = data.Select(x => int.Parse(x)).ToArray();
                return Day10_Functions.AggregateVoltage(data_integers);

            }

            public static void Main(string[] args)
            {
                var data_dict = Day10_Part1();

                foreach(var d in data_dict)
                {
                    Console.WriteLine("Dictionary element - {0} - {1}", d.Key, d.Value);
                }

            }
        }
    }
}
