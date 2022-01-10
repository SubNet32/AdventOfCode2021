using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AdventOfCode.Day22Content
{
 

    class Face
    {
        public Vector3 start;
        public Vector3 end;
        public int offset;
        public int type = 0;
        public bool isXFace;
        public bool isYFace;
        public bool isZFace;

        public Face(Vector3 start, Vector3 end)
        {
            this.start = start;
            this.end = end;
            DetermineFace();
        }

        private void DetermineFace()
        {
            isXFace = (start.X == end.X);
            isYFace = (start.Y == end.Y);
            isZFace = (start.Z == end.Z);
            if(isXFace)
            {
                type = 1;
                offset = (int)start.X;
            }
            else if (isYFace)
            {
                type = 2;
                offset = (int)start.Y;
            }
            else if (isZFace)
            {
                type = 3;
                offset = (int)start.Z;
            }

        }

        private Face2D ToFace2D()
        {
            return new Face2D(this);
        }

        public Vector3 GetFaceTypeVector()
        {
            return new Vector3(Convert.ToInt32(isXFace), Convert.ToInt32(isYFace), Convert.ToInt32(isZFace));
        }

        public Vector3 ProjectVectorToFaceLevel(Vector3 vector)
        {
            if(type==1)
            {
                vector.X = offset;
                return vector;
            }
            if (type == 2)
            {
                vector.Y = offset;
                return vector;
            }
            if (type == 3)
            {
                vector.Z = offset;
                return vector;
            }
            throw new Exception("Wrong type");
        }

        public bool Intersects(Face face)
        {
            if (type==face.type)
            {
                return (offset==face.offset && ToFace2D().Intersects(face.ToFace2D()));
            }
            else
            {
                if(type==1) //X
                {
                    return (face.end.Y > start.Y && face.start.Y < end.Y &&
                       face.end.Z > start.Z && face.start.Z < end.Z &&
                       face.start.X < offset && face.end.X > offset);
                }
                if (type == 2) //Y
                {
                    return (face.end.X > start.X && face.start.X < end.X &&
                       face.end.Z > start.Z && face.start.Z < end.Z &&
                       face.start.Y < offset && face.end.Y > offset);
                }
                if (type == 3) //Z
                {
                    return (face.end.X > start.X && face.start.X < end.X &&
                       face.end.Y > start.Y && face.start.Y < end.Y &&
                       face.start.Z < offset && face.end.Z > offset);
                }
                throw new Exception("Should not happen");
            }
        }

        public Face GetIntersectionFace(Face face)
        {
            if (type == face.type)
            {
                Face2D f1 = ToFace2D();
                Face2D f2 = face.ToFace2D();

                Face2D intersection2D = f1.GetIntersectionFace(f2);
                return new Face(ConvertToFaceVector(intersection2D.start), ConvertToFaceVector(intersection2D.end));
            }
            else
            {
                return null;
            }
        }

        public List<Face> BysectFace(Face bysectorFace)
        {
            if (type == bysectorFace.type)
                return null;

            List<Face> result = new List<Face>();
            if (bysectorFace.type == 1)
            {
                Face a = new Face(new Vector3(start.X, start.Y, start.Z), new Vector3(bysectorFace.offset, end.Y, end.Z));
                Face b = new Face(new Vector3(bysectorFace.offset, start.Y, start.Z), new Vector3(end.X, end.Y, end.Z));
                result.Add(a);
                result.Add(b);
            }
            if (bysectorFace.type == 2)
            {
                Face a = new Face(new Vector3(start.X, start.Y, start.Z), new Vector3(end.X, bysectorFace.offset, end.Z));
                Face b = new Face(new Vector3(start.X, bysectorFace.offset, start.Z), new Vector3(end.X, end.Y, end.Z));
                result.Add(a);
                result.Add(b);
            }
            if (bysectorFace.type == 3)
            {
                Face a = new Face(new Vector3(start.X, start.Y, start.Z), new Vector3(end.X, end.Y, bysectorFace.offset));
                Face b = new Face(new Vector3(start.X, start.Y, bysectorFace.offset), new Vector3(end.X, end.Y, end.Z));
                result.Add(a);
                result.Add(b);
            }
            return result;
        }

        public Vector3 ConvertToFaceVector(Vector2 vector)
        {
            if(type==1)//x
            {
                return new Vector3(offset, vector.X, vector.Y);
            }
            else if (type == 2)//y
            {
                return new Vector3(vector.X, offset, vector.Y);
            }
            else if (type == 3)//z
            {
                return new Vector3(vector.X, vector.Y, offset);
            }
            throw new Exception("Wrong Type");
        }

        public static List<Face> FromCube(Cube cube)
        {
            List<Face> faces = new List<Face>();
            faces.Add(new Face(
                new Vector3(cube.start.X, cube.start.Y, cube.start.Z),
                new Vector3(cube.start.X, cube.end.Y, cube.end.Z)));
            faces.Add(new Face(
                new Vector3(cube.end.X, cube.start.Y, cube.start.Z),
                new Vector3(cube.end.X, cube.end.Y, cube.end.Z)));
            faces.Add(new Face(
                new Vector3(cube.start.X, cube.start.Y, cube.start.Z),
                new Vector3(cube.end.X, cube.start.Y, cube.end.Z)));
            faces.Add(new Face(
                new Vector3(cube.start.X, cube.end.Y, cube.start.Z),
                new Vector3(cube.end.X, cube.end.Y, cube.end.Z)));
            faces.Add(new Face(
                new Vector3(cube.start.X, cube.start.Y, cube.start.Z),
                new Vector3(cube.end.X, cube.end.Y, cube.start.Z)));
            faces.Add(new Face(
                new Vector3(cube.start.X, cube.start.Y, cube.end.Z),
                new Vector3(cube.end.X, cube.end.Y, cube.end.Z)));

            return faces;
        }

        public override string ToString()
        {
            string typeS = "";
            typeS += isXFace ? "X-Face" : "";
            typeS += isYFace ? "Y-Face" : "";
            typeS += isZFace ? "Z-Face" : "";


            return "[ " + typeS + " - Off: " + offset + " - start" + start.ToString() + " end" + end.ToString();
        }


    }
}
