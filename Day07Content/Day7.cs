using AdventOfCode.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class CrabPosition
    {
        public int position;

        public CrabPosition(int startPos)
        {
            this.position = startPos;
        }

        public int PositionDifference(int newPos)
        {
            int diff = Math.Abs(newPos - position);
            int result = 0;
            if (diff % 2 == 0)
            {
                result = (diff + 1) * (diff / 2);
            }
            else
            {
                result = (diff) * Convert.ToInt32(Math.Ceiling(((double)(diff) / 2.0)));
            }

           // Console.WriteLine("Move from " + position + " to " + newPos + " : " + result + " fuel");
            return result;
        }
    }

    class Day7 : Day
    {
        public static int day = 7;
        
        public static void GetResult1()
        {
            //16,1,2,0,4,2,7,1,2,14
            string[] input = InputHandler.GetInputForDay(day)[0].Split(',');
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            CrabPosition[] positions = new CrabPosition[input.Length];
            int sum = 0;
            int minPosition = 0;
            int maxPosition = 0;
            for(int i = 0; i < input.Length; i++)
            {
                positions[i] = new CrabPosition(int.Parse(input[i]));
                sum += positions[i].position;
                minPosition = Math.Min(minPosition, positions[i].position);
                maxPosition = Math.Max(maxPosition, positions[i].position);
            }
            
            Console.WriteLine("Loaded " + positions.Length + " Positions");
            Console.WriteLine("They sum up to " + sum);
            Console.WriteLine("Mean " + (sum/positions.Length));

            bool searching = true;
            int fuelSum = 0;
            int minFuelSum = int.MaxValue;
            int minFuelSumPos = int.MaxValue;
            //for (int p = minPosition; p < maxPosition; p++)
            //{
            //    fuelSum = 0;
            //    for (int i = 0; i < positions.Length; i++)
            //    {
            //        fuelSum += positions[i].PositionDifference(p);
            //    }
            //    Console.WriteLine("FuelConsumption to reach position " + p + " = " + fuelSum);
            //    Console.WriteLine("");
            //    if (fuelSum < minFuelSum)
            //    {
            //        minFuelSum = fuelSum;
            //        minFuelSumPos = p;
            //    }
            //}

            int targetPos = Convert.ToInt32(Math.Round((double)(sum / positions.Length)));
            int fuelSumNext;
            int fuelSumPrev;
            int tries = 0;
            fuelSum = GetFuelSum(positions, targetPos);
            while (searching)
            {
                tries++;
                Console.WriteLine("Position: " + targetPos);
                fuelSumNext = GetFuelSum(positions, targetPos + 1);
                fuelSumPrev = GetFuelSum(positions, targetPos - 1);
                if(fuelSumNext < fuelSum)
                {
                    targetPos = targetPos + 1;
                    fuelSum = fuelSumNext;
                }
                else if(fuelSumPrev < fuelSum)
                {
                    targetPos = targetPos - 1;
                    fuelSum = fuelSumPrev;
                }
                else
                {
                    searching = false;
                    minFuelSum = fuelSum;
                }
            }

            Console.WriteLine("Tries : "+ tries);
            PrintResult(day, minFuelSum.ToString());
        }

        public static int GetFuelSum(CrabPosition[] positions, int targetPosition)
        {
            int fuelSum = 0;
            for (int i = 0; i < positions.Length; i++)
            {
                fuelSum += positions[i].PositionDifference(targetPosition);
            }
            return fuelSum;
        }
    }
}
