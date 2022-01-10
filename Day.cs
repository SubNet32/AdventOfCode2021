using AdventOfCode.Basic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode
{
    class Day
    {       
      
        public static void PrintResult(int day, string result)
        {
            TimeSpan time = DateTime.Now - Process.GetCurrentProcess().StartTime;
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("--");
            Console.WriteLine("---->Result for Day" + day + ": " + result);
            Console.WriteLine("---->After " + time.ToString(@"hh\:mm\:ss"));
            Console.WriteLine("--");
            Console.WriteLine("---------------------------------------");
        }
    }
}
