using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day20Content
{
    class Solver
    {
        public Image image;
        public int cycles = 50;
        public int reqMargin = 4;
        public bool infImageValue = false;

        public Solver(string[] input)
        {
            string enh = "";
            bool foundSpace = false;
            List<string> imageStringList = new List<string>();
            foreach(string s in input)
            {
                if(!foundSpace)
                {
                    if(string.IsNullOrEmpty(s))
                    {
                        foundSpace = true;
                    }
                    else
                    {
                        enh += s;
                    }
                }
                else
                {
                    imageStringList.Add(s);
                }
            }
            image = new Image(imageStringList.ToArray());
            EnhancementMask mask = new EnhancementMask(enh);

            for (int c = 0; c < cycles; c++)
            {
                image.CenterImage(reqMargin, infImageValue);
                image = EnhanceImage(image, mask);
                image.PrintField();
                if(mask.GetEnhancement(0))
                    infImageValue = !infImageValue;
            }

        }

        public Image EnhanceImage(Image image, EnhancementMask mask)
        {
            Utilities.Log("Enhancing Image with mask");
            Image newImage = new Image(image.size);
            for (int y = 0; y < newImage.size.height; y++)
            {
                for (int x = 0; x < newImage.size.width; x++)
                {
                    string s = image.Get9PixelFieldString(x, y, infImageValue);
                    newImage.SetPixel(x, y, mask.GetEnhancement(s));
                }
            }
            return newImage;
        }

   
    }
}
