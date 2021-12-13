using AdventOfCSharpCode2020.Helpers;
using AdventOfCSharpCodeHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCSharpCode2020
{
    namespace Day24
    {
        public class Day24_Processor : IDayProcessor
        {
            private static int[] CreateTilePosition(string input)
            {
                Dictionary<string, int[]> updates = new()
                {
                    { "nw", new[] { -1, 0 } },
                    { "ne", new[] { 0, 1 } },
                    { "sw", new[] { 0, -1 } },
                    { "se", new[] { 1, 0 } },
                    { "w", new[] { -1, -1 } },
                    { "e", new[] { 1, 1 } },
                };

                int[] result = new int[2] { 0, 0 };

                var inputs = input.Replace("w", "w,").Replace("e", "e,").Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var i in inputs)
                {
                    if (!updates.TryGetValue(i, out var update))
                    {
                        throw new ArgumentException("Your input is invalid.");
                    }

                    result[0] += update[0];
                    result[1] += update[1];
                }

                return result;
            }

            private class TileNeighbourCollection : IEnumerable<int[]>
            {
                private readonly TileNeighbourEnumerator _enumerator;
                public TileNeighbourCollection(int[] coord)
                {
                    _enumerator = new(coord);
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

                public IEnumerator<int[]> GetEnumerator()
                {
                    return _enumerator;
                }

                private class TileNeighbourEnumerator : IEnumerator<int[]>
                {
                    private static List<int[]> _updates = new()
                    {
                        new[] { -1, -1 },
                        new[] { -1, 0 },
                        new[] { 0, 1 },
                        new[] { 1, 1 },
                        new[] { 1, 0 },
                        new[] { 0, -1 }
                    };

                    private int[] _coord = null;
                    private int _index = 0;
                    private readonly int[] _coord_original;

                    public TileNeighbourEnumerator(int[] coord)
                    {
                        if (coord.Length is not 2)
                        {
                            throw new ArgumentException("Your input was invalid.");
                        }

                        _coord_original = new int[2];
                        coord.CopyTo(_coord_original, 0);
                    }

                    object IEnumerator.Current
                    {
                        get => Current;
                    }

                    public int[] Current
                    {
                        get
                        {
                            if (_coord is null) { return null; }
                            var result = new int[2];
                            _coord.CopyTo(result, 0);
                            return result;
                        }
                    }

                    public bool MoveNext()
                    {
                        if (_index < _updates.Count)
                        {
                            _coord = new int[2];
                            _coord_original.CopyTo(_coord, 0);
                            _coord[0] += _updates[_index][0];
                            _coord[1] += _updates[_index][1];
                            _index++;
                            return true;
                        }

                        return false;
                    }

                    public void Reset()
                    {
                        _coord = null;
                        _index = 0;
                    }

                    public void Dispose() { }
                }
            }

            private readonly Dictionary<int[], Coordinate> _coords = new(new CoordinateComparer(2, CoordinateComparer.GetHashCodeSum));
            private readonly int _iterations;

            private void Reset()
            {
                _coords.Clear();
            }

            private void Update(int[] input)
            {
                if (_coords.TryGetValue(input, out var coord))
                {
                    coord.active = !coord.active;
                }
                else
                {
                    _coords.Add(input, new()
                    {
                        active = false,
                        neighbours = 0,
                    });
                }
            }

            private void DailyArt()
            {
                // Reset neighbours.

                foreach (var c in _coords.Values)
                {
                    c.neighbours = 0;
                }

                // Calculate neighbours afresh. This includes adding / tracking new elements.

                Dictionary<int[], Coordinate> _coords_new = new(new CoordinateComparer(2, CoordinateComparer.GetHashCodeSum));


                foreach (var c in _coords)
                {
                    // If tile is flipped, need to apply count to neighbours.

                    if (!c.Value.active)
                    {
                        TileNeighbourCollection c_neighbours = new(c.Key);
                        foreach (var n in c_neighbours)
                        {
                            if (_coords.TryGetValue(n, out var n_value))
                            {
                                n_value.neighbours++;
                            }
                            else if (_coords_new.TryGetValue(n, out n_value))
                            {
                                n_value.neighbours++;
                            }
                            else
                            {
                                _coords_new.Add(n, new()
                                {
                                    active = true,
                                    neighbours = 1
                                });
                            }
                        }
                    }
                }

                // Transfer over the new elements.

                foreach (var c in _coords_new)
                {
                    _coords.Add(c.Key, c.Value);
                }

                // Recalculate flipped status.

                foreach (var c in _coords.Values)
                {
                    c.active = Coordinate.IsCoordinateActive_Day24(c);
                }
            }

            public Day24_Processor(int iterations)
            {
                _iterations = iterations;
            }

            public string Part1(IEnumerable<string> dp)
            {
                Reset();

                foreach(var d in dp)
                {
                    var input = CreateTilePosition(d);
                    Update(input);
                }

                var count = _coords.Values.Where(x => !x.active).Count();

                return $"Inactive (flipped) squares - {count}";
            }

            public string Part2(IEnumerable<string> dp)
            {
                Reset();

                foreach(var d in dp)
                {
                    var input = CreateTilePosition(d);
                    Update(input);
                }

                for (var i = 0; i < _iterations; i++)
                {
                    DailyArt();
                }

                var count = _coords.Values.Where(x => !x.active).Count();

                return $"Inactive (flipped) squares - {count}";
            }
        }

        public class Day24
        {
            public static void Main(string[] args)
            {
                var data = new FileDataProcessor(2020, 24);
                var day = new Day24_Processor(100);

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
