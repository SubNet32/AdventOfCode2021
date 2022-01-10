using AdventOfCode.Basic;
using AdventOfCode.Day03Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class BitCounter
    {
        public int count0;
        public int count1;

        public void AddBit(char s)
        {
            if(s=='0')
            {
                count0++;
            }
            else if(s=='1')
            {
                count1++;
            }
            else
            {
                throw new Exception("Error in Bit.AddBit input is not 0/1 but '" + s + "'");
            }
        }

        public void AddBit(int b)
        {
            if (b == 0)
            {
                count0++;
            }
            else if (b == 1)
            {
                count1++;
            }
            else
            {
                throw new Exception("Error in Bit.AddBit input is not 0/1 but '" + b + "'");
            }
        }

        public string GetHigherValueString()
        {
            if(count0>count1)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        public string GetLowerValueString()
        {
            if (count0 > count1)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
    }

   

    class CheckEntryResult
    {
        List<Entry> zeroEntries = new List<Entry>();
        List<Entry> oneEntries = new List<Entry>();

        public void AddEntry(Entry e, int bitPos)
        {
            int bit = e.GetBit(bitPos);
            if (bit == 0)
            {
                zeroEntries.Add(e);
            }
            else if (bit == 1)
            {
                oneEntries.Add(e);
            }
            else
            {
                throw new Exception("Error in CheckEntriesForBit. Bit is not 0/1 but '" + bit + "'");
            }
        }

        public List<Entry> GetLargerEntryList()
        {
            if(zeroEntries.Count > oneEntries.Count)
            {
                return zeroEntries;
            }
            else
            {
                return oneEntries;
            }
        }
        public List<Entry> GetShorterEntryList()
        {
            if (zeroEntries.Count <= oneEntries.Count)
            {
                return zeroEntries;
            }
            else
            {
                return oneEntries;
            }
        }
    }

    class Day3 : Day
    {
        public static int day = 3;

        //0010_0001_0101
        //010010111110
        //001010110111
        //001001011101

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            BitCounter[] bitCounter = new BitCounter[input[0].Length];
            Console.WriteLine("Created bitCounter[] with length: " + bitCounter.Length);

            foreach (string s in input)
            {
                for(int i = 0; i < s.Length; i++)
                {
                    if(bitCounter[i]==null) { bitCounter[i] = new BitCounter(); }

                    bitCounter[i].AddBit(s[i]);
                }
            }

            string gamma = "";
            string epsilon = "";
            for (int i = 0; i < bitCounter.Length; i++)
            {
                gamma += bitCounter[i].GetHigherValueString();
                epsilon += bitCounter[i].GetLowerValueString();
            }

            Console.WriteLine("Gamma: " + gamma + " dec: "+Utilities.ConvertBinaryStingToInt(gamma));
            Console.WriteLine("Epsilon: " +epsilon+ " dec: " + Utilities.ConvertBinaryStingToInt(epsilon));

            PrintResult(day, Convert.ToString((Utilities.ConvertBinaryStingToInt(gamma) * Utilities.ConvertBinaryStingToInt(epsilon))));
        }

        public static void GetResult2()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            List<Entry> entries = new List<Entry>();
            BitCounter bitCounter = new BitCounter();

            foreach(string s in input)
            {
                entries.Add(new Entry(s));
            }

            List<Entry> oxygenEntries = new List<Entry>(entries);
            List<Entry> co2Entries = new List<Entry>(entries);

            Entry oxyResult = null;
            Entry co2Result = null;

            for (int i = 0; i < input.Length; i++)
            {
                if (oxyResult == null)
                {
                    CheckEntryResult oxygenCheckResult = CheckEntriesForBit(oxygenEntries, i);
                    oxygenEntries = new List<Entry>(oxygenCheckResult.GetLargerEntryList());
                    Console.WriteLine("Remaining items in oxyList " + oxygenEntries.Count);
                    if (oxygenEntries.Count == 1)
                    {
                        Console.WriteLine("Found remaining item in oxygenEntryList -> " + oxygenEntries[0].bitString);
                        oxyResult = oxygenEntries[0];
                    }
                    else if(oxygenEntries.Count== 0)
                    {
                        throw new Exception("Error no remainging items in oxygenEntryList");
                    }
                }
                if (co2Result == null)
                {
                    CheckEntryResult co2CheckResult = CheckEntriesForBit(co2Entries, i);
                    co2Entries = new List<Entry>(co2CheckResult.GetShorterEntryList());
                    if (co2Entries.Count == 1)
                    {
                        Console.WriteLine("Found remaining item in co2EntryList -> " + co2Entries[0].bitString);
                        co2Result = co2Entries[0];
                    }
                    else if (co2Entries.Count == 0)
                    {
                        throw new Exception("Error no remainging items in co2EntryList");
                    }
                }
            }

            //long result = Utilities.ConvertBinaryStingToInt(oxyResult.bitString) * Utilities.ConvertBinaryStingToInt(co2Result.bitString);
            //PrintResult(day, Convert.ToString(long));

        }

        public static CheckEntryResult CheckEntriesForBit(List<Entry> entries, int bitPos)
        {
            CheckEntryResult result = new CheckEntryResult();

            foreach (Entry e in entries)
            {
                result.AddEntry(e, bitPos);
            }

            return result;
        }


    }
}
