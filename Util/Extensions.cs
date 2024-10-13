using System;
using System.Numerics;

namespace gHammerMapEditor.Util;

public static class Extensions
{
	public static Godot.Vector3 ToGDVec3(this Vector3 vec) => new Godot.Vector3(vec.X, vec.Y, vec.Z);
	public static float toDeg(this float radians) => radians * 180f / MathF.PI;
	public static float toRad(this float degrees) => degrees / 180f * MathF.PI;
}