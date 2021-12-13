using NUnit.Framework;
using AdventOfCSharpCode2021.Day1;
using AdventOfCSharpCodeHelpers;

namespace AdventOfCSharpCode2021Test
{
    namespace Day1
    {
        [TestFixture]
        public class Test_Day1
        {
            [TestCase(new string[] { "199", "200", "208", "210", "200", "207", "240", "269", "260", "263" }, 7)]
            public void Part1_ShouldReturnCorrectResult(string[] data, int expected)
            {
                StringDataProcessor data_processor = new(data);
                Day1_Processor day_processor = new();
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo($"Increases detected - {expected}."));
            }

            [TestCase(new string[] { "199", "200", "208", "210", "200", "207", "240", "269", "260", "263" }, 5)]
            public void Part2_ShouldReturnCorrectResult(string[] data, int expected)
            {
                StringDataProcessor data_processor = new(data);
                Day1_Processor day_processor = new();
                var result = day_processor.Part2(data_processor);

                Assert.That(result, Is.EqualTo($"Increases detected - {expected}."));
            }
        }
    }
}