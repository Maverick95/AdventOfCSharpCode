using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCSharpCode
{
    class Day2
    {
        private class Password
        {
            public char CheckChar { get; set; }

            public int CheckIntegerInput1 { get; set; }

            public int CheckIntegerInput2 { get; set; }

            public string CheckPassword { get; set; }

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

        public static void Main(string[] args)
        {
            try
            {
                var passwords = File.ReadAllLines(
                    string.Format("{0}\\{1}.txt",
                    Info.DataPath,
                    "Data_Day2"))
                    .Select(s => GetPassword(s))
                    .Where(s => s != null);

                Console.WriteLine("Part 1 - {0}",
                    passwords.Where(s => s.Check_Part1)
                    .Count());

                Console.WriteLine("Part 2 - {0}",
                    passwords.Where(s => s.Check_Part2)
                    .Count());
            }
            catch
            {

            }
        }
    }
}
