using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCSharpCode
{
    namespace Day25
    {
        public class Key
        {
            public uint Target { get; private set; }

            public bool Found = false;
            public uint Loop = 0U;

            public Key(uint t)
            {
                Target = t;
            }
        }

        public class Day25
        {
            private static readonly uint
                start = 1U,
                factor = 7U,
                limit = 20_201_227U;

            public static bool TestBoundaries(uint f, uint l)
            {
                uint limit_boundary = 0U;

                for (uint i = 0U; i < f; i++)
                {
                    if (limit_boundary + l - 1U < limit_boundary)
                    {
                        return false;
                    }

                    limit_boundary += l - 1U;
                }

                return true;
            }

            // Returns number of keys solved.
            public static int SolveKeys(Key[] keys, uint s, uint f, uint l)
            {
                if (!TestBoundaries(f, l))
                {
                    return 0;
                }

                foreach(var k in keys)
                {
                    k.Found = false;
                    k.Loop = 0U;
                }

                uint target_lp = s, loop_lp = 0U;
                int keys_found = 0;
                
                while (target_lp != 0U && keys_found != keys.Length)
                {
                    foreach (var k in keys.Where(k => !(k.Found)))
                    {
                        if (target_lp == k.Target)
                        {
                            k.Found = true;
                            k.Loop = loop_lp;
                            keys_found++;
                        }
                    }

                    target_lp *= f; target_lp %= l; loop_lp++;
                }

                return keys_found;
            }

            public static void Main(string[] args)
            {
                Key[] data = new Key[] { new Key(8_335_663U), new Key(8_614_349U) };

                int solved = SolveKeys(data, start, factor, limit);

                if (solved == data.Length)
                {
                    uint result = 1U;
                    foreach(var d in data)
                    {
                        for (uint l = 0U; l < d.Loop; l++)
                        {
                            result *= factor;
                            result %= limit;
                        }
                    }

                    Console.WriteLine(result);

                    uint key_1 = data[0].Target, loop_1 = data[0].Loop;
                    uint key_2 = data[1].Target, loop_2 = data[1].Loop;

                    for (uint i = 0; i < loop_1; i++)
                    {
                        key_2 *= factor;
                        key_2 %= limit;
                    }

                    for (uint i = 0; i < loop_2; i++)
                    {
                        key_1 *= factor;
                        key_1 %= limit;
                    }

                }
            }
        }
    }
}
