using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCSharpCodeHelpers;

namespace AdventOfCSharpCode2021
{
    namespace Day2
    {
        public class Day2_Processor: IDayProcessor
        {
            private static Dictionary<string, Action<Coordinate, int>> Actions_Part1 = new ()
            {
                { "forward", (coord, input) => { coord.Distance += input; } },
                { "up", (coord, input) => { coord.Depth -= input; } },
                { "down", (coord, input) => { coord.Depth += input; } },
            };

            private static Dictionary<string, Action<Coordinate, int>> Actions_Part2 = new()
            {
                { "forward", (coord, input) =>
                {
                    coord.Distance += input;
                    coord.Depth += input * coord.Aim;
                } },
                { "up", (coord, input) => { coord.Aim -= input; } },
                { "down", (coord, input) => { coord.Aim += input; } },
            };

            private static void UpdateCoordinate(
                Coordinate coord,
                string instruction,
                Dictionary<string, Action<Coordinate, int>> actions)
            {
                var data = instruction.Split(' ');
                if (data.Length == 2 &&
                    actions.TryGetValue(data[0], out var action) &&
                    int.TryParse(data[1], out var input))
                {
                    action(coord, input);
                }
                else
                {
                    throw new ArgumentException("Your input data is rubbish.");
                }
            }

            private class Coordinate
            {
                public int Distance { get; set; } = 0;
                public int Depth { get; set; } = 0;
                public int Aim { get; set; } = 0;
            }

            public string Part1(IEnumerable<string> dp)
            {
                Coordinate coord = new();

                foreach(var d in dp)
                {
                    UpdateCoordinate(coord, d, Actions_Part1);
                }

                return $"Result is... Distance: {coord.Distance}, Depth: {coord.Depth}, Product: {coord.Distance * coord.Depth}";
            }


            public string Part2(IEnumerable<string> dp)
            {
                Coordinate coord = new();

                foreach (var d in dp)
                {
                    UpdateCoordinate(coord, d, Actions_Part2);
                }

                return $"Result is... Distance: {coord.Distance}, Depth: {coord.Depth}, Product: {coord.Distance * coord.Depth}";
            }
        }

        public class Day2
        {
            public static void Main(string[] args)
            {
                var data = new FileDataProcessor(2021, 2);
                var day = new Day2_Processor();

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }

    }
}
