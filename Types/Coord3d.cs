using System;
using System.Numerics;

namespace gHammerMapEditor.Types;

public class Coord3d
{
    Vector4 vector;
    public float X => vector.X / vector.W;
    public float Y => vector.Y / vector.W;
    public float Z => vector.Z / vector.W;

    public Coord3d(float x = 0, float y = 0, float z = 0)
    {
        vector = new(x, y, z, 1);
    }

    public float Length()
    {
        return MathF.Sqrt(X * X + Y * Y + Z * Z);
    }
    public Coord3d Normalize()
    {
        float magnitude = Length();
        return new(X/magnitude, Y/magnitude, Z/magnitude);
    }
    
    public static Coord3d operator*(Transform lhs, Coord3d rhs)
    {
        Vector4 res = new();
        Matrix4x4 m = lhs.GetMat();
        res.X = m.M11 * rhs.X + m.M12 * rhs.Y + m.M13 * rhs.Z + m.M14;
        res.Y = m.M21 * rhs.X + m.M22 * rhs.Y + m.M23 * rhs.Z + m.M24;
        res.Z = m.M31 * rhs.X + m.M32 * rhs.Y + m.M33 * rhs.Z + m.M34;
        res.W = m.M41 * rhs.X + m.M42 * rhs.Y + m.M43 * rhs.Z + m.M44;
        return new Coord3d(res.X/res.W, res.Y/res.W, res.Z/res.W);
    }
    public static Vector3d operator-(Coord3d lhs, Coord3d rhs) => new (lhs.X-rhs.X, lhs.Y-rhs.Y, lhs.Z-rhs.Z);
    public static float Dot(Coord3d lhs, Coord3d rhs) => lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
    
    public static Vector3d Cross(Coord3d a, Coord3d b)
    {
        return new(a.Y*b.Z - a.Z*b.Y, a.Z*b.X - a.X*b.Z, a.X*b.Y - a.Y*b.X);
    }

    public Vector3d Cross(Coord3d b)
    {
        return new(Y * b.Z - Z * b.Y, Z * b.X - X * b.Z, X * b.Y - Y * b.X);
    }
    public Vector3d Cross(Vector3d b)
    {
        return new(Y * b.Z - Z * b.Y, Z * b.X - X * b.Z, X * b.Y - Y * b.X);
    }

    public Godot.Vector3 ToGDVector3()
    {
        return new Godot.Vector3(vector.X, vector.Y, vector.Z);
    }
}