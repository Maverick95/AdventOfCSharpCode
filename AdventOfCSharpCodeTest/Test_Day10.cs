using AdventOfCSharpCode;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCSharpCodeTest
{
    namespace Day10
    {
        [TestFixture(new int[] { 16,10,15,5,1,11,7,19,6,12,4 }, 7, 5)]
        [TestFixture(new int[] { 28,33,18,42,31,14,46,20,48,47,24,23,49,45,19,38,39,11,1,32,25,35,8,17,7,9,4,2,34,10,3 }, 22, 10)]
        public class Day10_Part1_Test
        {
            private Dictionary<int, int> lookups;
            private int r1, r3;

            public Day10_Part1_Test(int[] data, int result_1, int result_3)
            {
                lookups = AdventOfCSharpCode.Day10.Day10_Functions.AggregateVoltage(data);
                r1 = result_1;
                r3 = result_3;
            }

            [Test]
            public void Test1()
            {
                Assert.That(lookups, Has.Count.EqualTo(2));
                Assert.That(lookups, Has.Member(new KeyValuePair<int, int>(1, r1)));
                Assert.That(lookups, Has.Member(new KeyValuePair<int, int>(3, r3)));
            }

        }
    }
}
