using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCSharpCode2021.Day3;
using NUnit.Framework;
using AdventOfCSharpCodeHelpers;

namespace AdventOfCSharpCode2021Test
{
    namespace Day3
    {
        [TestFixture]
        public class Test_Day3
        {
            [TestCase(new string[]
                {
                "00100","11110","10110",
                "10111","10101","01111",
                "00111","11100","10000",
                "11001","00010","01010"
                }, 22, 9, 198)]
            public void Part1_ShouldReturnCorrectResult(string[] data, int gamma, int epsilon, int product)
            {
                StringDataProcessor data_processor = new(data);
                Day3_Processor day_processor = new(5);
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo($"Gamma: {gamma}, Epsilon: {epsilon}, Product: {product}"));
            }
        }
    }
}
