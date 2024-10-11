using System.Numerics;

namespace gHammerMapEditor.Util;

public static class Extensions
{
	public static Godot.Vector3 ToGDVec3(this Vector3 vec) => new Godot.Vector3(vec.X, vec.Y, vec.Z);
}