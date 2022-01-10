using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day24Content
{
    class Operation
    {
        public string type;
        public string varA;
        public string varB;
        public bool bIsValue;
        public long valueB;

        private Memory memory;

        public Operation(string s)
        {
            string[] sp = s.Split(' ');
            this.type = sp[0];
            this.varA = sp[1];
            if (sp.Length > 2)
            {
                this.varB = sp[2];
                CheckVarB();
            }
            memory = Memory.GetInstance();
        }

        public void CheckVarB()
        {
            for(int i = 0; i <= 9; i++)
            {
                if(varB.Contains(i.ToString()))
                {
                    bIsValue = true;
                    valueB = int.Parse(varB);
                    return;
                }
            }
        }

        public void Execute()
        {
            switch(type)
            {
                case "inp": Inp(); break;
                case "add": Add(); break;
                case "mul": Mul(); break;
                case "div": Div(); break;
                case "mod": Div(); break;
                case "eql": Eql(); break;
            }
          
        }

        private void Inp()
        {
            memory.Inp(varA);
            Variable a = memory.Load(varA);
            Utilities.Log(type + " " + a.ToString());
        }

        private void Add()
        {
            Variable a = memory.Load(varA);
            Variable b = GetVarB();
            Log(a, b);
            memory.Set(varA, a.value + b.value);
            LogRes(a);
        }
        private void Mul()
        {
            Variable a = memory.Load(varA);
            Variable b = GetVarB();
            Log(a, b);
            memory.Set(varA, a.value * b.value);
            LogRes(a);
        }
        private void Div()
        {
            Variable a = memory.Load(varA);
            Variable b = GetVarB();
            Log(a, b);
            memory.Set(varA, Convert.ToInt64(Math.Truncate((double)a.value / (double)b.value)));
            LogRes(a);
        }
        private void Mod()
        {
            Variable a = memory.Load(varA);
            Variable b = GetVarB();
            Log(a, b);
            memory.Set(varA, a.value % b.value);
            LogRes(a);
        }
        private void Eql()
        {
            Variable a = memory.Load(varA);
            Variable b = GetVarB();
            Log(a, b);
            memory.Set(varA, a.value == b.value ? 1 : 0);
            LogRes(a);
        }
        private Variable GetVarB()
        {
            if(bIsValue)
            {
                return new Variable("t", valueB);
            }
            else
            {
                return memory.Load(varB);
            }
        }

        private void Log(Variable a, Variable b)
        {
            Utilities.Log(type+" " + a.ToString() + " " + b.ToString());
        }

        private void LogRes(Variable a)
        {
            Utilities.Log("--> " + a.ToString());
        }
    }
}
