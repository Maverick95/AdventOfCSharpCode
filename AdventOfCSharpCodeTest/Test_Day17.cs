using AdventOfCSharpCodeHelpers;
using AdventOfCSharpCode2020.Day17;
using NUnit.Framework;

namespace AdventOfCSharpCodeTest
{
    namespace Day17
    {
        [TestFixture]
        public class Test_Day17
        {
            [TestCase(new string[] { ".#.", "..#", "###" }, 0, 5)]
            [TestCase(new string[] { ".#.", "..#", "###" }, 1, 11)]
            [TestCase(new string[] { ".#.", "..#", "###" }, 2, 21)]
            [TestCase(new string[] { ".#.", "..#", "###" }, 3, 38)]
            [TestCase(new string[] { ".#.", "..#", "###" }, 6, 112)]
            public void Part1_ShouldReturn_Correct_Result_ForIterations(string[] data, int iterations, int expected_active)
            {
                StringDataProcessor data_processor = new(data);
                Day17_Processor day_processor = new(iterations);
                var result = day_processor.Part1(data_processor);

                var expected = $"Conway Cubes remaining - {expected_active}";

                Assert.That(result, Is.EqualTo(expected));
            }

            [TestCase(new string[] { ".#.", "..#", "###" }, 0, 5)]
            [TestCase(new string[] { ".#.", "..#", "###" }, 1, 29)]
            [TestCase(new string[] { ".#.", "..#", "###" }, 2, 60)]
            [TestCase(new string[] { ".#.", "..#", "###" }, 6, 848)]
            public void Part2_ShouldReturn_Correct_Result_ForIterations(string[] data, int iterations, int expected_active)
            {
                StringDataProcessor data_processor = new(data);
                Day17_Processor day_processor = new(iterations);
                var result = day_processor.Part2(data_processor);

                var expected = $"Conway Cubes remaining - {expected_active}";

                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}
