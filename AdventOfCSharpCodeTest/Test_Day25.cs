using AdventOfCSharpCode.Day25;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCSharpCodeTest
{
    namespace Day25
    {
        [TestFixture]
        public class TestBoundaries_Test
        {
            [TestCase(7U, 10_000_000U, ExpectedResult = true)]
            [TestCase(7U, 50_000_000U, ExpectedResult = true)]
            [TestCase(7U, 100_000_000U, ExpectedResult = true)]
            [TestCase(7U, 613_566_757U, ExpectedResult = true)]
            [TestCase(7U, 613_566_758U, ExpectedResult = false)]
            [TestCase(7U, 1_000_000_000U, ExpectedResult = false)]
            [TestCase(7U, 2_000_000_000U, ExpectedResult = false)]
            [TestCase(7U, 4_000_000_000U, ExpectedResult = false)]
            public bool Test_IsValid(uint test_factor, uint test_limit)
            {
                return AdventOfCSharpCode.Day25.Day25.TestBoundaries(test_factor, test_limit);
            }
        }

        [TestFixture(1U, 7U, 20_201_227U)]
        public class SolveKeys_Test
        {
            private Key keys;

            private uint start, factor, limit;

            public SolveKeys_Test(uint s, uint f, uint l)
            {
                start = s; factor = f; limit = l;
            }
 
            [TestCase(5_764_801U, 8U)]
            [TestCase(17_807_724U, 11U)]
            public void SolveKey_Test(uint key, uint loop_result)
            {
                keys = new Key(key);

                Assert.That(AdventOfCSharpCode.Day25.Day25.SolveKeys(new Key[] { keys }, start, factor, limit), Is.EqualTo(1));
                Assert.That(keys.Found, Is.True);
                Assert.That(keys.Loop, Is.EqualTo(loop_result)); 
            }
        }

    }
}
