using AdventOfCSharpCode;
using AdventOfCSharpCode.Day12;
using NUnit.Framework;
using System.Linq;
using FakeItEasy;

namespace AdventOfCSharpCodeTest
{
    namespace Day12
    {
        [TestFixture]
        public class Test_Day12
        {
            [TestCase("F10", InstructionType.I_MOVE_FORWARD, 10)]
            [TestCase("N3", InstructionType.I_MOVE_NORTH, 3)]
            [TestCase("F7", InstructionType.I_MOVE_FORWARD, 7)]
            [TestCase("R90", InstructionType.I_TURN_RIGHT, 90)]
            [TestCase("F11", InstructionType.I_MOVE_FORWARD, 11)]
            public void Instruction_ShouldTranslateInstructions(string data, InstructionType expected_type, int expected_value)
            {
                var expected = new Instruction { Type = expected_type, Value = expected_value };
                var actual = Instruction.Translate(data);
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase(new string[] { "F10", "N3", "F7", "R90", "F11" }, 25)]
            public void Part1_ShouldReturn_25_ForInputProvided(string[] data, int expected)
            {
                var data_processor = DataProcessor.GenerateFakeDataProcessor(data);
                var day_processor = new Day12_Processor();
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo($"Part 1 result = {expected}"));
            }

            [TestCase(new string[] { "F10", "N3", "F7", "R90", "F11" }, 286)]
            public void Part2_ShouldReturn_286_ForInputProvided(string[] data, int expected)
            {
                var data_processor = DataProcessor.GenerateFakeDataProcessor(data);
                var day_processor = new Day12_Processor();
                var result = day_processor.Part2(data_processor);

                Assert.That(result, Is.EqualTo($"Part 2 result = {expected}"));
            }
        }
    }
}