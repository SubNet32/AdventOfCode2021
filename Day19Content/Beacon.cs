using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AdventOfCode.Day19Content
{
    class Beacon
    {
        public Vector3 originalPosition;
        public Vector3 position;

        public Beacon(string s)
        {
            string[] n = s.Split(',');
            originalPosition = new Vector3(float.Parse(n[0]), float.Parse(n[1]), float.Parse(n[2]));
            position = originalPosition;
        }

        public Beacon(Vector3 position)
        {
            this.originalPosition = position;
            this.position = position;
        }

        public BeaconCompare CompareTo(Beacon b)
        {
            return new BeaconCompare(position, b.position);
        }
        public BeaconCompare CompareTo(Beacon b, Orientation orientation)
        {
            return new BeaconCompare(position, b.position, orientation);
        }

        public Vector3 GetPositionInRelationTo(Orientation o)
        {
            Vector3 p = Orientation.RotateVector(position, o.rotation);
            p = Orientation.RotateVector(p, o.referenceRotation);
            p += o.position;
            return p;
        }

        public void Transform(Orientation orient)
        {
            this.position = Orientation.RotateVector(position, orient.rotation) + orient.position;
        }

        public int ManhattenDistTo(Beacon b)
        {
            return Convert.ToInt32(Math.Abs(position.X - b.position.X) +
                Math.Abs(position.Y - b.position.Y) +
                Math.Abs(position.Z - b.position.Z));
        }
    }

    class BeaconCompare
    {
        public Vector3 a;
        public Vector3 b;
        public Vector3 bRot;
        public Vector3 dist;
        public Vector3 position;
        public Orientation orientation;

        public BeaconCompare(Vector3 a, Vector3 b)
        {
            this.a = a;
            this.b = b;
            this.dist = a - b;
            this.position = Vector3.Zero;
            this.orientation = new Orientation();
        }

        public BeaconCompare(Vector3 a, Vector3 b, Orientation orientation)
        {
            this.position = Vector3.Zero;
            this.a = a;
            this.b = b;
            this.orientation = new Orientation(orientation.rotation);
            this.bRot = this.orientation.RotateVector(b);
            this.dist = a - bRot;
        }

        public override string ToString()
        {
            return "A: " + a.ToString() + " B: " + b.ToString() + " BRot: " + bRot.ToString() + " Dist: "+dist.ToString() + " Orient: " + orientation.ToString();
        }

    }
}
