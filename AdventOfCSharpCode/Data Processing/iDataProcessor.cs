using System;
using System.IO;
using System.Linq;

namespace AdventOfCSharpCode
{
    public interface iDataProcessor
    {
        public string Next { get; }

        public bool isNext { get; }

        public int Index { get; }

        public void Reset();
    }
}
