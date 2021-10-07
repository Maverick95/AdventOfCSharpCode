using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCSharpCode
{
    public interface iDataProcessor
    {
        public string Next { get; }

        public bool isNext { get; }

        public int Index { get; }

        public string[] Data { get; }

        public void Reset();
    }
}
