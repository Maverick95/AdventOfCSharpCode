using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCSharpCode
{
    namespace Day24
    {
        public class Coords
        {
            private int x = 0;
            private int y = 0;

            public int[] key
            {
                get
                {
                    return new int[] { x, y };
                }
            }

            public Coords(int i_x, int i_y)
            {
                x = i_x; y = i_x;
            }

            public Coords(string s)
            {
                // e, se, sw, w, nw, and ne

                while (s.Length > 0)
                {
                    switch (s[0])
                    {

                        case 'e':
                        case 'w':
                            {
                                switch (s[0])
                                {
                                    case 'e':
                                        x++; y++; break;

                                    case 'w':
                                        x--; y--; break;
                                }
                            }

                            s = s.Remove(0, 1);
                            break;

                        case 'n':
                        case 's':
                            {
                                switch (s.Substring(0, 2))
                                {
                                    case "nw":
                                        x--; break;

                                    case "ne":
                                        y++; break;

                                    case "sw":
                                        y--; break;

                                    case "se":
                                        x++; break;

                                    default:
                                        throw new Exception();
                                }
                            }

                            s = s.Remove(0, 2);
                            break;

                        default:
                            throw new Exception();

                    }
                }
            }
        }

        public class Tile
        {
            public bool Flipped { get; set; }

            public int Flipped_Neighbours { get; set; }
        }

        public class ArrayComparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y)
            {
                if (x.Length == y.Length)
                {
                    for (int i = 0; i < x.Length; i++)
                    {
                        if (x[i] != y[i])
                        {
                            return false;
                        }
                    }

                    return true;
                }

                return false;
            }

            public int GetHashCode(int[] obj) => Helpers.Hash(obj);
        }

        public class HashTableCoords
        {
            // The bool in the dictionary indicates whether the tile is flipped.

            private Dictionary<int[], Tile> coords { get; } = new Dictionary<int[], Tile>(new ArrayComparer());

            public int Flipped
            {
                get
                {
                    return coords.Where(k => k.Value.Flipped).Count();
                }
            }

            public int Count
            {
                get
                {
                    return coords.Count;
                }
            }

            public void Empty()
            {
                coords.Clear();
            }

            public void Update(Coords c)
            {
                var c_key = c.key;
                Tile c_value = null;

                if (coords.TryGetValue(c_key, out c_value))
                {
                    c_value.Flipped = !c_value.Flipped;

                }
                else
                {
                    coords.Add(c_key, new Tile() { Flipped = true, Flipped_Neighbours = 0 });
                }
            }

            public void DailyArt()
            {
                // Reset neighbours.

                foreach(var c in coords)
                {
                    c.Value.Flipped_Neighbours = 0;
                }

                // Calculate neighbours afresh. This includes adding / tracking new elements.

                Dictionary<int[], Tile> coords_new = new Dictionary<int[], Tile>(new ArrayComparer());

                int[][] coords_change = new int[][]
                {
                    new int[] { -1, -1 },
                    new int[] { -1, 0 },
                    new int[] { 0, 1 },
                    new int[] { 1, 1 },
                    new int[] { 1, 0 },
                    new int[] { 0, -1 }
                };

                foreach (var c in coords)
                {
                    // If tile is flipped, need to apply count to neighbours.

                    if (c.Value.Flipped)
                    {
                        foreach(var d in coords_change)
                        {
                            int[] c_key = new int[] { c.Key[0] + d[0], c.Key[1] + d[1] };

                            Tile c_value;

                            if (coords.TryGetValue(c_key, out c_value))
                            {
                                c_value.Flipped_Neighbours += 1;
                            }
                            else if (coords_new.TryGetValue(c_key, out c_value))
                            {
                                c_value.Flipped_Neighbours += 1;
                            }
                            else
                            {
                                coords_new.Add(c_key, new Tile() { Flipped = false, Flipped_Neighbours = 1 });
                            }
                        }
                    }
                }

                // Transfer over the new elements.

                foreach (var c in coords_new)
                {
                    coords.Add(c.Key, c.Value);
                }

                // Recalculate flipped status.

                foreach (var c in coords)
                {
                    if (c.Value.Flipped && (c.Value.Flipped_Neighbours == 0 || c.Value.Flipped_Neighbours > 2))
                    {
                        c.Value.Flipped = false;
                    }
                    else if (!c.Value.Flipped && c.Value.Flipped_Neighbours == 2)
                    {
                        c.Value.Flipped = true;
                    }
                }
            }

        }
        public class Day24
        {
            public static void Main(string[] args)
            {
                var data = DataProcessing.Import(24);
                var processor = new HashTableCoords();

                foreach(var d in data)
                {
                    processor.Update(new Coords(d));
                }

                Console.WriteLine("Total count - {0}", processor.Count);
                Console.WriteLine("Total flipped - {0}", processor.Flipped);

                for(int i=0; i < 100; i++)
                {
                    processor.DailyArt();
                }

                Console.WriteLine("Total count - {0}", processor.Count);
                Console.WriteLine("Total flipped - {0}", processor.Flipped);
            }
        }
    }
}
