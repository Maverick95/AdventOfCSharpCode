using AdventOfCSharpCodeHelpers;
using AdventOfCSharpCode2020.Day2;
using FakeItEasy;
using NUnit.Framework;

namespace AdventOfCSharpCodeTest
{
    namespace Day2
    {
        [TestFixture]
        public class Test_Day2
        {
            [Test]
            public void Part1_ShouldReturnCorrectResult()
            {
                var data = new string[]
                {
                    "1-3 a: abcde",
                    "1-3 b: cdefg",
                    "2-9 c: ccccccccc",
                };

                StringDataProcessor data_processor = new(data);
                Day2_Processor day_processor = new();
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo("Result! 2"));
            }

            [Test]
            public void Part2_ShouldReturnCorrectResult()
            {
                var data = new string[]
                {
                    "1-3 a: abcde",
                    "1-3 b: cdefg",
                    "2-9 c: ccccccccc",
                };

                StringDataProcessor data_processor = new(data);
                Day2_Processor day_processor = new();
                var result = day_processor.Part2(data_processor);

                Assert.That(result, Is.EqualTo("Result! 1"));
            }

        }
    }
}
