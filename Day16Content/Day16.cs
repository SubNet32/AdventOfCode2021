using AdventOfCode.Basic;
using AdventOfCode.Day16Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day16 : Day
    {
        public static int day = 16;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            Packet packet = new Packet();

            string hex = Utilities.ConvertHexStringToBinaryString(input[0]);
            Console.WriteLine("Input: " + hex);
            foreach(char s in hex)
            {
                Packet.Log("Feeding " + s);
                if (packet.Feed(s.ToString()))
                {
                    break;
                }
            }

            long result = packet.GetResult();
            packet.PrintPacket("");
            Console.WriteLine("");

            PrintResult(day, result.ToString());
        }
    }
}
