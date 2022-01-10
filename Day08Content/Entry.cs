using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day08Content
{
    class Entry
    {
        public string[] inputs;
        public string[] outputs;
        public List<string> allWords;

        public int value;

        public Mapping mapping;

        public Entry(string s)
        {
            string[] split = s.Split(" | ");
            inputs = split[0].Split(' ');
            outputs = split[1].Split(' ');

            allWords = new List<string>(inputs);
            allWords.AddRange(outputs);
            mapping = new Mapping(allWords);

            this.value = mapping.DecodeInputArray(outputs);
            Console.WriteLine(value);
        }

    }
}
