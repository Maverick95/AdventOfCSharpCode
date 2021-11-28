using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCSharpCode
{
    namespace Day8
    {
        public class Day8_Processor: IDayProcessor
        {
            // Set of instructions.
            private List<Instruction> _instructions { get; init; }

            // _sources maps destination indices to all the source indices that send them there
            // with a single instruction.
            Dictionary<int, List<int>> _sources { get; init; }

            public Day8_Processor()
            {
                _instructions = new ();
                _sources = new ();
            }

            private void Reset()
            {
                _instructions.Clear();
                _sources.Clear();
            }

            private void Process(IDataProcessor dp)
            {
                dp.Reset();
                Reset();

                var _index_source = 0;

                // Extract all required info from dp.

                while (dp.isNext)
                {
                    var _instruction = new Instruction(dp.Next);

                    // Add instruction.
                    _instructions.Add(_instruction);

                    // Add source.
                    var _index_destination = _index_source +
                        (_instruction.Type is InstructionType.Jump ? _instruction.Value : 1);

                    if (_sources.ContainsKey(_index_destination))
                    {
                        _sources[_index_destination].Add(_index_source);
                    }
                    else
                    {
                        _sources.Add(_index_destination, new() { _index_source });
                    }

                    _index_source++;
                }

                // Append all items you can reach from the beginning element.
                // From the beginning, you shouldn't fall outside of the bounds of the instructions.

                _index_source = 0;

                while (_instructions[_index_source].State is InstructionState.NoExecution)
                {
                    _instructions[_index_source].State = InstructionState.ExecutedFromStart;
                    _index_source += (_instructions[_index_source].Type is InstructionType.Jump ? _instructions[_index_source].Value : 1);

                    if (_index_source < 0 || _index_source >= _instructions.Count)
                    {
                        throw new ArgumentException("Your input data is rubbish.");
                    }
                }

                // Append all items you can reach from the end element.
                // From the end, you shouldn't access an element that can be accessed from the beginning.

                Queue<int> _destinations = new ();

                if (_sources.ContainsKey(_instructions.Count))
                {
                    foreach (var i in _sources[_instructions.Count])
                    {
                        _destinations.Enqueue(i);
                    }
                }

                while (_destinations.Any())
                {
                    var _dest = _destinations.Dequeue();

                    if (_instructions[_dest].State is InstructionState.NoExecution)
                    {
                        _instructions[_dest].State = InstructionState.ExecutedFromEnd;
                    }
                    else
                    {
                        throw new ArgumentException("Your input data is rubbish.");
                    }

                    if (_sources.ContainsKey(_dest))
                    {
                        foreach (var i in _sources[_dest])
                        {
                            _destinations.Enqueue(i);
                        }
                    }
                }
            }


            public string Part1(IDataProcessor dp)
            {
                Process(dp);

                // Any exception would have been already thrown above.

                int _accumulator = 0, _index = 0;

                while (_instructions[_index].Executed is false)
                {
                    _instructions[_index].Executed = true;

                    switch (_instructions[_index].Type)
                    {
                        case InstructionType.Accumulate:
                            _accumulator += _instructions[_index++].Value;
                            break;
                        case InstructionType.Jump:
                            _index += _instructions[_index].Value;
                            break;
                        case InstructionType.NoOperation:
                            _index++;
                            break;
                    }
                }

                return $"Part 1 result is {_accumulator}";
            }

            public string Part2(IDataProcessor dp)
            {
                Process(dp);

                // Any exception would have been already thrown above.

                // Theory is that if you now step through from the start, you'll hit one of these points
                // if you switch instructions.

                int _accumulator = 0, _index = 0;

                var executed_part_1 = false;

                while (executed_part_1 is false)
                {
                    if (_instructions[_index].Executed)
                    {
                        throw new ArgumentException("Your input data is rubbish.");
                    }

                    _instructions[_index].Executed = true;

                    if (_instructions[_index].Type == InstructionType.Accumulate)
                    {
                        _accumulator += _instructions[_index++].Value;
                    }
                    else
                    {
                        var _index_new = _index + (_instructions[_index].Type == InstructionType.NoOperation ? _instructions[_index].Value : 1);

                        // Indicates the switch in instruction if a jump / no-op is the valid switch.

                        if (_index_new >= 0 && _index_new <= _instructions.Count &&
                            (_index_new == _instructions.Count ||
                            _instructions[_index_new].State == InstructionState.ExecutedFromEnd))
                        {
                            _index = _index_new;
                            executed_part_1 = true;
                        }
                        else
                        {
                            _index += (_instructions[_index].Type == InstructionType.Jump ? _instructions[_index].Value : 1);
                        }
                    }
                }

                // You've found the switch in instruction, but still need to carry on to the end.

                while (_index != _instructions.Count)
                {
                    _instructions[_index].Executed = true;

                    switch (_instructions[_index].Type)
                    {
                        case InstructionType.Accumulate:
                            _accumulator += _instructions[_index++].Value;
                            break;
                        case InstructionType.Jump:
                            _index += _instructions[_index].Value;
                            break;
                        case InstructionType.NoOperation:
                            _index++;
                            break;
                    }
                }

                return $"Part 2 result is {_accumulator}";
            }
        }

        public enum InstructionType
        {
            Accumulate,
            Jump,
            NoOperation,
        }

        public enum InstructionState
        {
            NoExecution,
            ExecutedFromStart,
            ExecutedFromEnd,
        }

        public class Instruction
        {
            private static Dictionary<string, InstructionType> _types = new()
            {
                ["acc"] = InstructionType.Accumulate,
                ["jmp"] = InstructionType.Jump,
                ["nop"] = InstructionType.NoOperation,
            };

            private static int GetValue(string i)
            {
                if (i.Length > 1)
                {
                    var multiplier = 0;

                    switch (i[0])
                    {
                        case '+':
                            multiplier = 1;
                            break;
                        case '-':
                            multiplier = -1;
                            break;
                        default:
                            throw new ArgumentException("Your input data is rubbish.");
                    }

                    if (int.TryParse(i[1..], out var value))
                    {
                        return multiplier * value;
                    }
                    else
                    {
                        throw new ArgumentException("Your input data is rubbish.");
                    }
                }
                else
                {
                    throw new ArgumentException("Your input data is rubbish.");
                }
            }

            public InstructionType Type { get; init; }
            public int Value { get; init; }
            public InstructionState State { get; set; }
            public bool Executed { get; set; }

            public Instruction(string i)
            {
                var i_split = i.Split(' ');

                if (i_split.Length == 2)
                {
                    if (_types.TryGetValue(i_split[0], out var _type))
                    {
                        Type = _type;
                        Value = GetValue(i_split[1]);
                        State = InstructionState.NoExecution;
                        Executed = false;
                    }
                    else
                    {
                        throw new ArgumentException("Your input data is rubbish.");
                    }
                }
                else
                {
                    throw new ArgumentException("Your input data is rubbish.");
                }
            }
        }

        public class Day8
        {
            public static void Main(string[] args)
            {
                var data = new DataProcessor(8);
                var day = new Day8_Processor();

                Console.WriteLine(day.Part1(data));
                Console.WriteLine(day.Part2(data));
            }
        }
    }
}
