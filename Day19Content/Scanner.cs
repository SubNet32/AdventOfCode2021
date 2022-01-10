using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AdventOfCode.Day19Content
{

    class Scanner
    {
        public Orientation orientation;
        public List<Beacon> beacons;
        public bool foundOrientation;
        public int index = 0;

        public Scanner(int index)
        {
            orientation = new Orientation();
            beacons = new List<Beacon>();
            this.index = index;
        }

        public void AddBeacon(string s)
        {
            Beacon b = new Beacon(s);
            if(Math.Abs(b.position.X) <= 1000 && Math.Abs(b.position.Y) <= 1000 && Math.Abs(b.position.Z) <= 1000)
                beacons.Add(b);
        }

        public void SetOrientation(Orientation o)
        {
            this.orientation = new Orientation(o);
            TransformBeacons(this.orientation);
            //this.orientation.position = Orientation.RotateVectorInverse(this.orientation.position, reference.rotation);
            //this.orientation.position += reference.position;

            //this.orientation.referenceRotation = reference.rotation*-1;
            //this.orientation.rotation = reference.rotation;
            Utilities.Log("Setting orientation for scanner"+index+": "  + orientation.ToString());
            
            foundOrientation = true;
        }

        public void FindOrientation(Scanner scannerB)
        {
            if (!scannerB.foundOrientation)
            {
                Utilities.Log("");
                Utilities.Log("-------------------------------");
                Utilities.Log("Scanner" + index+" checking with Scanner"+scannerB.index);
                foreach (Orientation o in Orientation.GetPossibleOrientations())
                {
                    Orientation foundO = FindOrientationWithOrient(scannerB, o);
                    if (foundO != null)
                    {
                        scannerB.SetOrientation(foundO);
                        return;
                    }
                }
            }
        }

        public Orientation FindOrientationWithOrient(Scanner scannerB, Orientation orient)
        {
            List<BeaconCompare> compares = new List<BeaconCompare>();
            foreach (Beacon a in beacons)
            {
                foreach (Beacon b in scannerB.beacons)
                {
                    compares.Add(a.CompareTo(b, orient));
                }
            }
            //Utilities.Log("Checking for Similarites with orientation: "+ orient.ToString());
            BeaconCompare result = CheckSimilarities(compares);

            Orientation foundOrient = FindPositionOfB(compares, result);
            if (foundOrient != null)
            {
               return foundOrient;
            }
            //Utilities.Log("found none");
            return null;
        }

        public BeaconCompare CheckSimilarities(List<BeaconCompare> list)
        {
            //Utilities.Log("Checking BeaconCompare List with count: " + list.Count);
            foreach (BeaconCompare bc in list)
            {
                int cx = list.FindAll(vec => vec.dist.X == bc.dist.X).Count;
                int cy = list.FindAll(vec => vec.dist.Y == bc.dist.Y).Count;
                int cz = list.FindAll(vec => vec.dist.Z == bc.dist.Z).Count;
                //Utilities.Log("Checking BC " + bc.ToString());
                if (cx >= 12)
                {
                    Utilities.Log("Found x " + bc.dist.X + " " + cx + " times");
                    bc.position.X = bc.dist.X;
                    return bc;
                }
                if (cy >= 12)
                {
                    Utilities.Log("Found y " + bc.dist.Y + " " + cy + " times");
                    bc.position.Y = bc.dist.Y;
                    return bc;
                }
                if (cz >= 12)
                {
                    Utilities.Log("Found z " + bc.dist.Z + " " + cz + " times");
                    bc.position.Z = bc.dist.Z;
                    return bc;
                }
            }
            return null;
        }

        public Orientation FindPositionOfB(List<BeaconCompare> compareList, BeaconCompare reference)
        {
            if (compareList == null || compareList.Count < 2 || reference==null)
                return null;
            Vector3 refAxis = Vector3.Normalize(reference.position) * Vector3.Normalize(reference.position);
            compareList = compareList.FindAll(c => c.dist * refAxis == reference.dist * refAxis);
            Vector3 b1 = compareList[0].b;
            Vector3 b2 = compareList[1].b;
            Vector3 b3 = compareList[2].b;
            Vector3 b4 = compareList[3].b;
            Vector3 res1 = Vector3.Zero;
            Vector3 res2 = Vector3.Zero;
            Vector3 res3 = Vector3.Zero;
            Vector3 res4 = Vector3.Zero;
            Orientation orient = new Orientation();

            for(int x = 0; x < 360; x+=90)
            {
                for (int y = 0; y < 360; y += 90)
                {
                    for (int z = 0; z < 360; z += 90)
                    {
                        b1 = orient.RotateVector(compareList[0].b);
                        b2 = orient.RotateVector(compareList[1].b);
                        b3 = orient.RotateVector(compareList[2].b);
                        b4 = orient.RotateVector(compareList[3].b);
                        res1 = compareList[0].a - b1;
                        res2 = compareList[1].a - b2;
                        res3 = compareList[2].a - b3;
                        res4 = compareList[3].a - b4;
                        if (res1 == res2 && res1==res3 && res3==res4)
                        {
                            Utilities.Log("Found Positon and Rotation");
                            orient.position = res1;
                            Utilities.Log("Found orientation: " + orient.ToString());

                            return orient;
                        }
                        orient.AddRotation(new Vector3(0, 0, 1), 90);
                    }
                    orient.AddRotation(new Vector3(0, 1, 0), 90);
                }
                orient.AddRotation(new Vector3(1, 0, 0), 90);
            }

            return null;
        }

        public Vector3 RotateVector(Vector3 vector, Quaternion rotation)
        {
            Vector3 rotationVector = Vector3.Transform(vector, rotation);
            rotationVector.X = Convert.ToSingle(Math.Round(Convert.ToDouble(rotationVector.X)));
            rotationVector.Y = Convert.ToSingle(Math.Round(Convert.ToDouble(rotationVector.Y)));
            rotationVector.Z = Convert.ToSingle(Math.Round(Convert.ToDouble(rotationVector.Z)));
            return rotationVector;
        }

        public void PrintBeacons()
        {
            Utilities.Log("--------------------");
            Utilities.Log("Printing Beacons");
            foreach(Beacon b in beacons)
            {
                Utilities.Log(b.GetPositionInRelationTo(this.orientation).ToString());
            }
            Utilities.Log("");
        }

        public List<Beacon> GetBeacons()
        {
            return beacons;
        }

        public void TransformBeacons(Orientation orient)
        {
            foreach (Beacon b in beacons)
            {
                b.Transform(orient);
            }
        }
        public int ManhattenDistTo(Scanner s)
        {
            return Convert.ToInt32(Math.Abs(orientation.position.X - s.orientation.position.X) +
                Math.Abs(orientation.position.Y - s.orientation.position.Y) +
                Math.Abs(orientation.position.Z - s.orientation.position.Z));
        }

    }
}
