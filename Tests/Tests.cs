using gHammerMapEditor.Types;
using Godot;
using System;

public partial class Tests : Node2D
{
	public override void _Ready()
	{
		Transform trans = new(null, new (1, 2, 3), new (2, 2, 2));
		Coord3d crd = new(1, 1, 1);
		Coord3d crd2 = new(2, 2, 2);
		
		var newCrd = trans * crd;
		var newCrd2 = trans * crd2;
		
		GD.Print($"X: {newCrd.X}, Y: {newCrd.Y}, z: {newCrd.Z}");
		GD.Print($"X: {newCrd2.X}, Y: {newCrd2.Y}, z: {newCrd2.Z}");
	}

	public override void _Process(double delta)
	{
	}
}
