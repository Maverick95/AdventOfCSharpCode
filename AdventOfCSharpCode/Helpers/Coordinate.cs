using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCSharpCode2020.Helpers
{
    public class Coordinate
    {
        public static bool IsCoordinateActive_Day17(Coordinate coord)
        {
            return (coord.active && (coord.neighbours is 2 or 3)) ||
                    (!coord.active && coord.neighbours is 3);
        }

        public static bool IsCoordinateActive_Day24(Coordinate coord)
        {
            /*
             * 1) Any black tile with zero or more than 2 black tiles immediately adjacent to it is flipped to white.
             * 2) Any white tile with exactly 2 black tiles immediately adjacent to it is flipped to black.
             */

            return (coord.active && coord.neighbours is not 2) ||
                (!coord.active && (coord.neighbours is 0 or > 2));
        }

        public int neighbours { get; set; } = 0;

        // For Day 24, active = true means White, active = false means Black.
        public bool active { get; set; } = true;
    }
}
