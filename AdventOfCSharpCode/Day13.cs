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

            }
        }
    }
}
