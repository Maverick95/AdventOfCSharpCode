using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCSharpCode
{
    class Day17
    {
        private class CoordComparer: IEqualityComparer<int[]>
        {
            public bool Equals(int[] d1, int[] d2) => d1[0] == d2[0] && d1[1] == d2[1] && d1[2] == d2[2];
            public int GetHashCode(int[] d)
            {
                // Oh god it's awful.

                var e =
                    (200 * 200 * (d[0] + 100)) +
                    (200 * (d[1] + 100)) +
                    (d[2] + 100);

                return e;
            }
        }

        private class CoordComparer_Part2: IEqualityComparer<int[]>
        {
            public bool Equals(int[] d1, int[] d2) => d1[0] == d2[0] && d1[1] == d2[1] && d1[2] == d2[2] && d1[3] == d2[3];
            public int GetHashCode(int[] d)
            {
                // Oh god it's awful.

                var e =
                    (200 * 200 * 200 * (d[0] + 100)) +
                    (200 * 200 * (d[1] + 100)) +
                    (200 * (d[2] + 100)) +
                    (d[3] + 100);

                return e;
            }
        }

        private class CoordData
        {
            public int neighbours { get; set; } = 0;

            public bool active { get; set; } = true;

            public bool RemainActive
            {
                get
                {
                    return (active && (neighbours == 2 || neighbours == 3)) ||
                        (!active && neighbours == 3);
                }
            }

        }

        public static void Main(string[] args)
        {
            Main_Part1(args);
            Main_Part2(args);
        }

        private static void Main_Part1(string[] args)
        {
            // Part 1.

            var conway = new Dictionary<int[], CoordData>(new CoordComparer());

            var conway_data = DataProcessing.Import(17);

            for (int y=0; y < conway_data.Length; y++)
            {
                var conway_y = conway_data[y];
                for (int x=0; x < conway_y.Length; x++)
                {
                    if (conway_y[x] == '#')
                    {
                        conway.Add(new int[3] { x, y, 0 }, new CoordData());
                    }
                }
            }

            // Main functionality.

            for (int j = 1; j <= 6; j++)
            {

                // First pass - all elements currently active.

                // Pass through and reset neighbours.

                foreach (var c in conway)
                {
                    c.Value.neighbours = 0;
                }

                // Pass through and add on neighbours for each - adding in new elements if required.
                // This time you'll need to check the active property.

                var conway_new = new Dictionary<int[], CoordData>(new CoordComparer());

                foreach (var c in conway)
                {
                    if (c.Value.active)
                    {
                        for (int x = c.Key[0] - 1; x <= c.Key[0] + 1; x++)
                        {
                            for (int y = c.Key[1] - 1; y <= c.Key[1] + 1; y++)
                            {
                                for (int z = c.Key[2] - 1; z <= c.Key[2] + 1; z++)
                                {
                                    if (x != c.Key[0] || y != c.Key[1] || z != c.Key[2])
                                    {
                                        // Look for element, insert as inactive if not there.
                                        if (conway.ContainsKey(new int[] { x, y, z }))
                                        {
                                            conway[new int[] { x, y, z }].neighbours++;
                                        }
                                        else if (conway_new.ContainsKey(new int[] { x, y, z }))
                                        {
                                            conway_new[new int[] { x, y, z }].neighbours++;
                                        }
                                        else
                                        {
                                            conway_new.Add(new int[] { x, y, z }, new CoordData()
                                            {
                                                neighbours = 1,
                                                active = false
                                            });
                                        }
                                    }
                                }
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
                    c.Value.active = c.Value.RemainActive;

                    if (!c.Value.active)
                    {
                        conway_new.Add(c.Key, c.Value);
                    }
                }

                foreach (var c in conway_new)
                {
                    conway.Remove(c.Key);
                }
            }

            Console.WriteLine(string.Format("{0}", conway.Count));
        }

        private static void Main_Part2(string[] args)
        {
            // Part 2.

            var conway = new Dictionary<int[], CoordData>(new CoordComparer_Part2());

            var conway_data = DataProcessing.Import(17);

            for (int y = 0; y < conway_data.Length; y++)
            {
                var conway_y = conway_data[y];
                for (int x = 0; x < conway_y.Length; x++)
                {
                    if (conway_y[x] == '#')
                    {
                        conway.Add(new int[4] { x, y, 0, 0 }, new CoordData());
                    }
                }
            }

            // Main functionality.

            for (int j = 1; j <= 6; j++)
            {

                // First pass - all elements currently active.

                // Pass through and reset neighbours.

                foreach (var c in conway)
                {
                    c.Value.neighbours = 0;
                }

                // Pass through and add on neighbours for each - adding in new elements if required.
                // This time you'll need to check the active property.

                var conway_new = new Dictionary<int[], CoordData>(new CoordComparer_Part2());

                foreach (var c in conway)
                {
                    if (c.Value.active)
                    {
                        for (int x = c.Key[0] - 1; x <= c.Key[0] + 1; x++)
                        {
                            for (int y = c.Key[1] - 1; y <= c.Key[1] + 1; y++)
                            {
                                for (int z = c.Key[2] - 1; z <= c.Key[2] + 1; z++)
                                {
                                    for (int w = c.Key[3] - 1; w <= c.Key[3] + 1; w++)
                                    {
                                        if (x != c.Key[0] || y != c.Key[1] || z != c.Key[2] || w != c.Key[3])
                                        {
                                            // Look for element, insert as inactive if not there.
                                            if (conway.ContainsKey(new int[] { x, y, z, w }))
                                            {
                                                conway[new int[] { x, y, z, w }].neighbours++;
                                            }
                                            else if (conway_new.ContainsKey(new int[] { x, y, z, w }))
                                            {
                                                conway_new[new int[] { x, y, z, w }].neighbours++;
                                            }
                                            else
                                            {
                                                conway_new.Add(new int[] { x, y, z, w }, new CoordData()
                                                {
                                                    neighbours = 1,
                                                    active = false
                                                });
                                            }
                                        }
                                    }
                                }
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
                    c.Value.active = c.Value.RemainActive;

                    if (!c.Value.active)
                    {
                        conway_new.Add(c.Key, c.Value);
                    }
                }

                foreach (var c in conway_new)
                {
                    conway.Remove(c.Key);
                }
            }

            Console.WriteLine(string.Format("{0}", conway.Count));
        }
    }
}
