using AdventOfCSharpCode.Day12;
using NUnit.Framework;
using System.Linq;

namespace AdventOfCSharpCodeTest
{
    namespace Day12
    {
        // All test cases are taken from Advent of Code, Day 12, part 1
        // https://adventofcode.com/2020/day/12
        
        [TestFixture("F10", InstructionType.I_MOVE_FORWARD, 10)]
        [TestFixture("N3", InstructionType.I_MOVE_NORTH, 3)]
        [TestFixture("F7", InstructionType.I_MOVE_FORWARD, 7)]
        [TestFixture("R90", InstructionType.I_TURN_RIGHT, 90)]
        [TestFixture("F11", InstructionType.I_MOVE_FORWARD, 11)]

        public class Test_Instruction
        {
            private Instruction proc_instr { get; set; }
            private InstructionType exp_type { get; set; }
            private int exp_value { get; set; }

            public Test_Instruction(string input, InstructionType t, int v)
            {
                proc_instr = Instruction.Translate(input);
                exp_type = t;
                exp_value = v;
            }

            [Test(Description="Types match")]
            public void Test_Type()
            {
                Assert.That(proc_instr.Type, Is.EqualTo(exp_type));
            }

            [Test(Description ="Values match")]
            public void Test_Value()
            {
                Assert.That(proc_instr.Value, Is.EqualTo(exp_value));
            }

        }

        [TestFixture(typeof(Ship_Part1), new string[] { }, 0, 0, 0)]
        [TestFixture(typeof(Ship_Part1), new string[] { "F10", "N3", "F7", "R90", "F11" }, 17, -8, 25)]
        [TestFixture(typeof(Ship_Part2), new string[] { }, 0, 0, 0)]
        [TestFixture(typeof(Ship_Part2), new string[] { "F10", "N3", "F7", "R90", "F11" }, 214, -72, 286)]
        public class Test_Ship<T>
            where T : Ship, new()
        {
            private Instruction[] instructions { get; }

            private int exp_east { get; }
            private int exp_north { get; }
            private int exp_manhattan { get; }

            private T ship { get; } = new T();

            public Test_Ship(string[] instr, int end_east, int end_north, int end_manhattan)
            {
                instructions = instr.Select(x => Instruction.Translate(x)).ToArray();
                exp_east = end_east;
                exp_north = end_north;
                exp_manhattan = end_manhattan;
            }

            [OneTimeSetUp]
            public void SetUp()
            {
                foreach(var i in instructions)
                {
                    ship.Update(i);
                }
            }

            [Test]
            public void Test_East()
            {
                Assert.That(ship.East, Is.EqualTo(exp_east));
            }

            [Test]
            public void Test_North()
            {
                Assert.That(ship.North, Is.EqualTo(exp_north));
            }
            
            [Test]
            public void Test_Manhattan()
            {
                Assert.That(ship.Manhattan, Is.EqualTo(exp_manhattan));
            }
        }
    }
}