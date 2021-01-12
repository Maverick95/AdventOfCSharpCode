using AdventOfCSharpCode.Day13;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCSharpCodeTest
{
    namespace Day13
    {
        [TestFixture]
        public static class Test_Day13
        {
            private static string test_timestamp = "939";
            private static string test_buses = "7,13,x,x,59,x,31,19";

            [Test]
            public static void TestCase()
            {
                var test_data = new BusTimetableStorage(test_timestamp, test_buses);

                Assert.That(test_data.EarliestTimestamp, Is.EqualTo(939));

                Assert.That(test_data.Buses, Has.Length.EqualTo(5));
                Assert.That(test_data.Buses, Has.All.GreaterThanOrEqualTo(7));
                Assert.That(test_data.Buses, Has.All.LessThanOrEqualTo(59));
            }
        }
    }
}
