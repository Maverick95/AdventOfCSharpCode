using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCSharpCode
{
    namespace Day12
    {
        public enum Direction
        {
            // Lay these out in the appropriate turning order.
            // A right-turn means add one, a left-turn means subtract one (of 90 in both cases).

            SD_EAST = 0,
            SD_SOUTH = 1,
            SD_WEST = 2,
            SD_NORTH = 3
        }

        public enum InstructionType
        {
            I_MOVE_EAST,
            I_MOVE_SOUTH,
            I_MOVE_WEST,
            I_MOVE_NORTH,
            I_TURN_LEFT,
            I_TURN_RIGHT,
            I_MOVE_FORWARD
        }

        public record Instruction
        {
            private static Dictionary<char, InstructionType> _instructions = new()
            {
                ['N'] = InstructionType.I_MOVE_NORTH,
                ['E'] = InstructionType.I_MOVE_EAST,
                ['S'] = InstructionType.I_MOVE_SOUTH,
                ['W'] = InstructionType.I_MOVE_WEST,
                ['L'] = InstructionType.I_TURN_LEFT,
                ['R'] = InstructionType.I_TURN_RIGHT,
                ['F'] = InstructionType.I_MOVE_FORWARD,
            };

            public InstructionType Type { get; init; }
            public int Value { get; init; }

            public static Instruction Translate(string s)
            {
                if (s.Length >= 2 &&
                    _instructions.TryGetValue(s[0], out var _type) &&
                    int.TryParse(s.Substring(1), out var _value))
                {
                    Instruction result = new Instruction
                    {
                        Value = _value,
                        Type = _type,
                    };

                    if (result.Type is not (InstructionType.I_TURN_LEFT or InstructionType.I_TURN_LEFT)
                        || result.Value % 90 == 0)
                    {
                        return result;
                    }
                }
                
                throw new ArgumentException("Instruction - incorrect format");
            }
        }

        public abstract class Ship
        {
            public int East { get; protected set; } = 0;
            public int North { get; protected set; } = 0;
            
            public int Manhattan => Math.Abs(East) + Math.Abs(North);

            public abstract void Update(Instruction i);
        }

        public class Ship_Part1 : Ship
        {
            private Direction Direction = Direction.SD_EAST;

            private void ChangeDirection(int units, bool isLeft)
            {
                for (int i = 0; i < units; i++)
                {
                    if (isLeft)
                    {
                        if (Direction == Direction.SD_EAST)
                        {
                            Direction = Direction.SD_NORTH;
                        }
                        else
                        {
                            Direction -= 1;
                        }
                    }
                    else
                    {
                        if (Direction == Direction.SD_NORTH)
                        {
                            Direction = Direction.SD_EAST;
                        }
                        else
                        {
                            Direction += 1;
                        }
                    }
                }
            }

            public override void Update(Instruction i)
            {
                switch (i.Type)
                {
                    case InstructionType.I_MOVE_EAST:
                        East += i.Value;
                        break;

                    case InstructionType.I_MOVE_SOUTH:
                        North -= i.Value;
                        break;

                    case InstructionType.I_MOVE_WEST:
                        East -= i.Value;
                        break;

                    case InstructionType.I_MOVE_NORTH:
                        North += i.Value;
                        break;

                    case InstructionType.I_TURN_LEFT:
                    case InstructionType.I_TURN_RIGHT:
                        ChangeDirection(i.Value / 90, i.Type == InstructionType.I_TURN_LEFT);
                        break;

                    case InstructionType.I_MOVE_FORWARD:
                        {
                            switch (Direction)
                            {
                                case Direction.SD_EAST:
                                    East += i.Value;
                                    break;
                                case Direction.SD_SOUTH:
                                    North -= i.Value;
                                    break;
                                case Direction.SD_WEST:
                                    East -= i.Value;
                                    break;
                                case Direction.SD_NORTH:
                                    North += i.Value;
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        public class Ship_Part2 : Ship
        {
            private int Waypoint_East = 10;
            private int Waypoint_North = 1;

            private void ChangeDirection(int units, bool isLeft)
            {
                int
                    factor_new_east_old_north = isLeft ? -1 : 1,
                    factor_new_north_old_east = isLeft ? 1 : -1;

                for (int i=0; i < units; i++)
                {
                    int
                        new_wp_east = Waypoint_North * factor_new_east_old_north,
                        new_wp_north = Waypoint_East * factor_new_north_old_east;

                    Waypoint_East = new_wp_east;
                    Waypoint_North = new_wp_north;
                }
            }

            public override void Update(Instruction i)
            {
                switch (i.Type)
                {
                    case InstructionType.I_MOVE_EAST:
                        Waypoint_East += i.Value;
                        break;

                    case InstructionType.I_MOVE_SOUTH:
                        Waypoint_North -= i.Value;
                        break;

                    case InstructionType.I_MOVE_WEST:
                        Waypoint_East -= i.Value;
                        break;

                    case InstructionType.I_MOVE_NORTH:
                        Waypoint_North += i.Value;
                        break;

                    case InstructionType.I_TURN_LEFT:
                    case InstructionType.I_TURN_RIGHT:
                        ChangeDirection(i.Value / 90, i.Type == InstructionType.I_TURN_LEFT);
                        break;

                    case InstructionType.I_MOVE_FORWARD:
                        East += i.Value * Waypoint_East;
                        North += i.Value * Waypoint_North;
                        break;
                }
            }
        }

        public class Day12_Processor: IDayProcessor
        {
            public string Part1(IEnumerable<string> dp)
            {
                var ship_1 = new Ship_Part1();

                foreach(var d in dp)
                {
                    var data = Instruction.Translate(d);
                    ship_1.Update(data);
                }

                return $"Part 1 result = {ship_1.Manhattan}";
            }

            public string Part2(IEnumerable<string> dp)
            {
                var ship_2 = new Ship_Part2();

                foreach(var d in dp)
                {
                    var data = Instruction.Translate(d);
                    ship_2.Update(data);
                }

                return $"Part 2 result = {ship_2.Manhattan}";
            }
        }

        public class Day12
        {
            public static void Main(string[] args)
            {
                var data = new FileDataProcessor(12);
                var day = new Day12_Processor();

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
