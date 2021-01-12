using AdventOfCSharpCode.Day24;
using System;
using System.Text;
using NUnit.Framework;

namespace AdventOfCSharpCodeTest
{
    namespace Day24
    {
        /*
         * sesenwnenenewse
         * neeenesenwnwwsw
         * seswnesw
         * nwnwneseeswswnene
         * swweswneswnenw
         */

        /*
         * se   se  nw  ne  ne  ne  w   se              x = 1, y = 2
         * ne   e   e   ne  se  nw  nw  w   sw          x = 0, y = 2
         * se   sw  ne  sw                              x = 1, y = -1
         * nw   nw  ne  se  e   sw  sw  ne  ne          x = 0, y = 2
         * sw   w   e   sw  ne  sw  ne  nw              x = -1, y = -1
         */

        [TestFixture("sesenwnenenewse", 1, 2)]
        [TestFixture("neeenesenwnwwsw", 0, 2)]
        [TestFixture("seswnesw", 1, -1)]
        [TestFixture("nwnwneseeswswnene", 0, 2)]
        [TestFixture("swweswneswnenw", -1, -1)]
        public class Coords_Test
        {
            private Coords coords;
            private int x, y;

            public Coords_Test(string s, int i_x, int i_y)
            {
                coords = new Coords(s);
                x = i_x; y = i_y;
            }

            [Test]
            public void Test_Length()
            {
                Assert.That(coords.key, Has.Exactly(2).Items);
            }

            [Test]
            public void Test_X()
            {
                Assert.That(coords.key[0], Is.EqualTo(x));
            }

            [Test]
            public void Test_Y()
            {
                Assert.That(coords.key[1], Is.EqualTo(y));
            }
        }

        public class HashTableCoords_Tests
        {
            // Given one major test example here.

            private static string[] test_input = new string[]
            {
                "sesenwnenenewseeswwswswwnenewsewsw",
                "neeenesenwnwwswnenewnwwsewnenwseswesw",
                "seswneswswsenwwnwse",
                "nwnwneseeswswnenewneswwnewseswneseene",
                "swweswneswnenwsewnwneneseenw",
                "eesenwseswswnenwswnwnwsewwnwsene",
                "sewnenenenesenwsewnenwwwse",
                "wenwwweseeeweswwwnwwe",
                "wsweesenenewnwwnwsenewsenwwsesesenwne",
                "neeswseenwwswnwswswnw",
                "nenwswwsewswnenenewsenwsenwnesesenew",
                "enewnwewneswsewnwswenweswnenwsenwsw",
                "sweneswneswneneenwnewenewwneswswnese",
                "swwesenesewenwneswnwwneseswwne",
                "enesenwswwswneneswsenwnewswseenwsese",
                "wnwnesenesenenwwnenwsewesewsesesew",
                "nenewswnwewswnenesenwnesewesw",
                "eneswnwswnwsenenwnwnwwseeswneewsenese",
                "neswnwewnwnwseenwseesewsenwsweewe",
                "wseweeenwnesenwwwswnew"
            };

            [TestFixture]
            public class HashTableCoords_Test_Part1
            {
                private HashTableCoords coords = new HashTableCoords();

                [OneTimeSetUp]
                public void SetUp()
                {
                    foreach (var s in test_input)
                    {
                        coords.Update(new Coords(s));
                    }
                }

                [Test]
                public void Test_Count()
                {
                    Assert.That(coords.Count, Is.EqualTo(15));
                }

                [Test]
                public void Test_Flipped()
                {
                    Assert.That(coords.Flipped, Is.EqualTo(10));
                }
            }

            [TestFixture]
            public class HashTableCoords_Test_Part2
            {
                private HashTableCoords coords = new HashTableCoords();

                [SetUp]
                public void SetUp()
                {
                    foreach (var s in test_input)
                    {
                        coords.Update(new Coords(s));
                    }
                }

                [TestCase(0, ExpectedResult = 10)]
                [TestCase(1, ExpectedResult = 15)]
                [TestCase(2, ExpectedResult = 12)]
                [TestCase(3, ExpectedResult = 25)]
                [TestCase(4, ExpectedResult = 14)]
                [TestCase(5, ExpectedResult = 23)]
                [TestCase(6, ExpectedResult = 28)]
                [TestCase(7, ExpectedResult = 41)]
                [TestCase(8, ExpectedResult = 37)]
                [TestCase(9, ExpectedResult = 49)]
                [TestCase(10, ExpectedResult = 37)]
                [TestCase(20, ExpectedResult = 132)]
                [TestCase(30, ExpectedResult = 259)]
                [TestCase(40, ExpectedResult = 406)]
                [TestCase(50, ExpectedResult = 566)]
                [TestCase(60, ExpectedResult = 788)]
                [TestCase(70, ExpectedResult = 1106)]
                [TestCase(80, ExpectedResult = 1373)]
                [TestCase(90, ExpectedResult = 1844)]
                [TestCase(100, ExpectedResult = 2208)]
                public int Test_DailyArt(int n)
                {
                    for (int i=0; i < n; i++)
                    {
                        coords.DailyArt();
                    }

                    return coords.Flipped;
                }

                [TearDown]
                public void TearDown()
                {
                    coords.Empty();
                }

            }
        }
    }
}
