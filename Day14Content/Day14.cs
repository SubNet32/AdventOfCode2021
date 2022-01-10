using AdventOfCode.Basic;
using AdventOfCode.Day14Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day14 : Day
    {
        public static int day = 14;
        public static int cylces = 40;


        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);
            PolymerSolver solver = new PolymerSolver(input);

            solver.SetStartInput(input[0]);
            

            for (int i = 0; i < cylces; i++)
            {

                solver.DevelopePolymores();
                //polymores = solver.DevelopePolymores(polymores);
                ////solver.PrintPolymerList(polymores);
                ////solver.CalcPolyDifference(polymores).ToString();
                solver.CalcPolyDifference();
                Console.WriteLine("Progress: Step:"+(i+1)+ " " + (((float)i/(float)cylces)*100.0).ToString("00") + "%" );
                Console.WriteLine("-------------------------------------");
            }


            PrintResult(day, solver.CalcPolyDifference().ToString());
        }
    }
}
