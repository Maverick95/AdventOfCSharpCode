using AdventOfCSharpCode;
using AdventOfCSharpCode.Day12;
using NUnit.Framework;
using System.Linq;
using FakeItEasy;

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

        [TestFixture(new string[] { }, 0, 0)]
        [TestFixture(new string[] { "F10", "N3", "F7", "R90", "F11" }, 25, 286)]
        public class Test_Ship
        {
            private iDataProcessor processor;

            private string[] instructions;

            private int results_part1;
            private int results_part2;

            public Test_Ship(string[] instr, int rp1, int rp2)
            {
                processor = A.Fake<iDataProcessor>();

                A.CallTo(() => processor.isNext).Returns(false);
                A.CallTo(() => processor.Next).Returns(null);

                instructions = instr;
                results_part1 = rp1;
                results_part2 = rp2;
            }

            [SetUp]
            public void Setup()
            {
                // Need to mock backwards.

                for (var i = 1; i <= instructions.Length; i++)
                {
                    A.CallTo(() => processor.isNext).Returns(true).Once();
                    A.CallTo(() => processor.Next).Returns(instructions[instructions.Length - i]).Once();
                }
            }

            [Test]
            public void Test_Part1()
            {
                var d12 = new Day12_Processor();
                Assert.That(d12.Part1(processor), Is.EqualTo(string.Format("Part 1 result = {0}", results_part1)));
            }

            [Test]
            public void Test_Part2()
            {
                var d12 = new Day12_Processor();
                Assert.That(d12.Part2(processor), Is.EqualTo(string.Format("Part 2 result = {0}", results_part2)));
            }
        }
    }
}