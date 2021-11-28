using System.Collections.Generic;

namespace AdventOfCSharpCode
{
    public interface IDayProcessor
    {
        public string Part1(IEnumerable<string> dp);

        public string Part2(IEnumerable<string> dp);
    }
}
