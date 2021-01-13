using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCSharpCode
{
    namespace Day8
    {
        public enum InstructionType
        {
            Accumulate,
            Jump,
            NoOperation
        }
        public class Instruction
        {
            public InstructionType type { get; set; }

            public int value { get; set; }
        }

        public class Day8
        {

            public static InstructionType GetInstructionType(string input)
            {
                var return_value = InstructionType.NoOperation;

                switch (input)
                {
                    case "acc":
                        return_value = InstructionType.Accumulate;
                        break;
                    case "jmp":
                        return_value = InstructionType.Jump;
                        break;
                }

                return return_value;
            }

            public static int GetValue(string input)
            {
                int return_value = 0;

                try
                {
                    int value = int.Parse(input.Substring(1));
                    int multiplier = 0;

                    switch (input[0])
                    {
                        case '+':
                            multiplier = 1;
                            break;
                        case '-':
                            multiplier = -1;
                            break;
                    }

                    return_value = value * multiplier;
                }
                catch
                {

                }

                return return_value;
            }

            public static Instruction GetInstruction(string input)
            {
                var input_data = input.Split(' ');

                if (input_data.Length == 2)
                {
                    return new Instruction { type = GetInstructionType(input_data[0]), value = GetValue(input_data[1]) };
                }

                return null;
            }

            public static void Main_Part2_Recurse(ref List<int>[] connections, ref bool[] end, int index)
            {
                end[index] = true;

                if (connections[index] != null)
                {
                    foreach (var i in connections[index])
                    {
                        Main_Part2_Recurse(ref connections, ref end, i);
                    }
                }
            }

            public static void Main(string[] args)
            {
                try
                {
                    var data = DataProcessing.Import(8).Select((x) => GetInstruction(x)).ToArray();

                    var data_executed = data.Select((x) => false).ToArray();

                    // Part 1.

                    int global_accumulator = 0, global_index = 0;

                    while (!data_executed[global_index])
                    {
                        data_executed[global_index] = true;

                        switch (data[global_index].type)
                        {
                            case InstructionType.Accumulate:
                                global_accumulator += data[global_index++].value;
                                break;
                            case InstructionType.Jump:
                                global_index += data[global_index].value;
                                break;
                            case InstructionType.NoOperation:
                                global_index++;
                                break;
                        }
                    }

                    Console.WriteLine("Part 1 result is {0}", global_accumulator);

                    // Part 2.

                    // Identify start / end points for instructions, create map to work backwards from the end.

                    var data_connections = new List<int>[data.Length];

                    for (int i = 0; i < data.Length; i++)
                    {
                        int j = i;

                        switch (data[i].type)
                        {
                            case InstructionType.Accumulate:
                            case InstructionType.NoOperation:
                                j++;
                                break;
                            case InstructionType.Jump:
                                j += data[i].value;
                                break;
                        }

                        if (j >= 0 && j < data.Length)
                        {
                            if (data_connections[j] == null)
                            {
                                data_connections[j] = new List<int>();
                            }

                            data_connections[j].Add(i);
                        }
                    }

                    // Identify end region of graph (any point where from you can reach the end).

                    var data_end = data.Select((x) => false).ToArray();

                    Main_Part2_Recurse(ref data_connections, ref data_end, data.Length - 1);

                    // Now step through the function call again from start to finish, identify a valid change.

                    var data_executed_2 = data.Select((x) => false).ToArray();

                    global_accumulator = 0;
                    global_index = 0;

                    var check_change = true;

                    while (!data_executed_2[global_index])
                    {
                        data_executed_2[global_index] = true;

                        switch (data[global_index].type)
                        {
                            case InstructionType.Accumulate:
                                global_accumulator += data[global_index++].value;
                                break;
                            case InstructionType.Jump:
                                {
                                    if (check_change && data_end[global_index + 1])
                                    {
                                        check_change = false;
                                        global_index++;
                                    }
                                    else
                                    {
                                        global_index += data[global_index].value;
                                    }
                                }
                                break;
                            case InstructionType.NoOperation:
                                {
                                    if (check_change &&
                                        global_index + data[global_index].value >= 0 &&
                                        global_index + data[global_index].value < data.Length &&
                                        data_end[global_index + data[global_index].value])
                                    {
                                        check_change = false;
                                        global_index += data[global_index].value;
                                    }
                                    else
                                    {
                                        global_index++;
                                    }
                                }
                                break;
                        }

                        if (!check_change && global_index == data.Length)
                        {
                            break;
                        }
                    }

                    Console.WriteLine("Part 2 result is {0}", global_accumulator);
                }
                catch
                {

                }
            }
        }
    }
}
