using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AdventOfCode.Day22Content
{
    class Face2D
    {
        public Vector2 start;
        public Vector2 end;

        public Face2D(Face face)
        {
            if (face.isXFace)
            {
                start = new Vector2(face.start.Y, face.start.Z);
                end = new Vector2(face.end.Y, face.end.Z);
            }
            if (face.isYFace)
            {
                start = new Vector2(face.start.X, face.start.Z);
                end = new Vector2(face.end.X, face.end.Z);
            }
            if (face.isZFace)
            {
                start = new Vector2(face.start.X, face.start.Y);
                end = new Vector2(face.end.X, face.end.Y);
            }
        }
        

        public Face2D(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        public bool Intersects(Face2D face)
        {
            return (face.end.X > this.start.X && face.start.X < this.end.X
                 && face.end.Y > this.start.Y && face.start.Y < this.end.Y);
        }

        public Face2D GetIntersectionFace(Face2D face)
        {
            if (!Intersects(face))
                return null;
            Vector2 intersectionStart = new Vector2(Math.Max(start.X, face.start.X), Math.Max(start.Y, face.start.Y));
            Vector2 intersectionEnd = new Vector2(Math.Min(end.X, face.end.X), Math.Min(end.Y, face.end.Y));
            return new Face2D(intersectionStart, intersectionEnd);
        }

        public bool Contains(Vector2 pos)
        {
            return (pos.X >= start.X && pos.X <= end.X && pos.Y >= start.Y && pos.Y <= end.Y);
        }

        public override string ToString()
        {
            return "start" + start.ToString() + " end" + end.ToString();
        }


    }
   
}
