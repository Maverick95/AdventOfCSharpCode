using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AdventOfCSharpCode2021.Day2;
using AdventOfCSharpCodeHelpers;

namespace AdventOfCSharpCode2021Test
{
    namespace Day2
    {
        [TestFixture]
        public class Test_Day2
        {
            [TestCase(new string[] {
                "forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2"}, 15, 10, 150)]
            public void Part1_ShouldReturnCorrectResult(string[] data, int distance, int depth, int product)
            {
                StringDataProcessor data_processor = new(data);
                Day2_Processor day_processor = new();
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo($"Result is... Distance: {distance}, Depth: {depth}, Product: {product}"));
            }

            [TestCase(new string[] {
                "forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2"}, 15, 60, 900)]
            public void Part2_ShouldReturnCorrectResult(string[] data, int distance, int depth, int product)
            {
                StringDataProcessor data_processor = new(data);
                Day2_Processor day_processor = new();
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo($"Result is... Distance: {distance}, Depth: {depth}, Product: {product}"));
            }
        }
    }
}
