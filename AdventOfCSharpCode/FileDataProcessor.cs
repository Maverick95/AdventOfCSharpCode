﻿using FakeItEasy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCSharpCode
{
    public class DataEnumerator : IEnumerator<string>
    {
        private readonly string[] _data;
        private int _index = -1;

        public DataEnumerator(string[] data)
        {
            if (data.Length is 0)
            {
                throw new ArgumentException("Your input is rubbish.");
            }
            _data = new string[data.Length];
            data.CopyTo(_data, 0);
        }

        public DataEnumerator(string filename)
        {
            if (File.Exists(filename))
            {
                _data = File.ReadAllLines(filename)
                    .Select(x => x.Trim())
                    .Where(x => x.Length > 0)
                    .ToArray();
            }
            else
            {
                throw new ArgumentException("Your input data is rubbish.");
            }
        }

        public string Current
        {
            get => _index is -1 ? null : _data[_index];
        }

        object IEnumerator.Current
        {
            get => Current;
        }

        public bool MoveNext() => ++_index < _data.Length;

        public void Reset()
        {
            _index = -1;
        }

        public void Dispose()
        {

        }
    }

    public class StringDataProcessor: IEnumerable<string>
    {
        private readonly DataEnumerator _enumerator;

        public StringDataProcessor(string[] data)
        {
            _enumerator = new(data);
        }

        public IEnumerator<string> GetEnumerator() => _enumerator;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class FileDataProcessor : IEnumerable<string>
    {
        private static string DataPath { get; } = "C:\\DEV\\AdventOfCSharpCode\\AdventOfCSharpCode\\Data";

        private readonly DataEnumerator _enumerator;

        public FileDataProcessor(int day)
        {
            string path = $"{DataPath}\\{day}.txt";
            _enumerator = new(path);
        }

        public IEnumerator<string> GetEnumerator() => _enumerator;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
