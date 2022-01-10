using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day21Content
{
    public sealed class Field
    {

        public static Field instance = null;
        public static Field GetInstance()
        {
            if (instance == null)
            {
                instance = new Field();
            }
            return instance;
        }

        public int Move(int position, int amount)
        {
            int newPosition = position + amount;
            while(newPosition>10)
            {
                newPosition -= 10;
            }
            return newPosition;
        }
    }
}
