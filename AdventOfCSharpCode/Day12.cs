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

        public class Instruction
        {
            public InstructionType Type { get; private set; }
            public int Value { get; private set; }

            public static Instruction Translate(string s)
            {
                if (s == null || s.Length < 2)
                {
                    throw new Exception("Instruction - incorrect format");
                }

                Instruction i_return = new Instruction();

                int i_value;

                if (!int.TryParse(s.Substring(1), out i_value))
                {
                    throw new Exception("Instruction - incorrect format");
                }

                i_return.Value = i_value;

                switch (s[0])
                {
                    case 'N':
                        i_return.Type = InstructionType.I_MOVE_NORTH;
                        break;
                    case 'E':
                        i_return.Type = InstructionType.I_MOVE_EAST;
                        break;
                    case 'S':
                        i_return.Type = InstructionType.I_MOVE_SOUTH;
                        break;
                    case 'W':
                        i_return.Type = InstructionType.I_MOVE_WEST;
                        break;
                    case 'L':
                        i_return.Type = InstructionType.I_TURN_LEFT;
                        if (i_return.Value % 90 > 0)
                        {
                            throw new Exception("Instruction - incorrect format");
                        }
                        break;
                    case 'R':
                        i_return.Type = InstructionType.I_TURN_RIGHT;
                        if (i_return.Value % 90 > 0)
                        {
                            throw new Exception("Instruction - incorrect format");
                        }
                        break;
                    case 'F':
                        i_return.Type = InstructionType.I_MOVE_FORWARD;
                        break;
                    default:
                        throw new Exception("Instruction - incorrect format");
                }

                return i_return;
            }
        }

        public abstract class Ship
        {
            public int East { get; protected set; } = 0;
            public int North { get; protected set; } = 0;
            
            public int Manhattan
            {
                get
                {
                    return Math.Abs(East) + Math.Abs(North);
                }
            }

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

        public class Day12_Processor: iDayProcessor
        {
            public string Part1(iDataProcessor dp)
            {
                dp.Reset();
                var ship_1 = new Ship_Part1();

                while (dp.isNext)
                {
                    var data = Instruction.Translate(dp.Next);
                    ship_1.Update(data);

                }

                return string.Format("Part 1 result = {0}", ship_1.Manhattan.ToString());
            }

            public string Part2(iDataProcessor dp)
            {
                dp.Reset();
                var ship_2 = new Ship_Part2();

                while (dp.isNext)
                {
                    var data = Instruction.Translate(dp.Next);
                    ship_2.Update(data);
                }

                return string.Format("Part 2 result = {0}", ship_2.Manhattan.ToString());

            }
        }

        public class Day12
        {
            public static void Main(string[] args)
            {
                var data = new DataProcessor(12);
                var day = new Day12_Processor();

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
