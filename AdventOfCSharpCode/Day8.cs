using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCSharpCode
{
    namespace Day8
    {
        public class Day8_Processor: iDayProcessor
        {
            private Instruction[] _instructions;

            public string Part1(iDataProcessor dp)
            {
                _instructions = dp.Data.Select(d => new Instruction(d))
                    .ToList()
                    .Union( new[] { new Instruction("nop +0") })
                    .ToArray();
                
                int global_accumulator = 0, global_index = 0;

                while (_instructions[global_index].State is InstructionState.NoExecution)
                {
                    _instructions[global_index].State = InstructionState.ExecutedFromStart;

                    switch (_instructions[global_index].Type)
                    {
                        case InstructionType.Accumulate:
                            global_accumulator += _instructions[global_index++].Value;
                            break;
                        case InstructionType.Jump:
                            global_index += _instructions[global_index].Value;
                            break;
                        case InstructionType.NoOperation:
                            global_index++;
                            break;
                    }

                    if (global_index < 0 || global_index >= _instructions.Length)
                    {
                        throw new ArgumentException("Your input data is rubbish.");
                    }
                }

                return $"Part 1 result is {global_accumulator}";
            }

            public string Part2(iDataProcessor dp)
            {
                _instructions = dp.Data.Select(d => new Instruction(d))
                    .ToList()
                    .Union(new[] { new Instruction("nop +0") })
                    .ToArray();

                // _sources maps destination indices to all the source indices that send them there
                // with a single instruction.
                
                Dictionary<int, List<int>> _sources = new ();

                var _index_source = 0;

                foreach (var i in _instructions)
                {
                    var _index_destination = _index_source;

                    if (i.Type == InstructionType.Jump)
                    {
                        _index_destination += i.Value;
                    }
                    else
                    {
                        _index_destination++;
                    }

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

                Queue<int> _destinations = new ();
                _destinations.Enqueue(_instructions.Length - 1);

                while (_destinations.Any())
                {
                    var _dest = _destinations.Dequeue();
                    _instructions[_dest].State = InstructionState.ExecutedFromEnd;
                    if (_sources.ContainsKey(_dest))
                    {
                        foreach (var i in _sources[_dest])
                        {
                            _destinations.Enqueue(i);
                        }
                    }
                }

                // Theory is that if you now step through from the start, you'll hit one of these points
                // if you switch instructions.

                // Nearly done here.

                int global_accumulator = 0, global_index = 0;

                while (_instructions[global_index].State is InstructionState.NoExecution)
                {
                    _instructions[global_index].State = InstructionState.ExecutedFromStart;

                    switch (_instructions[global_index].Type)
                    {
                        case InstructionType.Accumulate:
                            global_accumulator += _instructions[global_index++].Value;
                            break;
                        case InstructionType.Jump:
                            global_index += _instructions[global_index].Value;
                            break;
                        case InstructionType.NoOperation:
                            global_index++;
                            break;
                    }

                    if (global_index < 0 || global_index >= _instructions.Length)
                    {
                        throw new ArgumentException("Your input data is rubbish.");
                    }
                }

                return "Part 2!";
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
    }
}
