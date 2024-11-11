using Godot;
using System;

public partial class RefCube : StaticBody3D
{
	[Export] MeshInstance3D cube;
	[Export] MeshInstance3D sheen;
	public override void _Ready()
	{
	}

	public void Toggle(bool on)
	{
		sheen.Visible = on;
	}

	public void SetMat(Material mat)
	{
		cube.MaterialOverride = mat;
	}
}
