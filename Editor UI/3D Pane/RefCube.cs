using Godot;
using System;

public partial class RefCube : StaticBody3D
{
	MeshInstance3D sheen;
	public override void _Ready()
	{
		sheen = GetNode<MeshInstance3D>("Sheen");
	}

	public void Toggle(bool on)
	{
		sheen.Visible = on;
	}
}
