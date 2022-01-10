using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Day20Content
{
    class Pixel
    {
        public bool set;

        public Pixel(string value)
        {
            this.set = value=="#";
        }
        public Pixel(bool value)
        {
            this.set = value;
        }
        public Pixel()
        {
            this.set = false;
        }

        public override string ToString()
        {
            return set ? "#" : ".";
        }

        public string GetBinaryString()
        {
            return set ? "1" : "0";
        }
        public int GetBinaryValue()
        {
            return set ? 1 : 0;
        }
    }

     class ImageSize
    {
        public int startX;
        public int startY;
        public int width;
        public int height;

        public ImageSize(int startX, int startY, int width, int height)
        {
            this.startX = startX;
            this.startY = startY;
            this.width = width;
            this.height = height;
        }

        public ImageSize(int width, int height)
        {
            this.startX = 0;
            this.startY = 0;
            this.width = width;
            this.height = height;
        }

        public void Normalize()
        {
            width += -(startX);
            startX = 0;
            height += -(startY);
            startY = 0;
        }

        public int GetRangeX()
        {
            return width - startX;
        }

        public int GetRangeY()
        {
            return height - startY;
        }

        public bool Contains(int x, int y)
        {
            return (x >= startX && x < width && y >= startY && y < height);
        }

        public override string ToString()
        {
            return "["+width + " x " + height+"]";
        }
    }

    class Image
    {
        public Pixel[,] image;

        public ImageSize size;

        public Image(string[] input)
        {
            size = new ImageSize(input[0].Length, input.Length);
            image = new Pixel[size.width, size.height];
            for (int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    image[x, y] = new Pixel(input[y][x].ToString());
                }
            }
            PrintField();
        }

        public Image(ImageSize size)
        {
            this.size = new ImageSize(size.width, size.height);
            image = new Pixel[size.width, size.height];
            for (int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    image[x, y] = new Pixel();
                }
            }
        }

        public void CenterImage(int reqMargin, bool setValue)
        {
            int top = int.MaxValue;
            int bot = 0;
            int left = int.MaxValue;
            int right = 0;

            for (int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    Pixel p = GetPixel(x, y);
                    if (p != null && p.set)
                    {
                        top = Math.Min(y, top);
                        bot = y;
                        left = Math.Min(x, left);
                        right = Math.Max(x, right);
                    }
                }
            }
            Utilities.Log("CenterImage Top: " + top);
            Utilities.Log("CenterImage Bot: " + bot);
            Utilities.Log("CenterImage Left: " + left);
            Utilities.Log("CenterImage right: " + right);
            top = Math.Max(reqMargin - top, 0);
            bot = Math.Max(reqMargin - ((size.height - 1) - bot), 0);
            left = Math.Max(reqMargin - left, 0);
            right = Math.Max(reqMargin - ((size.width - 1) - right), 0);

            ExpandImage(left, right, top, bot, setValue);
        }

        public void ExpandImage(int marginLeft, int marginRight, int marginTop, int marginBot, bool setValue)
        {
            ImageSize originalSize = new ImageSize(size.width, size.height);
            size = new ImageSize(originalSize.width + marginLeft + marginRight, originalSize.height + marginTop + marginBot);
            Pixel[,] newImage = new Pixel[size.width, size.height];

            for (int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    int oX = x - marginLeft;
                    int oY = y - marginTop;
                    if (originalSize.Contains(oX, oY))
                    {
                        newImage[x, y] = new Pixel(image[oX,oY].set);
                    }
                    else
                    {
                        newImage[x, y] = new Pixel(setValue);
                    }
                }
            }
            image = newImage;
            Utilities.Log("Expanding Image by Left: " + marginLeft + " Right: " + marginRight + " Top: " + marginTop + " Bottom: " + marginBot);
            PrintField();
        }

        public void PrintField()
        {
            string s = "";
            for (int y = size.startY; y < size.height; y++)
            {
                s = "";
                for (int x = size.startX; x < size.width; x++)
                {
                    s += image[x, y].ToString();
                }
                Console.WriteLine(s);
            }
            Console.WriteLine("ImageSize: " + size.ToString()); ;
            Console.WriteLine("Pixelcount in Image: " + GetPixelCount());
            Console.WriteLine("");
        }

        public int GetPixelCount()
        {
            int result = 0;
            for (int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    if (image[x, y].set)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        public Pixel GetPixel(int x, int y)
        {
            if (size.Contains(x, y))
            {
                return image[x, y];
            }
            return null;
        }

        public void SetPixel(int x, int y, bool value)
        {
            if (size.Contains(x, y))
            {
                image[x, y].set = value;
            }
        }

        public string Get9PixelFieldString(int x, int y, bool infImageValue)
        {
            string s = "";
            for(int fy = y - 1; fy <= y + 1; fy++)
            {
                for (int fx = x - 1; fx <= x + 1; fx++)
                {
                    if(size.Contains(fx,fy))
                    {
                        s += image[fx, fy].GetBinaryString();
                    }
                    else
                    {
                        s += Convert.ToInt32(infImageValue).ToString();
                    }
                }
            }
            return s;
        }
      
    }
}
