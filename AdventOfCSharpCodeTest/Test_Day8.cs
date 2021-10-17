using AdventOfCSharpCode;
using FakeItEasy;
using NUnit.Framework;
using System.Linq;

namespace AdventOfCSharpCodeTest
{
    namespace Day8
    {
        [TestFixture]
        public class Test_Day8
        {
            [TestCase(new string[]
            {
                "nop +0",
                "acc +1",
                "jmp +4",
                "acc +3",
                "jmp -3",
                "acc -99",
                "acc +1",
                "jmp -4",
                "acc +6" ,
            }, 5)]
            public void Part1_Should_Return_CorrectResult_ForInput(string[] data, int expected_result)
            {
                var data_processor = DataProcessor.GenerateFakeDataProcessor(data);
                var day_processor = new AdventOfCSharpCode.Day8.Day8_Processor();
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo($"Part 1 result is {expected_result}"));
            }

            [TestCase(new string[]
            {
                "nop +0",
                "acc +1",
                "jmp +4",
                "acc +3",
                "jmp -3",
                "acc -99",
                "acc +1",
                "jmp -4",
                "acc +6" ,
            }, 8)]
            public void Part2_Should_Return_CorrectResult_ForInput(string[] data, int expected_result)
            {
                var data_processor = DataProcessor.GenerateFakeDataProcessor(data);
                var day_processor = new AdventOfCSharpCode.Day8.Day8_Processor();
                var result = day_processor.Part2(data_processor);

                Assert.That(result, Is.EqualTo($"Part 2 result is {expected_result}"));
            }
        }
    }
}
