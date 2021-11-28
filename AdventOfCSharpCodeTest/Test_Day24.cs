using AdventOfCSharpCode.Day24;
using System;
using System.Text;
using NUnit.Framework;
using AdventOfCSharpCode;

namespace AdventOfCSharpCodeTest
{
    namespace Day24
    {
        [TestFixture]
        public class Test_Day24
        {
            private static string[] _config = new[]
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

            [TestCase(10, 10)]
            public void Part1_ShouldReturn_Correct_Result(int iterations, int expected_active)
            {
                StringDataProcessor data_processor = new(_config);
                Day24_Processor day_processor = new(iterations);
                var result = day_processor.Part1(data_processor);

                var expected = $"Inactive (flipped) squares - {expected_active}";

                Assert.That(result, Is.EqualTo(expected));
            }

            [TestCase(1, 15)]
            [TestCase(2, 12)]
            [TestCase(3, 25)]
            [TestCase(4, 14)]
            [TestCase(5, 23)]
            [TestCase(6, 28)]
            [TestCase(7, 41)]
            [TestCase(8, 37)]
            [TestCase(9, 49)]
            [TestCase(10, 37)]
            [TestCase(20, 132)]
            [TestCase(30, 259)]
            [TestCase(40, 406)]
            [TestCase(50, 566)]
            [TestCase(60, 788)]
            [TestCase(70, 1106)]
            [TestCase(80, 1373)]
            [TestCase(90, 1844)]
            [TestCase(100, 2208)]
            public void Part2_ShouldReturn_Correct_Result_ForIterations(int iterations, int expected_active)
            {
                StringDataProcessor data_processor = new(_config);
                Day24_Processor day_processor = new(iterations);
                var result = day_processor.Part2(data_processor);

                var expected = $"Inactive (flipped) squares - {expected_active}";

                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}
