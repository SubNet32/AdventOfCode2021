using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day24Content
{
    class Result
    {
        public int changedIndex;
        public List<long> inputs;
        public List<long> results;
        public bool noChange;

        public Result(int changedIndex)
        {
            this.changedIndex = changedIndex;
            inputs = new List<long>();
            results = new List<long>();
        }

        public void AddResult(long input, long result)
        {
            inputs.Add(input);
            results.Add(result);
        }

        public void CheckResults()
        {
            results.Sort();

            Console.WriteLine("Results for index: "+changedIndex);
            foreach (long l in results)
            {
                Console.WriteLine(l);
            }

            long diffSum = 0;
            for (int i = 0; i < results.Count-1; i++)
            {
                long diff = Math.Abs(results[i + 1] - results[i]);
                diffSum += diff;
                Console.WriteLine(diff);
            }
            if(diffSum==0)
            {
                noChange = true;
                Console.WriteLine("No Change");
            }

        }

    }
}
