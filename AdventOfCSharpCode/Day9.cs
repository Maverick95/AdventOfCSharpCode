using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCSharpCode
{
    namespace Day9
    {
        public record ContiguousSum
        {
            public int min { get; set; }
            public int max { get; set; }
            public int sum { get; set; }
            public int minPlusMax { get; set; }
        }

        public class Day9_Processor: IDayProcessor
        {
            private int _preamble_length { get; init; }

            private Dictionary<int, int> _additions { get; init; }

            private Queue<int> _preamble { get; init; }

            private List<ContiguousSum> _sum_storage { get; init; }

            private Dictionary<int, int> _sum_lookup { get; init; }

            public Day9_Processor(int preamble_length)
            {
                _preamble_length = preamble_length < 1 ? 1 : preamble_length;
                _additions = new ();
                _preamble = new ();
                _sum_storage = new ();
                _sum_lookup = new ();
            }

            public void Reset()
            {
                _additions.Clear();
                _preamble.Clear();
                _sum_storage.Clear();
                _sum_lookup.Clear();
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

                foreach (var s in _sum_storage)
                {
                    if (added < s.min) { s.min = added; }
                    if (added > s.max) { s.max = added; }
                    s.sum += added;
                    s.minPlusMax = s.min + s.max;
                    _sum_lookup.TryAdd(s.sum, s.minPlusMax);
                }

                _sum_storage.Add(new()
                {
                    min = added,
                    max = added,
                    sum = added,
                    minPlusMax = 2 * added,
                });

                _sum_lookup.TryAdd(added, 2 * added);
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

            public string Part1(IEnumerable<string> dp)
            {
                Reset();
                var index = 0;
                
                foreach(var d in dp)
                {
                    if (int.TryParse(d, out var added))
                    {
                        if (index++ > _preamble_length)
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


            public string Part2(IEnumerable<string> dp)
            {
                Reset();

                var index = 0;
                bool lookup_found = false;
                int lookup = 0;

                foreach(var d in dp)
                {
                    if (int.TryParse(d, out var added))
                    {
                        if (index++ > _preamble_length)
                        {
                            if (!IsAddition(added))
                            {
                                lookup_found = true;
                                lookup = added;
                            }

                            Dequeue();
                        }

                        Enqueue(added);

                        if (lookup_found && _sum_lookup.TryGetValue(lookup, out var result))
                        {
                            return $"Result found! {result}";
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Your input data is rubbish.");
                    }
                }

                return "No result found!";
            }
        }

        public class Day9
        {
            public static void Main(string[] args)
            {
                var data = new FileDataProcessor(9);
                var day = new Day9_Processor(25);

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
