using AdventOfCSharpCode.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCSharpCode
{
    namespace Day17
    {
        public class Day17_Processor: IDayProcessor
        {
            private readonly int _iterations;

            public Day17_Processor(int iterations)
            {
                _iterations = iterations;
            }

            private Dictionary<int[], Coordinate> Simulate(IEnumerable<string> dp, int dimensions)
            {
                var comparer = new CoordinateComparer(dimensions, CoordinateComparer.GetHashCodeSum);

                Dictionary<int[], Coordinate>
                    conway = new(comparer),
                    conway_new = new(comparer),
                    conway_remove = new(comparer);

                var y = 0;
                foreach (var d in dp)
                {
                    var x = 0;
                    foreach (var c in d)
                    {
                        if (c is '#')
                        {
                            conway.Add(CreateCoordinate(dimensions, x, y), new());
                        }
                        x++;
                    }

                    y++;
                }

                // Main functionality.

                for (int j = 0; j < _iterations; j++)
                {
                    // Pass through and reset neighbours.

                    foreach (var c in conway.Values)
                    {
                        c.neighbours = 0;
                    }

                    // Pass through and add on neighbours for each - adding in new elements if required.
                    // This time you'll need to check the active property.

                    foreach (var c in conway)
                    {
                        if (c.Value.active)
                        {
                            CoordinateNeighbourCollection c_neighbours = new(c.Key);
                            foreach (var n in c_neighbours)
                            {
                                if (conway.TryGetValue(n, out var n_value))
                                {
                                    n_value.neighbours++;
                                }
                                else if (conway_new.TryGetValue(n, out n_value))
                                {
                                    n_value.neighbours++;
                                }
                                else
                                {
                                    conway_new.Add(n, new()
                                    {
                                        neighbours = 1,
                                        active = false
                                    });
                                }
                            }
                        }
                    }

                    foreach (var c in conway_new)
                    {
                        conway.Add(c.Key, c.Value);
                    }

                    conway_new.Clear();

                    // Pass through and update status, remove.

                    foreach (var c in conway)
                    {
                        c.Value.active = Coordinate.IsCoordinateActive_Day17(c.Value);

                        if (!c.Value.active)
                        {
                            conway_remove.Add(c.Key, c.Value);
                        }
                    }

                    foreach (var c in conway_remove)
                    {
                        conway.Remove(c.Key);
                    }

                    conway_remove.Clear();
                }

                return conway;
            }

            public string Part1(IEnumerable<string> dp)
            {
                var conway = Simulate(dp, 3);
                return $"Conway Cubes remaining - {conway.Count}";
            }

            public string Part2(IEnumerable<string> dp)
            {
                var conway = Simulate(dp, 4);
                return $"Conway Cubes remaining - {conway.Count}";
            }


            private static int[] CreateCoordinate(int length, int x, int y)
            {
                if (length < 1)
                {
                    throw new ArgumentException("Your input is invalid.");
                }

                var result = new int[length];
                result[0] = x;
                if (length > 1)
                {
                    result[1] = y;
                    for (var i = 2; i < length; i++)
                    {
                        result[i] = 0;
                    }
                }

                return result;
            }

            private class CoordinateNeighbourCollection: IEnumerable<int[]>
            {
                private readonly CoordinateNeighbourEnumerator _enumerator;
                public CoordinateNeighbourCollection(int[] coord)
                {
                    _enumerator = new(coord);
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

                public IEnumerator<int[]> GetEnumerator()
                {
                    return _enumerator;
                }

                private class CoordinateNeighbourEnumerator: IEnumerator<int[]>
                {
                    private int[] _coord = null;

                    private readonly int[] _coord_original;
                    private readonly IEqualityComparer<int[]> _comparer;
                    private readonly int _length;

                    public CoordinateNeighbourEnumerator(int[] coord)
                    {
                        if (coord.Length == 0)
                        {
                            throw new ArgumentException("Your input was invalid.");
                        }
                        _length = coord.Length;
                        _coord_original = new int[_length];
                        coord.CopyTo(_coord_original, 0);
                        _comparer = new CoordinateComparer(_length, CoordinateComparer.GetHashCodeSum);
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
                            var result = new int[_length];
                            _coord.CopyTo(result, 0);
                            return result;
                        }
                    }

                    public bool MoveNext()
                    {
                        var next = false;

                        if (_coord is null)
                        {
                            next = true;
                            _coord = new int[_length];
                            _coord_original
                                .Select(x => x - 1)
                                .ToArray()
                                .CopyTo(_coord, 0);
                        }
                        else
                        {
                            for (var i = 1; i <= _length; i++)
                            {
                                if (_coord[_length - i] < _coord_original[_length - i] + 1)
                                {
                                    next = true;
                                    _coord[_length - i]++;
                                    _coord_original
                                        .TakeLast(i - 1)
                                        .Select(x => x - 1)
                                        .ToArray()
                                        .CopyTo(_coord, _length - i + 1);

                                    if (_comparer.Equals(_coord, _coord_original))
                                    {
                                        _coord[_length - 1]++;
                                    }

                                    break;
                                }
                            }
                        }

                        return next;
                    }

                    public void Reset()
                    {
                        _coord = null;
                    }

                    public void Dispose() { }
                }
            }
        }

        public class Day17
        {
            public static void Main(string[] args)
            {
                var data = new FileDataProcessor(17);
                var day = new Day17_Processor(6);

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
