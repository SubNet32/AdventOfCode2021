using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AdventOfCode.Day22Content
{
    class Solver
    {
        public List<Cube> inputCubes;
        List<Cube> cubes;

        public Solver(string[] input)
        {
            inputCubes = new List<Cube>();
           
            foreach(string s in input)
            {
                Cube c = new Cube(s);
                inputCubes.Add(c);

            }
        }

        public long Solve()
        {
            cubes = new List<Cube>();

           
            foreach(Cube cube in inputCubes)
            {
                Utilities.Log("");
                Utilities.Log("----------------------");
                if (cube.isOn)
                {
                    Utilities.Log("Adding InputCube: " + cube.ToString());
                    AddCube(cube);
                }
                else
                {
                    Utilities.Log("Removing Content inside of: " + cube.ToString());
                    RemoveCubeContent(cube);
                }
                //CheckCubes();
            }


            Utilities.Log("");
            Utilities.Log("");
            Utilities.Log("--------------------------------------------------------------");
            Utilities.Log("Done. Printing all Cubes");

            long volume = 0;
            foreach (Cube cube in cubes)
            {
                volume += cube.GetVolume();
                Utilities.Log(cube.ToString());
            }

            Utilities.Log("");
            Utilities.Log("");
            Utilities.Log("Amount of cubes: "+cubes.Count);

            return volume;
        }

        public void AddCube(Cube cube)
        {
            if (cubes.Count == 0)
            {
                cubes.Add(cube);
                return;
            }

            List<Cube> newCubes = new List<Cube>();
            List<Cube> removeCubes = new List<Cube>();
            foreach(Cube c in cubes)
            {
                if (c.Contains(cube))
                {
                    Utilities.Log("");
                    Utilities.Log("Another cube completely contains this cube. Dont Add");
                    return;
                }
                if (cube.Contains(c))
                {
                    Utilities.Log("");
                    Utilities.Log("Cube contains other cube " + c.ToString());
                    removeCubes.Add(c);
                }
                else if (c.Intersects(cube))
                {
                    Utilities.Log("");
                    Utilities.Log("Intersection detected with " + c.ToString());
                    newCubes.AddRange(CubeOperations.SplitterCubesKeepOnly1(c, cube));
                    removeCubes.Add(c);
                }
                
                
            }
            foreach(Cube c in removeCubes)
            {
                cubes.Remove(c);
            }
            cubes.AddRange(newCubes);
            cubes.Add(cube);
        }

        public void RemoveCubeContent(Cube container)
        {
            if (cubes.Count == 0)
                return;

            List<Cube> newCubes = new List<Cube>();
            List<Cube> removeCubes = new List<Cube>();
            foreach (Cube c in cubes)
            {
                if(container.Contains(c))
                {
                    removeCubes.Add(c);
                }
                if(c.Intersects(container))
                {
                    newCubes.AddRange(CubeOperations.RemoveIntersection(c, container));
                    removeCubes.Add(c);
                }
            }
            foreach (Cube c in removeCubes)
            {
                cubes.Remove(c);
            }
            cubes.AddRange(newCubes);
        }

        public void CheckCubes()
        {
            foreach (Cube a in cubes)
            {
                foreach (Cube b in cubes)
                {
                    if (a != b)
                    {
                        if (a.Contains(b))
                        {
                            throw new Exception("Found b in a");
                        }
                        if (b.Contains(a))
                        {
                            throw new Exception("Found a in b");
                        }
                        if (a.Intersects(b))
                        {
                            throw new Exception("Intersection b with a");
                        }
                    }
                }
            }
        }
    }
}
