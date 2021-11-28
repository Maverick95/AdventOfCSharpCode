using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCSharpCode.Helpers
{
    public class CoordinateComparer : IEqualityComparer<int[]>
    {
        public static int GetHashCodeSum(int[] c)
        {
            var result = 0;
            foreach (var i in c)
            {
                result += i;
            }

            return result;
        }

        private readonly int _size;
        private Func<int[], int> _getHashCode;

        public CoordinateComparer(int size, Func<int[], int> getHashCode)
        {
            _size = size;
            _getHashCode = getHashCode;
        }

        public bool Equals(int[] c1, int[] c2)
        {
            if (c1.Length != _size || c1.Length != c2.Length)
            {
                throw new ArgumentException("Your input is invalid.");
            }

            for (var i = 0; i < _size; i++)
            {
                if (c1[i] != c2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(int[] c)
        {
            if (c.Length != _size)
            {
                throw new ArgumentException("Your input is invalid.");
            }

            return _getHashCode(c);
        }
    }
}
