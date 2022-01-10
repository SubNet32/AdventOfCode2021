using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AdventOfCode.Day22Content
{
    class Cube
    {
        public Vector3 start;
        public Vector3 end;
        public bool isOn;

        public List<Cube> subCubes;

        public Cube(string s)
        {
            string[] val1 = s.Split(' ');
            isOn = val1[0] == "on";
            string[] pos = val1[1].Replace("x=", "").Replace("y=", "").Replace("z=", "").Split(',');
            string[] xPos = pos[0].Split("..");
            string[] yPos = pos[1].Split("..");
            string[] zPos = pos[2].Split("..");
            start = new Vector3(Math.Min(int.Parse(xPos[0]), int.Parse(xPos[1])),
               Math.Min(int.Parse(yPos[0]), int.Parse(yPos[1])),
               Math.Min(int.Parse(zPos[0]), int.Parse(zPos[1])));
            end = new Vector3(1+Math.Max(int.Parse(xPos[0]), int.Parse(xPos[1])),
               1+Math.Max(int.Parse(yPos[0]), int.Parse(yPos[1])),
               1+Math.Max(int.Parse(zPos[0]), int.Parse(zPos[1])));

            Utilities.Log("New Cube: " + ToString());
            subCubes = new List<Cube>();
        }

        public Cube(bool value, Vector3 start, Vector3 end)
        {
            this.isOn = value;
            this.start = start;
            this.end = end;
            subCubes = new List<Cube>();
        }

        public Cube(bool value, List<Face> faces)
        {
            this.isOn = value;

            Vector3 min = faces[0].start;
            Vector3 max = faces[0].end;

            foreach(Face f in faces)
            {
                min = Vector3.Min(min, Vector3.Min(f.start, f.end));
                max = Vector3.Max(max, Vector3.Max(f.start, f.end));
            }

            start = min;
            end = max;

            if (start.X == end.X || start.Y == end.Y || start.Z == end.Z)
                throw new Exception("This is not a cube but a plane");

            subCubes = new List<Cube>();
        }

        public override string ToString()
        {
            return "[" + start.X.ToString("0") + ".." + end.X.ToString("0") + " | " +
                start.Y.ToString("0") + ".." + end.Y.ToString("0") + " | " +
                start.Z.ToString("0") + ".." + end.Z.ToString("0") + "] isOn: "+isOn + 
                "  Volume: "+GetVolume();
        }

        public long GetVolume()
        {
            long distX = Convert.ToInt64(end.X - start.X);
            long distY = Convert.ToInt64(end.Y - start.Y);
            long distZ = Convert.ToInt64(end.Z - start.Z);
            return distX * distY * distZ;
        }

        public bool Intersects(Cube cube)
        {
            return ((end.X > cube.start.X) &&
                (start.X < cube.end.X) &&
                (end.Y > cube.start.Y) &&
                (start.Y < cube.end.Y) &&
                (end.Z > cube.start.Z) &&
                (start.Z < cube.end.Z));
        }

        public bool Contains(Cube cube)
        {
            return ((cube.start.X >= start.X && cube.end.X <= end.X) &&
                (cube.start.Y >= start.Y && cube.end.Y <= end.Y) &&
                (cube.start.Z >= start.Z && cube.end.Z <= end.Z));
        }

        public bool Contains(Face face)
        {
            return ((face.start.X >= start.X && face.end.X <= end.X) &&
              (face.start.Y >= start.Y && face.end.Y <= end.Y) &&
              (face.start.Z >= start.Z && face.end.Z <= end.Z));
        }

     

        public bool Contains(Vector3 point)
        {
            return ((point.X >= start.X) &&
                (point.Y >= start.Y) &&
                (point.Z >= start.Z) &&
                (point.X <= end.X) &&
                (point.Y <= end.Y) &&
                (point.Z <= end.Z));
        }

        public void Add(Vector3 point)
        {
            start = Vector3.Min(start, point);
            end = Vector3.Max(end, point);
        }

    }
}
