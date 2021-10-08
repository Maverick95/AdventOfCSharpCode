using AdventOfCSharpCode;
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

                var data_processor = DataProcessor.GenerateFakeDataProcessor(data);
                var day_processor = new AdventOfCSharpCode.Day2.Day2_Processor();
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

                var data_processor = DataProcessor.GenerateFakeDataProcessor(data);
                var day_processor = new AdventOfCSharpCode.Day2.Day2_Processor();
                var result = day_processor.Part2(data_processor);

                Assert.That(result, Is.EqualTo("Result! 1"));
            }

        }
    }
}
