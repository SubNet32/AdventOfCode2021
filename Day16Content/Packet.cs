using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Day16Content
{

    class Packet
    {
        public Value version;
        public Value typeId;
        public Value lengthTypeId;
        public bool lengthType0;
        public bool lengthType1;
        public Value subCounter;
        public bool isComplete;

        public long saveValue;

        public string packetString;

        public List<Packet> subPackets;
        public PacketLiteralValue literalValue;

        public static bool debug = true;
        public static void Log(string s)
        {
            if (debug)
                Console.WriteLine(s);
        }

        public Packet()
        {
            version = new Value(3);
            typeId = new Value(3);
            lengthTypeId = new Value(1);
            subPackets = new List<Packet>();
            literalValue = new PacketLiteralValue();
            isComplete = false;
            Log("New Packet created");
        }

        public void PrintPacket(string modifyer)
        {
            string s = "V<" + version.GetPrintString() + ">" + " T<" + typeId.GetPrintString() + ">";
            if(typeId.value==4)
            {
                s += "  Value: " + literalValue.value.GetPrintString();
            }
            else
            {
                s += " LenTypeId<" + lengthTypeId.GetPrintString()+">";
                s += " subCounter<" + subCounter.GetPrintString() + ">";
            }
            s += "     value: " + saveValue;
            Console.WriteLine(modifyer+">"+s);
            if(subPackets.Count>0)
            {
                Console.WriteLine(modifyer+">Packet contains '" +subPackets.Count+"' SubPackets");
                foreach(Packet p in subPackets)
                {
                    p.PrintPacket(modifyer+"--");
                }
                Console.WriteLine("");
            }
        }

        public long GetResult()
        {
            long sum = 0;

            if(typeId.value==0)
            {
                sum = GetOfValues(GetType.sum);
            }
            else if(typeId.value==1)
            {
                sum = GetOfValues(GetType.product);
            }
            else if(typeId.value == 2)
            {
                sum = GetOfValues(GetType.min);
            }
            else if (typeId.value == 3)
            {
                sum = GetOfValues(GetType.max);
            }
            else if (typeId.value == 4)
            {
                sum = GetOfValues(GetType.value);
            }
            else if (typeId.value == 5)
            {
                sum = GetOfValues(GetType.GT);
            }
            else if (typeId.value == 6)
            {
                sum = GetOfValues(GetType.LT);
            }
            else if (typeId.value == 7)
            {
                sum = GetOfValues(GetType.EQ);
            }

            saveValue = sum;
            return sum;
        }

        public enum GetType
        {
            value, sum, product, min, max, GT, LT, EQ 
        }
        public long GetOfValues(GetType type)
        {
            if(type==GetType.value)
            {
                return literalValue.value.value;
            }
            if(type==GetType.LT)
            {
                return Convert.ToInt64(subPackets[0].GetResult() < subPackets[1].GetResult());
            }
            if (type == GetType.GT)
            {
                return Convert.ToInt64(subPackets[0].GetResult() > subPackets[1].GetResult());
            }
            if (type == GetType.EQ)
            {
                return Convert.ToInt64(subPackets[0].GetResult() == subPackets[1].GetResult());
            }

            long sum = 0;
            if (type == GetType.product)
                sum = -1;
            long minRes = -1;
            long maxRes = 0;
            foreach (Packet p in subPackets)
            {
                long res = p.GetResult();
                if (minRes == -1 || res < minRes)
                    minRes = res;
                if (res > maxRes)
                    maxRes = res;

                if (type == GetType.sum)
                    sum += res;
                else if (type == GetType.product)
                {
                    if (sum == -1)
                        sum = res;
                    else
                        sum *= res;
                }
            }

            if (type == GetType.min)
                return minRes;
            if (type == GetType.max)
                return maxRes;

            return sum;
        }

        public long GetVersionSum()
        {
            long sum = version.value;
            if(subPackets.Count>0)
            {
                foreach(Packet p in subPackets)
                {
                    sum += p.GetVersionSum();
                }
            }
            return sum;
        }

        public bool Feed(string s)
        {
            packetString += s;
            if (!version.isComplete)
            {
                version.Feed(s);
                return false;
            }
            if (!typeId.isComplete)
            {
                typeId.Feed(s);
                return false;
            }

            if (typeId.value==4)
            {
                if(literalValue.Feed(s))
                {
                    isComplete = true;
                }
                return isComplete;
            }
            else if(!lengthTypeId.isComplete)
            {
                lengthTypeId.Feed(s);
                if(lengthTypeId.value==0) //total length of bits
                {
                    subCounter = new Value(15);
                    lengthType0 = true;
                }
                else //number of sub packets
                {
                    subCounter = new Value(11);
                    lengthType1 = true;
                }

                return false;
            }
            else if(!subCounter.isComplete)
            {
                subCounter.Feed(s);
                return false;
            }
            else
            {
                if(ManageSubPackets(s))
                {
                    isComplete = true;
                    Log("Packet complete!");
                }
                return isComplete;
            }
        }

        private int subPacketCounter;
        public bool ManageSubPackets(string s)
        {
            if (isComplete)
                throw new Exception("Packet is already complete");
            if(subPackets.Count==0 || subPackets.Last().isComplete)
            {
                subPackets.Add(new Packet());
            }

            if(lengthType0)
            {
                subPacketCounter++;
            }

            Packet p = subPackets.Last();
            p.Feed(s);
            if(p.isComplete)
            {
                if(lengthType1)
                {
                    subPacketCounter++;
                }

                if(subPacketCounter == subCounter.value)
                {
                    isComplete = true;
                    return isComplete;
                }
            }

            if(subPacketCounter>=subCounter.value)
            {
                throw new Exception("SubPacketCounter: '" + subPacketCounter + "' exceeded subCounter: " + subCounter.value);
            }

            return false;

        }

    
    
    }
}
