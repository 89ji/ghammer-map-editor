using System;
using System.Numerics;

namespace gHammerMapEditor.Types;

public class Coord3d : IEquatable<Coord3d>
{
    Vector4 vector;
    public float X => vector.X / vector.W;
    public float Y => vector.Y / vector.W;
    public float Z => vector.Z / vector.W;

    public Coord3d(float x = 0, float y = 0, float z = 0)
    {
        vector = new(x, y, z, 1);
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

    public bool Equals(Coord3d other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return vector.Equals(other.vector);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        Coord3d other = (Coord3d)obj;
        if(X != other.X) return false;
        if(Y != other.Y) return false;
        if(Z != other.Z) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ (int) Y.GetHashCode() ^ (int) Z.GetHashCode();
    }
}