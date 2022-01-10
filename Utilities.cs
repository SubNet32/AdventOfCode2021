using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    static class Utilities
    {
        public static bool debug=true;

        public static void Log(string s)
        {
            if (debug)
                Console.WriteLine(s);
        }

        public static long ConvertBinaryStingToInt(string binS)
        {
            long result = 0;
            for(int i = 0; i < binS.Length; i++)
            {
                if(binS[i] == '1')
                {
                    result += Convert.ToInt64(Math.Pow(2.0, binS.Length - i - 1));
                }
            }

            return result;
        }

        public static string ConvertHexStringToBinaryString(string hex)
        {
            string result = "";
            foreach(char c in hex)
            {
                result += ConvertHexCharToBinary(c);
            }
            return result;
        }

        public static string ConvertHexCharToBinary(char hex)
        {
            return Convert.ToString(Convert.ToInt32(hex.ToString(), 16), 2).PadLeft(4, '0');
        }

        public static string FormatNumberString(string s)
        {
            s = s.Replace("  ", " ");
            if(s[0]==' ')
            {
                s = s.Remove(0, 1);
            }
            return s;
        }

        public static int NormalizeInt(int i)
        {
            if(i > 0)
            {
                i = 1;
            }
            else if( i < 0)
            {
                i = -1;
            }
            return i;
        }

        public static int DivideByAndRoundUp(int value, int div)
        {
            return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(value) / Convert.ToDouble(div)));
        }

        public static int DivideByAndRoundDown(int value, int div)
        {
            return Convert.ToInt32(Math.Floor(Convert.ToDouble(value) / Convert.ToDouble(div)));
        }
        public static float ConvertToRadians(float angle)
        {
            return Convert.ToSingle((Math.PI / 180.0)) * angle;
        }

        public static float ConvertToDegree(float radians)
        {
            return Convert.ToSingle(((radians * 180.0) / Math.PI));
        }
    }
}
