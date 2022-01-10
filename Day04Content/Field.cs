using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day04Content
{
    class Field
    {
        public FieldEntry[,] field; //X,Y
        public int horizontalLength;
        public int verticalLength;

        public Field(string[] fieldString)
        {
            horizontalLength = fieldString[0].Split(' ').Length;

            //string[] test = fieldString[0].Split(' ');
            //Console.WriteLine("Test for: " + fieldString[0]);
            //for (int i = 0; i < test.Length; i++)
            //{
            //    Console.WriteLine(i+" "+test[i]);
            //}

            verticalLength = fieldString.Length;
            Console.WriteLine("Creating new Field with " + horizontalLength + "x" + verticalLength);
            field = new FieldEntry[horizontalLength, verticalLength];
            for (int y = 0; y < verticalLength; y++)
            {
                string[] split = fieldString[y].Split(' ');
                for (int x = 0; x < horizontalLength; x++)
                {
                    //Console.WriteLine("New Field: ["+x+"|"+y+"]" + split[x]);
                    field[x, y] = new FieldEntry(split[x]);
                }
            }

            PrintField();
        }

        public int MarkNumber(int number)
        {
            bool horizontalBingo = true;
            for (int y = 0; y < verticalLength; y++)
            {
                horizontalBingo = true;
                for (int x = 0; x < horizontalLength; x++)
                {
                    if (!field[x, y].Mark(number))
                    {
                        horizontalBingo = false;
                    }
                }
                if (horizontalBingo)
                {
                    return GetUnmarkedNumberSum();
                }
            }

            bool verticalBingo = true;
            for (int x = 0; x < horizontalLength; x++)
            {
                verticalBingo = true;
                for (int y = 0; y < verticalLength; y++)
                {
                    if (!field[x, y].marked)
                    {
                        verticalBingo = false;
                        break;
                    }
                }
                if (verticalBingo)
                {
                    return GetUnmarkedNumberSum();
                }
            }

            return -1;
        }

        public int GetUnmarkedNumberSum()
        {
            int sum = 0;
            for (int x = 0; x < horizontalLength; x++)
            {
                for (int y = 0; y < verticalLength; y++)
                {
                    if (!field[x, y].marked)
                    {
                        sum += field[x, y].number;
                    }
                }
            }
            return sum;
        }

        public void PrintField()
        {
            for (int y = 0; y < verticalLength; y++)
            {
                string line = "";
                for (int x = 0; x < horizontalLength; x++)
                {
                    line += field[x, y].GetEntryString();
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("");
        }
    }

    class FieldEntry
    {
        public int number;
        public bool marked;

        public FieldEntry(int number)
        {
            this.number = number;
            this.marked = false;
        }

        public FieldEntry(string number)
        {
            this.number = int.Parse(number);
            this.marked = false;
        }

        public bool Mark()
        {
            this.marked = true;
            return this.marked;
        }

        public bool Mark(int number)
        {
            if (this.number == number)
                this.marked = true;

            return this.marked;
        }

        public string GetEntryString()
        {
            if (marked)
            {
                return "|" + number.ToString("00") + "|";
            }
            else
            {
                return " " + number.ToString("00") + " ";
            }
        }
    }
}
