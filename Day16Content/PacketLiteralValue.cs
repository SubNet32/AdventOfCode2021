using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace AdventOfCode.Day16Content
{
    class PacketLiteralValue
    {
        public Value value;
        public List<Value> subValues;
        public bool isLastPacket;
        public bool isComplete;

        public PacketLiteralValue()
        {
            subValues = new List<Value>();
            isLastPacket = false;
            isComplete = false;
        }

        public bool Feed(string feed)
        {
            if (isComplete)
                throw new Exception("PacketLiteralValue is already complete");
            if (subValues.Count == 0 || subValues.Last().isComplete)
            {
                if (feed == "0")
                {
                    isLastPacket = true;
                    Packet.Log("Adding LAST LiteralValue '" + feed + "'");
                }
                else
                {
                    Packet.Log("Adding new LiteralValue");
                }
                subValues.Add(new Value(4));
                return false;
            }

            Value l = subValues.Last();

            if (l.Feed(feed) && isLastPacket)
            {
                isComplete = true;
                value = Value.CombineValuesFromList(subValues);
                Console.WriteLine("LiteralValue complete: " + value.GetPrintString());
                return true;
            }
            return isComplete;

        }
    }
}
