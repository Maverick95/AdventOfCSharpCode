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

            private Dictionary<int, int> _additions { get; init; }

            private Queue<int> _preamble { get; init; }

            public Day9_Processor(int preamble_length)
            {
                _preamble_length = preamble_length < 1 ? 1 : preamble_length;
                _additions = new ();
                _preamble = new ();
            }

            public void Reset()
            {
                _additions.Clear();
                _preamble.Clear();
            }

            public bool IsAddition(int added)
            {
                return _additions.ContainsKey(added);
            }

            public void Enqueue(int added)
            {
                foreach (var pa in _preamble)
                {
                    var new_addition = pa + added;
                    _additions.Remove(new_addition, out var new_volume);
                    _additions.Add(new_addition, ++new_volume);
                }

                _preamble.Enqueue(added);
            }

            public void Dequeue()
            {
                var removed = _preamble.Dequeue();

                foreach (var pa in _preamble)
                {
                    var removed_additions = pa + removed;
                    if (_additions.Remove(removed_additions, out var removed_volume))
                    {
                        if (--removed_volume > 0)
                        {
                            _additions.Add(removed_additions, removed_volume);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Something has gone horribly wrong with your implementation.");
                    }
                }
            }

            public string Part1(iDataProcessor dp)
            {
                dp.Reset();
                Reset();

                while (dp.isNext)
                {
                    if (int.TryParse(dp.Next, out var added))
                    {
                        if (dp.Index > _preamble_length)
                        {
                            if (!IsAddition(added))
                            {
                                return $"Result found! {added}";
                            }

                            Dequeue();
                        }

                        Enqueue(added);
                    }
                    else
                    {
                        throw new ArgumentException("Your input data is rubbish.");
                    }
                }

                return "No result found!";
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
