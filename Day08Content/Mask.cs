using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day08Content
{
    class Mask
    {
        public string[] mask;
        public static int maskSize = 7;

        public Mask(string defaultString)
        {
            mask = new string[maskSize];
            for (int i = 0; i < maskSize; i++)
            {
                mask[i] = defaultString;
            }
        }

        public bool IsComplete()
        {
            for (int i = 0; i < maskSize; i++)
            {
                if (mask[i].Length != 1)
                {
                    return false;
                }
            }
            return true;
        }

        public void RemoveAllOf(string del, int[] except)
        {
            string newS = "";
            bool delete;
            for (int i = 0; i < maskSize; i++)
            {
                newS = "";
                delete = true;
                if (except!=null)
                {
                    for(int e = 0; e < except.Length; e++)
                    {
                        if(except[e]==i)
                        {
                            delete = false;
                            break;
                        }
                    }
                }
                if (delete)
                {
                    for (int s = 0; s < mask[i].Length; s++)
                    {
                        if (!del.Contains(mask[i][s]))
                        {
                            newS += mask[i][s];
                        }
                    }
                    mask[i] = newS;
                }
            }
        }

        public string RemoveAllBut(string source, string but)
        {
            string newS = "";
            for (int i = 0; i < source.Length; i++)
            {
                if (but.Contains(source[i]))
                {
                    newS += source[i];
                }
            }
            return newS;
        }

        public void PrintMask()
        {
            for (int i = 0; i < maskSize; i++)
            {
                if (mask[i] == "")
                {
                    Console.WriteLine(i + ": -");
                }
                else
                {
                    Console.WriteLine(i + ": " + mask[i]);
                }
            }
        }

        public bool ContainsMappingFor(int[] mapping, int index)
        {
            if (mapping == null || mapping.Length < 1)
                return false;
            for (int m = 0; m < mapping.Length; m++)
            {
                if (mapping[m] == index)
                {
                    return true;
                }
            }
            return false;
        }

        public void MapStringToMask(string s, int[] mapping)
        {
            for(int i = 0; i < maskSize; i++)
            {
                if(ContainsMappingFor(mapping, i))
                {
                    mask[i] = RemoveAllBut(mask[i], s);
                }
                else
                {
                    RemoveAllOf(s, mapping);
                }
                CheckSingleCharRemaining(i);
            }
        }

        public void MapStringToMaskExclusive(string s, int[] mapping)
        {
            for(int i = 0; i < maskSize; i++)
            {
                if(ContainsMappingFor(mapping, i))
                {
                    mask[i] = RemoveAllBut(mask[i], s);
                }
                CheckSingleCharRemaining(i);
            }
        }

        public void CheckSingleCharRemaining(int index)
        {
            if(mask[index].Length==1)
            {
                RemoveAllOf(mask[index], new int[] { index });
            }
        }

        public int Decode(string s)
        {
            if (s == "")
                throw new Exception("Invalid input for decoding");
            if (!IsComplete())
                throw new Exception("Decoding with incomplete mask tried");

            bool[] active = new bool[7];
            int activeCount = 0;
            for (int i = 0; i < mask.Length; i++)
            {
                active[i] = s.Contains(mask[i]);
                if (active[i])
                    activeCount++;
            }

            if (activeCount == 6 && active[0] && active[1] && active[2] && active[4] && active[5] && active[6])
                return 0;
            else if (activeCount == 2 && active[2] && active[5])
                return 1;
            else if (activeCount == 5 && active[0] && active[2] && active[3] && active[4] && active[6])
                return 2;
            else if (activeCount == 5 && active[0] && active[2] && active[3] && active[5] && active[6])
                return 3;
            else if (activeCount == 4 && active[1] && active[2] && active[3] && active[5])
                return 4;
            else if (activeCount == 5 && active[0] && active[1] && active[3] && active[5] && active[6])
                return 5;
            else if (activeCount == 6 && active[0] && active[1] && active[3] && active[4] && active[5] && active[6])
                return 6;
            else if (activeCount == 3 && active[0] && active[2] && active[5])
                return 7;
            else if (activeCount == 7)
                return 8;
            else if (activeCount == 6 && active[0] && active[1] && active[2] && active[3] && active[5] && active[6])
                return 9;
            else
                throw new Exception("Decoding error");
        }
    }
}
