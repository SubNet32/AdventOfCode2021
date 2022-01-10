using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AdventOfCode.Day19Content
{
   
    class Orientation
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 referenceRotation;

        public Orientation()
        {
            position = Vector3.Zero;
            rotation = Vector3.Zero;
            referenceRotation = Vector3.Zero;
        }
        public Orientation(Orientation orient)
        {
            position = orient.position;
            rotation = new Vector3(orient.rotation.X, orient.rotation.Y, orient.rotation.Z);
            referenceRotation = Vector3.Zero;
        }

        public Orientation(Vector3 axis, float angle)
        {
            position = Vector3.Zero;
            SetRotation(axis, angle);
            referenceRotation = Vector3.Zero;
        }
        public Orientation(Vector3 rotation)
        {
            position = Vector3.Zero;
            this.rotation = rotation;
            referenceRotation = Vector3.Zero;
            LimitRotation();
        }

        public void SetRotation(Vector3 axis, float angle)
        {
            this.rotation = axis*angle;
            LimitRotation();
        }
        public void SetRotation(Vector3 rotation)
        {
            this.rotation = rotation;
            LimitRotation();
        }

        public void AddRotation(Vector3 axis, float angle)
        {
            this.rotation += axis * angle;
            LimitRotation();
        }
        public void AddRotation(Vector3 rotation)
        {
            this.rotation += rotation;
            LimitRotation();
        }

        public void LimitRotation()
        {
            while (rotation.X >= 360)
                rotation.X -= 360;
            while (rotation.Y >= 360)
                rotation.Y -= 360;
            while (rotation.Z >= 360)
                rotation.Z -= 360;

            while (rotation.X <= -360)
                rotation.X += 360;
            while (rotation.Y <= -360)
                rotation.Y += 360;
            while (rotation.Z <= -360)
                rotation.Z += 360;
        }

        public static Vector3 RotateVector(Vector3 vector, Vector3 rotation)
        {
            if(rotation.X!=0)
            {
                vector = Vector3.Transform(vector, Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), Utilities.ConvertToRadians(rotation.X)));
            }
            if (rotation.Y != 0)
            {
                vector = Vector3.Transform(vector, Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), Utilities.ConvertToRadians(rotation.Y)));
            }
            if (rotation.Z != 0)
            {
                vector = Vector3.Transform(vector, Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), Utilities.ConvertToRadians(rotation.Z)));
            }

            vector.X = Convert.ToSingle(Math.Round(vector.X));
            vector.Y = Convert.ToSingle(Math.Round(vector.Y));
            vector.Z = Convert.ToSingle(Math.Round(vector.Z));

            return vector;
        }

        public Vector3 RotateVector(Vector3 vector)
        {
            return RotateVector(vector, this.rotation);
        }

        public static Vector3 RotateVectorInverse(Vector3 vector, Vector3 rotation)
        {
            if (rotation.X != 0)
            {
                vector = Vector3.Transform(vector, Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), Utilities.ConvertToRadians(rotation.X * -1)));
            }
            if (rotation.Y != 0)
            {
                vector = Vector3.Transform(vector, Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), Utilities.ConvertToRadians(rotation.Y * -1)));
            }
            if (rotation.Z != 0)
            {
                vector = Vector3.Transform(vector, Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), Utilities.ConvertToRadians(rotation.Z*-1)));
            }
            
           

            vector.X = Convert.ToSingle(Math.Round(vector.X));
            vector.Y = Convert.ToSingle(Math.Round(vector.Y));
            vector.Z = Convert.ToSingle(Math.Round(vector.Z));

            return vector;
        }


        public Vector3 RotateVectorInverse(Vector3 vector)
        {
            return RotateVector(vector, this.rotation);
        }


        public static List<Orientation> GetPossibleOrientations()
        {
            List<Orientation> orientations = new List<Orientation>();
            orientations.Add(new Orientation());
            orientations.Add(new Orientation(new Vector3(1, 0, 0), 90));
            orientations.Add(new Orientation(new Vector3(1, 0, 0), -90));
            orientations.Add(new Orientation(new Vector3(1, 0, 0), 180));
            orientations.Add(new Orientation(new Vector3(0, 1, 0), 90));
            orientations.Add(new Orientation(new Vector3(0, 1, 0), -90));
            orientations.Add(new Orientation(new Vector3(0, 1, 0), 180));
            orientations.Add(new Orientation(new Vector3(0, 0, 1), 90));
            orientations.Add(new Orientation(new Vector3(0, 0, 1), -90));
            orientations.Add(new Orientation(new Vector3(0, 0, 1), 180));
            //orientations.Add(new Orientation(new Vector3(0, 1, 1), 90));
            //orientations.Add(new Orientation(new Vector3(0, 1, 1), -90));
            //orientations.Add(new Orientation(new Vector3(0, 1, 1), 180));
            //orientations.Add(new Orientation(new Vector3(1, 1, 0), 90));
            //orientations.Add(new Orientation(new Vector3(1, 1, 0), -90));
            //orientations.Add(new Orientation(new Vector3(1, 1, 0), 180));
            //orientations.Add(new Orientation(new Vector3(1, 1, 1), 90));
            //orientations.Add(new Orientation(new Vector3(1, 1, 1), -90));
            //orientations.Add(new Orientation(new Vector3(1, 1, 1), 180));
            return orientations;
        }
     

        public override string ToString()
        {
            return "Pos: " + position.ToString() + " Rot: " + rotation.ToString() +" RefRot: "+referenceRotation.ToString();
        }
    }
    
}
