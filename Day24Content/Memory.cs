using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Day24Content
{
    public sealed class Memory
    {

        public static Memory instance;

        private List<Variable> var = new List<Variable>();
        private long input = 0;
        public bool inputLoaded;

        public static Memory GetInstance()
        {
            if(instance==null)
            {
                instance = new Memory();
            }
            return instance;
        }

        public void Inp(string name)
        {
            if(!Exists(name))
            {
                var.Add(new Variable(name, input));
            }
            else
            {
                Load(name).value = input;
            }
            inputLoaded = true;
        }

        public void SetInput(long input)
        {
            this.input = input;
            inputLoaded = false;
        }

        public Variable Load(string name)
        {
            return ExistsOrCreate(name);
        }

        public void Set(string name, long value)
        {
            ExistsOrCreate(name).value = value;
        }
        public bool Exists(string name)
        {
            int n;
            if(int.TryParse(name, out n))
            {
                throw new Exception("Wrong");
            }
            return (var.Any(v => v.name == name));
        }
        public Variable ExistsOrCreate(string name)
        {
            if (!Exists(name))
            {
                var.Add(new Variable(name, 0));
            }
            return var.Find(v => v.name == name);
        }

        public List<string> GetMemory()
        {
            return var.Select(v => v.ToString()).ToList();
        }

        public void Clear()
        {
            var.Clear();
            input = 0;
            inputLoaded = false;
        }
    }
}
