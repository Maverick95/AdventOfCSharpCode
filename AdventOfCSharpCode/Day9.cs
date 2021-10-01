using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCSharpCode
{
    namespace Day9
    {
        public class Day9_Processor: iDayProcessor
        {
            private int _preamble_length { get; init; }

            public Day9_Processor(int preamble_length)
            {
                _preamble_length = preamble_length < 1 ? 1 : preamble_length;
            }

            public string Part1(iDataProcessor dp)
            {
                dp.Reset();
                Dictionary<int, int> store_additions = new ();
                Queue<int> store_preamble = new ();
                int line_index = 0;

                while (dp.isNext)
                {
                    if (int.TryParse(dp.Next, out var added))
                    {
                        if (line_index >= _preamble_length)
                        {
                            if (!store_additions.ContainsKey(added))
                            {
                                // This is the exit point.
                                return added.ToString();
                            }

                            // Now you're removing a value from the preamble.

                            var removed = store_preamble.Dequeue();

                            foreach (var pa in store_preamble)
                            {
                                var removed_additions = pa + removed;
                                if (store_additions.Remove(removed_additions, out var removed_volume))
                                {
                                    if (--removed_volume > 0)
                                    {
                                        store_additions.Add(removed_additions, removed_volume);
                                    }
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                        }

                        // Now you're adding a value into the preamble.

                        foreach(var pa in store_preamble)
                        {
                            // Add new element into store_additions.
                            var new_addition = pa + added;
                            var new_volume = 0;
                            store_additions.Remove(new_addition, out new_volume);
                            store_additions.Add(new_addition, ++new_volume);
                        }

                        store_preamble.Enqueue(added);

                        line_index++;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                return "Part 1!";
            }


            public string Part2(iDataProcessor dp)
            {
                dp.Reset();
                return "Part 2!";
            }
        }

        public class Day9
        {
            public static void Main(string[] args)
            {
                var data = new DataProcessor(9);
                var day = new Day9_Processor(25);

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
