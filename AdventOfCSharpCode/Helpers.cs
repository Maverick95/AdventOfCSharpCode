using System;
using System.Text;

namespace AdventOfCSharpCode
{
    public static class Helpers
    {
        // Constants copied from C++ std::unordered_map<> implementation.

        private const int hash_offset = -2_128_831_035;
        private const int hash_factor = 16_777_619;

        private static int Hash(int x, int offset)
        {
            return (x ^ offset) * hash_factor;
        }

        public static int Hash(int[] x)
        {
            int y = hash_offset;

            foreach(var i in x)
            {
                y = Hash(i, y);
            }

            return y;
        }


    }
}
