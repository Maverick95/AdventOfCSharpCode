using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCSharpCode
{
    namespace Day13
    {
        public class BusTimetableStorage
        {
            public int EarliestTimestamp { get; private set; } = 0;

            public int[] Buses { get; private set; }

            public int[] NextBus
            {
                get
                {
                    bool first = true;
                    int next_time = 0; int next_bus = 0;

                    foreach (var b in Buses)
                    {
                        int b_remainder = b - (EarliestTimestamp % b);

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
                    
                    return new int[] { next_bus, next_time, next_bus * next_time };
                }
            }

            public BusTimetableStorage(string et, string b)
            {
                int et_return;

                if (!int.TryParse(et, out et_return))
                {
                    throw new Exception("Invalid arguments");
                }

                EarliestTimestamp = et_return;

                Buses = b.Split(',')
                    .Select(x => x.Trim())
                    .Select(x =>
                    {
                        int x_return;
                        if (int.TryParse(x, out x_return))
                        {
                            return x_return;
                        }
                        return 0;
                    })
                    .Where(x => x > 0)
                    .ToArray();
            }
        }

        public class Day13
        {
            public static void Main(string[] args)
            {
                var data = DataProcessing.Import(13);
                if (data.Length == 2)
                {
                    var data_results = new BusTimetableStorage(data[0], data[1]).NextBus;
                    Console.WriteLine(string.Format("Part 1 result - Bus {0} in {1} minutes, {0} x {1} = {2}",
                        data_results[0],
                        data_results[1],
                        data_results[2]));
                }
            }
        }
    }
}
