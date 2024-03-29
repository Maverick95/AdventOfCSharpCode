﻿using AdventOfCSharpCodeHelpers;
using AdventOfCSharpCode2020.Day10;
using NUnit.Framework;

namespace AdventOfCSharpCodeTest
{
    namespace Day10
    {
        [TestFixture]
        public class Test_Day10
        {
            [TestCase(new string[]
            {
                "16",   "10",   "15",   "5",
                "1",    "11",   "7",    "19",
                "6",    "12",   "4",
            }, 7, 5)]
            [TestCase(new string[]
            {
                "28",   "33",   "18",   "42",
                "31",   "14",   "46",   "20",
                "48",   "47",   "24",   "23",
                "49",   "45",   "19",   "38",
                "39",   "11",   "1",    "32",
                "25",   "35",   "8",    "17",
                "7",    "9",    "4",    "2",
                "34",   "10",   "3",
            }, 22, 10)]
            public void Part1_Examples_ReturnsCorrectResult(string[] data, int difference_1, int difference_3)
            {
                StringDataProcessor data_processor = new(data);
                Day10_Processor day_processor = new ();
                var result = day_processor.Part1(data_processor);

                Assert.That(result, Is.EqualTo($"Result! {difference_1} * {difference_3} = {difference_1 * difference_3}"));
            }
        }
    }
}
