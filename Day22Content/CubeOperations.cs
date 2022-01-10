using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AdventOfCode.Day22Content
{
    class CubeOperations
    {
        public static List<Cube> SplitterCubes(Cube cube1, Cube cube2)
        {
            Cube intersCube = CubeOperations.CreateIntersectionCube(cube1,cube2);
            if (intersCube == null)
            {
                return new List<Cube>{cube1, cube2};
            }

            List<Cube> restCube1 = RemovePiece(cube1, intersCube);
            List<Cube> restCube2 = RemovePiece(cube2, intersCube);

            List<Cube> result = new List<Cube>();
            result.Add(intersCube);
            result.AddRange(restCube1);
            result.AddRange(restCube2);

            long inputVolume = cube1.GetVolume() + cube2.GetVolume();
            long resultVolume = 0;
            foreach(Cube c in result)
            {
                resultVolume += c.GetVolume();
            }
            if(inputVolume - intersCube.GetVolume() != resultVolume)
            {
                throw new Exception("Error in CubeSplitting");
            }

            return result;
        }

        public static List<Cube> SplitterCubesKeepOnly1(Cube cube1, Cube cube2)
        {
            Utilities.Log("Splittering Cube and only keeping Cube1");
            Cube intersCube = CubeOperations.CreateIntersectionCube(cube1,cube2);
            if (intersCube == null)
            {
                Utilities.Log("Intersection Cube == null. Keeping Cube1");
                return new List<Cube> { cube1 };
            }

            List<Cube> restCube1 = RemovePiece(cube1, intersCube);

            List<Cube> result = new List<Cube>();
            result.AddRange(restCube1);

            long inputVolume = cube1.GetVolume();
            long resultVolume = 0;
            Utilities.Log("Result: ");
            foreach (Cube c in result)
            {
                resultVolume += c.GetVolume();
                Utilities.Log(c.ToString());
            }
            if (inputVolume != resultVolume+intersCube.GetVolume())
            {
                throw new Exception("Error in CubeSplittingKeepOnly1");
            }

            return result;
        }

        public static List<Cube> RemoveIntersection(Cube cube, Cube removal)
        {
            Cube intersCube = CubeOperations.CreateIntersectionCube(cube, removal);
            if (intersCube == null || intersCube.GetVolume()==0)
            {
                return new List<Cube> { cube };
            }

            List<Cube> restCube1 = RemovePiece(cube, intersCube);

            List<Cube> result = new List<Cube>();
            result.AddRange(restCube1);

            long inputVolume = cube.GetVolume();
            long resultVolume = 0;
            foreach (Cube c in result)
            {
                resultVolume += c.GetVolume();
            }
            if (inputVolume - intersCube.GetVolume() != resultVolume)
            {
                throw new Exception("Error in CubeSplittingKeepOnly1");
            }

            return result;
        }

        public static List<Cube> SplitCube(Cube cube, Face face)
        {
            Utilities.Log("Splitting cube " + cube.ToString());
            Utilities.Log("With Face " + face.ToString());
            List<Cube> result = new List<Cube>();
            result.Add(new Cube(cube.isOn, cube.start, face.ProjectVectorToFaceLevel(cube.end)));
            result.Add(new Cube(cube.isOn, face.ProjectVectorToFaceLevel(cube.start), cube.end));
            Utilities.Log("Result1 " + result[0].ToString());
            Utilities.Log("Result2 " + result[1].ToString());
            
            return result;
        }

        public static List<Cube> RemovePiece(Cube cube, Cube removal)
        {
            if (!cube.Contains(removal) || removal== null || cube==null || cube.GetVolume()==0 || removal.GetVolume()==0)
                return null;

            Utilities.Log("");
            Utilities.Log("Removing Piece from cube");
            Utilities.Log("Cube: " + cube.ToString());
            Utilities.Log("Removal: " + removal.ToString());


            List<Face> notTouchingFaces = FindNotTouchingFaces(removal, cube);
            List<Cube> result = new List<Cube>();

            if (notTouchingFaces.Count== 0) //all faces touch
                return result;

            Face f = notTouchingFaces[0];
            
            List<Cube> splitters = SplitCube(cube, f);
            Cube rest = null;
            if(splitters[0].Contains(removal))
            {
                result.Add(splitters[1]);
                rest = splitters[0];
            }
            else if(splitters[1].Contains(removal))
            {
                result.Add(splitters[0]);
                rest = splitters[1];
            }
            else
            {
                throw new Exception("Something went wrong");
            }

            Utilities.Log("Rest is " + rest.ToString());
            
            List<Cube> restList = RemovePiece(rest, removal);
            if (restList != null && restList.Count > 0)
            {
                foreach (Cube c in restList)
                {
                    result.Add(c);
                }
            }
            return result;
        }

        private static List<Face> FindNotTouchingFaces(Cube cube1, Cube cube2)
        {
            List<Face> faces1 = Face.FromCube(cube1);
            List<Face> faces2 = Face.FromCube(cube2);
            List<Face> notTouchingFaces = new List<Face>();

            foreach(Face f1 in faces1)
            {
                if(faces2.Find(f => f.type==f1.type && f.offset==f1.offset)==null)
                {
                    notTouchingFaces.Add(f1);
                    Utilities.Log("Found not touching face " + f1.ToString());
                }
            }
            return notTouchingFaces;
        }

        public static Cube CreateIntersectionCube(Cube cube1, Cube cube2)
        {
            if (cube1.Contains(cube2))
            {
                return cube2;
            }
            else if (cube2.Contains(cube1))
            {
                return cube1;
            }
            else if (cube1.Intersects(cube2))
            {
                List<Face> intersectionFaces = CubeOperations.GetIntersectionFaces(cube1, cube2);
                if (intersectionFaces.Count >= 2)
                {
                    Cube c = new Cube(true, intersectionFaces);
                    Utilities.Log("IntersectionCube: " + c.ToString());
                    if (c.GetVolume() > 0)
                        return c;
                }
            }
            return null;
        }

        public static List<Face> GetIntersectionFaces(Cube cube1, Cube cube2)
        {
            List<Face> cube1Faces = Face.FromCube(cube1);
            List<Face> cube2Faces = Face.FromCube(cube2);
            List<Face> intersectingFaces1 = new List<Face>();
            List<Face> intersectingFaces2 = new List<Face>();

            foreach (Face f1 in cube1Faces)
            {
                foreach (Face f2 in cube2Faces)
                {
                    if (f1.Intersects(f2))
                    {
                        if (!intersectingFaces1.Contains(f1))
                        {
                            intersectingFaces1.Add(f1);
                        }
                        if (!intersectingFaces2.Contains(f2))
                            intersectingFaces2.Add(f2);
                    }
                    if (cube1.Contains(f2))
                    {
                        if (!intersectingFaces2.Contains(f2))
                            intersectingFaces2.Add(f2);
                    }
                    if (cube2.Contains(f1))
                    {
                        if (!intersectingFaces2.Contains(f1))
                            intersectingFaces2.Add(f1);
                    }
                }
            }

            Utilities.Log("Faces1: ");
            foreach (Face f1 in intersectingFaces1)
                Utilities.Log(f1.ToString());

            Utilities.Log("");
            Utilities.Log("Faces2: ");
            foreach (Face f1 in intersectingFaces2)
                Utilities.Log(f1.ToString());


            List<Face> result = new List<Face>();
            List<Face> useIntersectingFaces = intersectingFaces1.Count > intersectingFaces2.Count ?
                intersectingFaces1 : intersectingFaces2;
            List<Face> otherCubeFaces = intersectingFaces1.Count > intersectingFaces2.Count ?
                cube2Faces : cube1Faces;

            foreach (Face f in useIntersectingFaces)
            {
                Face intersection = f.GetIntersectionFace(otherCubeFaces.Find(cf => cf.type == f.type));
                Utilities.Log("Added intersection " + intersection.ToString());
                result.Add(intersection);
            }
            return result;
        }
    }
}
