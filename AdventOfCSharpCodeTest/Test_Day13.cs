﻿using AdventOfCSharpCodeHelpers;
using AdventOfCSharpCode2020.Day13;
using NUnit.Framework;

namespace AdventOfCSharpCodeTest
{
    namespace Day13
    {
        [TestFixture]
        public class Test_Day13
        {
            [TestCase(new string[] { "939", "7,13,x,x,59,x,31,19" }, 59, 5)]
            public void Part1_ShouldReturn_Correct_Result(string[] data, int expected_number, int expected_time)
            {
                StringDataProcessor data_processor = new(data);
                Day13_Processor day_processor = new();
                var result = day_processor.Part1(data_processor);

                var expected = $"Next bus = {expected_number} : Next time = {expected_time} : Multiplied = {expected_number * expected_time}";

                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}
