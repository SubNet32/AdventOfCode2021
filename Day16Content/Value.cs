using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day16Content
{
    class Value
    {

        public string valueString;
        public long value;
        public int length;
        public bool isComplete;

        public Value(int length)
        {
            this.length = length;
            this.isComplete = false;
        }

        public bool CheckComplete()
        {
            if (valueString.Length == length)
            {
                value = Utilities.ConvertBinaryStingToInt(valueString);
                isComplete = true;
            }
            return isComplete;
        }

        public bool Feed(string s)
        {
            if (isComplete)
                throw new Exception("Value overfed");
            valueString += s;
            if(CheckComplete())
            {
                Packet.Log("Value complete: " + GetPrintString());
                return true;
            }

            return false;
        }

        public bool SetValueString(string s)
        {
            if (isComplete)
                throw new Exception("Value is already complete");
            valueString = s;
            return CheckComplete();
        }

        public string GetPrintString()
        {
            string s = "(" + value + ")" + valueString;
            if (!isComplete)
            {
                s += "..";
            }
            return s;
        }

        public static Value Combine(Value v1, Value v2)
        {
            if (v1 == null)
                return v2;
            else if (v2 == null)
                return v1;
            Value nv = new Value(v1.length + v2.length);
            nv.SetValueString((v1.valueString + v2.valueString));
            return nv;
        }

        public static Value CombineValuesFromList(List<Value> values)
        {
            if (values.Count > 0)
            {
                Value nv = null;
                for (int i = 0; i < values.Count; i++)
                {
                    nv = Value.Combine(nv, values[i]);
                }
                return nv;
            }
            return null;
        }
    }
}
