﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCSharpCode
{
    namespace Day2
    {
        public class Password
        {
            public char CheckChar { get; init; }

            public int CheckIntegerInput1 { get; init; }

            public int CheckIntegerInput2 { get; init; }

            public string CheckPassword { get; init; }

            public bool Check_Part1
            {
                get
                {
                    int check_char_count = CheckPassword.ToCharArray().Where((c) => c == CheckChar).Count();
                    return
                        check_char_count >= CheckIntegerInput1 &&
                        check_char_count <= CheckIntegerInput2;
                }
            }

            public bool Check_Part2
            {
                get
                {
                    return
                    (new int[] { CheckIntegerInput1, CheckIntegerInput2 })
                    .Select((x) => CheckPassword.ToCharArray()[x - 1])
                    .Where((x) => x == CheckChar)
                    .Count() == 1;
                }
            }
        }
        public class Day2_Processor : iDayProcessor
        {
            private static Regex rgx_password = new Regex("^[0-9]+-[0-9]+ [a-z]: [a-z]+$");

            private static Password GetPassword(string s)
            {
                if (rgx_password.IsMatch(s))
                {
                    var s_data = s.Split(new char[] { '-', ' ', ':' });
                    int
                        s_ll = int.Parse(s_data[0]),
                        s_lu = int.Parse(s_data[1]);

                    char
                        s_cc = s_data[2][0];

                    if (s_lu >= s_ll)
                    {
                        return
                            new Password
                            {
                                CheckChar = s_cc,
                                CheckIntegerInput1 = s_ll,
                                CheckIntegerInput2 = s_lu,
                                CheckPassword = s_data[4]
                            };
                    }

                }

                return null;

            }

            public string Part1(iDataProcessor dp)
            {
                var result = dp.Data
                    .Select(d => GetPassword(d))
                    .Where(p => p is not null)
                    .Where(p => p.Check_Part1)
                    .Count();

                return $"Result! {result}";
            }

            public string Part2(iDataProcessor dp)
            {
                var result = dp.Data
                    .Select(d => GetPassword(d))
                    .Where(p => p is not null)
                    .Where(p => p.Check_Part2)
                    .Count();

                return $"Result! {result}";
            }

        }

        public class Day2
        {
            public static void Main(string[] args)
            {
                var data = new DataProcessor(2);
                var day = new Day2_Processor();

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
