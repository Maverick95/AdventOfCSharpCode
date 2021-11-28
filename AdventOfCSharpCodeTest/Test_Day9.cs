using AdventOfCSharpCode;
using AdventOfCSharpCode.Day9;
using FakeItEasy;
using NUnit.Framework;
using System.Linq;

namespace AdventOfCSharpCodeTest
{
    namespace Day9
    {
        [TestFixture]

        public class Test_Day9
        {
            [TestCase(26)]
            [TestCase(49)]
            public void Part1_NumbersWithSuccess_AfterPreamble_1_to_25(int next)
            {
                var data = new string[26];
                for (var i = 0; i < 25; i++) { data[i] = (i + 1).ToString(); }
                data[25] = next.ToString();

                StringDataProcessor data_processor = new(data);
                Day9_Processor day_processor = new(25);
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo("No result found!"));
            }

            [TestCase(100)]
            [TestCase(50)]
            public void Part1_NumbersWithFailure_AfterPreamble_1_to_25(int next)
            {
                var data = new string[26];
                for (var i = 0; i < 25; i++) { data[i] = (i + 1).ToString(); }
                data[25] = next.ToString();

                StringDataProcessor data_processor = new(data);
                Day9_Processor day_processor = new(25);
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo($"Result found! {next}"));
            }

            [TestCase(26)]
            [TestCase(64)]
            [TestCase(66)]
            public void Part1_NumbersWithSuccess_AfterPreamble_20_Then_2_to_25_Then_45(int next)
            {
                var data = new string[27];
                data[0] = (20).ToString();
                for (var i = 1; i <= 19; i++) { data[i] = i.ToString(); }
                for (var i = 20; i < 25; i++) { data[i] = (i + 1).ToString(); }
                data[25] = (45).ToString();
                data[26] = next.ToString();

                StringDataProcessor data_processor = new(data);
                Day9_Processor day_processor = new(25);
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo("No result found!"));
            }

            [TestCase(65)]
            public void Part1_NumbersWithFailure_AfterPreamble_20_Then_2_to_25_Then_45(int next)
            {
                var data = new string[27];
                data[0] = (20).ToString();
                for (var i = 1; i <= 19; i++) { data[i] = i.ToString(); }
                for (var i = 20; i < 25; i++) { data[i] = (i + 1).ToString(); }
                data[25] = (45).ToString();
                data[26] = next.ToString();

                StringDataProcessor data_processor = new(data);
                Day9_Processor day_processor = new(25);
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo($"Result found! {next}"));
            }

            [Test]
            public void Part1_LargerExample_Preamble5_ShouldReturn127()
            {
                var data = new int[]
                {
                    35,     20,     15,     25,     47,
                    40,     62,     55,     65,     95,
                    102,    117,    150,    182,    127,    // 127 is the error number
                    219,    299,    277,    309,    576
                };

                StringDataProcessor data_processor = new(data.Select(d => d.ToString()).ToArray());
                Day9_Processor day_processor = new(5);
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo($"Result found! 127"));
            }

            [Test]
            public void Part2_LargerExample_Preamble5_ShouldMatch127_WithResult_62()
            {
                var data = new int[]
                {
                    35,     20,     15,     25,     47,
                    40,     62,     55,     65,     95,
                    102,    117,    150,    182,    127,    // 127 is the error number
                    219,    299,    277,    309,    576
                };

                StringDataProcessor data_processor = new(data.Select(d => d.ToString()).ToArray());
                Day9_Processor day_processor = new(5);
                var result = day_processor.Part2(data_processor);

                Assert.That(result, Is.EqualTo($"Result found! 62"));
            }
        }
    }
}
