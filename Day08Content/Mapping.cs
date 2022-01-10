using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day08Content
{

    class Mapping
    {
        public Mask mask; // 0:Top 1:Top-left 2:Top-Right 3:Middle 4:Bottom-left 5:Bottom-Right 6:Bottom

        public static string allPossibleWires = "abcdefg";

        List<string> decodingListShortWords;
        List<string> decodingListLongWords;

        public Mapping(List<string> decodingStrings)
        {
            mask = new Mask(allPossibleWires);

            decodingListShortWords = new List<string>();
            decodingListLongWords = new List<string>();
            foreach (string s in decodingStrings)
            {
                if (s.Length <= 4)
                    decodingListShortWords.Add(s);
                else
                    decodingListLongWords.Add(s);
            }
            StartDecoding();
        }

        public void StartDecoding()
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Start decoding");
            mask.PrintMask();

            foreach (string s in decodingListShortWords)
            {
                Console.WriteLine("Mapping decodingString: " + s);
                switch (s.Length)
                {
                    case 2: mask.MapStringToMask(s, new int[] { 2, 5 }); break;
                    case 3: mask.MapStringToMask(s, new int[] { 0, 2, 5 }); break;
                    case 4: mask.MapStringToMask(s, new int[] { 1, 2, 3, 5 }); break;
                }
                mask.PrintMask();
                if (mask.IsComplete())
                {
                    return;
                }
            }
            mask.PrintMask();
            foreach (string s in decodingListLongWords)
            {
                Console.WriteLine("Mapping decodingString(long): " + s);
                switch (s.Length)
                {
                    case 5: mask.MapStringToMaskExclusive(s, new int[] { 0, 3, 6 }); break;
                    case 6: mask.MapStringToMaskExclusive(s, new int[] { 0, 1, 5, 6 }); break;
                }
                mask.PrintMask();
                if(mask.IsComplete())
                {
                    return;
                }
            }
        }

        public int DecodeInputArray(string[] input)
        {
            if(mask != null && input!=null && input.Length>0)
            {
                string result = "";
                foreach(string s in input)
                {
                    if(!string.IsNullOrEmpty(s))
                    {
                        result += mask.Decode(s).ToString();
                    }
                }
                return int.Parse(result);
            }
            throw new Exception("Error in DecodeInputArray");
        }
 

        //public void MapString(string s)
        //{
        //    if (complete)
        //        return;
        //    Console.WriteLine("-----------------------------------------------------");
        //    Console.WriteLine("Mapping string " + s);
        //    PrintMapping();
        //    Console.WriteLine("");
        //    if (s.Length <= 4)
        //    {
        //        switch (s.Length)
        //        {
        //            case 2: MapStringSize2(s); break;
        //            case 3: MapStringSize3(s); break;
        //            case 4: MapStringSize4(s); break;
        //        }
        //    }
        //    else
        //    {

        //    }
        //    PrintMapping();
        //    Console.WriteLine("");
        //    CheckMapping(s);
        //    Console.WriteLine("");
        //    Console.WriteLine("Endresult");
        //    PrintMapping();
        //}

        //public void CheckMapping(string inputS)
        //{
        //    Console.WriteLine("Cecking Mapping");
        //    for (int i = 0; i < 7; i++)
        //    {
        //        if (possibleSeg[i].Length == 2)
        //        {
        //            for (int c = 0; c < 7; c++)
        //            {
        //                if (i != c && possibleSeg[i] == possibleSeg[c])
        //                {
        //                    Console.WriteLine("Found 2 equal seq " + " " + i + " = " + c + "  " + possibleSeg[i]);

        //                    if (CheckEasyDelete(i, c))
        //                    {
        //                        seg[c] = possibleSeg[i][0].ToString();
        //                        seg[i] = possibleSeg[i][1].ToString();
        //                        RemoveAllOf(possibleSeg[i]);
        //                        PrintMapping();
        //                        i = 0;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("No easy solution");
        //                        string[] testMask1 = seg;
        //                        testMask1[i] = possibleSeg[i][0].ToString();
        //                        testMask1[c] = possibleSeg[i][1].ToString();
        //                        string[] testMask2 = seg;
        //                        testMask2[c] = possibleSeg[i][0].ToString();
        //                        testMask2[i] = possibleSeg[i][1].ToString();

        //                        int resultTest1 = Decode(inputS, testMask1);
        //                        int resultTest2 = Decode(inputS, testMask2);

        //                        Console.WriteLine("TestResult1: " + resultTest1 + "TestResult2: " + resultTest2);

        //                        if (resultTest1 == -1 && resultTest2 >= 0)
        //                        {
        //                            seg[c] = possibleSeg[i][0].ToString();
        //                            seg[i] = possibleSeg[i][1].ToString();
        //                            RemoveAllOf(possibleSeg[i]);
        //                            PrintMapping();
        //                            i = 0;
        //                            break;
        //                        }
        //                        else if (resultTest2 == -1 && resultTest1 >= 0)
        //                        {
        //                            seg[i] = possibleSeg[i][0].ToString();
        //                            seg[c] = possibleSeg[i][1].ToString();
        //                            RemoveAllOf(possibleSeg[i]);
        //                            PrintMapping();
        //                            i = 0;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        if (possibleSeg[i].Length == 1)
        //        {
        //            seg[i] = possibleSeg[i];
        //            RemoveAllOf(possibleSeg[i]);
        //            i = 0;
        //        }
        //    }

        //    complete = true;
        //    for (int i = 0; i < 7; i++)
        //    {
        //        if (seg[i] == "")
        //        {
        //            complete = false;
        //            break;
        //        }
        //    }
        //    if (complete)
        //    {
        //        Console.WriteLine("Mapping complete");
        //    }
        //}

        //public bool CheckEasyDelete(int i, int c)
        //{
        //    return ((i == 1 || i == 4 || i == 2 || i == 5) && (c == 1 || c == 4 || c == 2 || c == 5));
        //}




        //public int Decode(string s)
        //{
        //    return Decode(s, seg);
        //}
       

        //public int TestDecode(string s, string[] mask)
        //{
        //    if (s == "")
        //        return 0;

        //    bool[] active = new bool[7];
        //    int activeCount = 0;
        //    for (int i = 0; i < mask.Length; i++)
        //    {
        //        active[i] = s.Contains(mask[i]);
        //        if (active[i])
        //            activeCount++;
        //    }

        //    if (activeCount == 6 && active[0] && active[1] && active[2] && active[4] && active[5] && active[6])
        //        return 0;
        //    else if (activeCount == 2 && active[2] && active[5])
        //        return 1;
        //    else if (activeCount == 5 && active[0] && active[2] && active[3] && active[4] && active[6])
        //        return 2;
        //    else if (activeCount == 5 && active[0] && active[2] && active[3] && active[5] && active[6])
        //        return 3;
        //    else if (activeCount == 4 && active[1] && active[2] && active[3] && active[5])
        //        return 4;
        //    else if (activeCount == 5 && active[0] && active[1] && active[3] && active[5] && active[6])
        //        return 5;
        //    else if (activeCount == 6 && active[0] && active[1] && active[3] && active[4] && active[5] && active[6])
        //        return 6;
        //    else if (activeCount == 3 && active[0] && active[2] && active[5])
        //        return 7;
        //    else if (activeCount == 7)
        //        return 8;
        //    else if (activeCount == 6 && active[0] && active[1] && active[2] && active[3] && active[5] && active[6])
        //        return 9;
        //    else
        //        return -1;
        //}
    }
}
