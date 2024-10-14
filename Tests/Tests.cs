using gHammerMapEditor.Types;
using Godot;
using System;
using gHammerMapEditor.Serialization;
using Vector3 = System.Numerics.Vector3;

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

		BrushList bl = BrushList.Instance;
		bl.AddBrush(new());
		bl.AddBrush(new(new (new(5, 10, 15), new (10, 5, 0), new(20, 100, 10))));
		bl.AddBrush(new(new (null, new (0, -10, 0), new(10, 1, 10))));
		bl.AddBrush(new(new Transform(null, null, new  Vector3(10, 10, 10))));

		string path = @"C:\Users\Jocelyn\Documents\Projects\ghammer-map-editor\exported_data.map";
		Serializer.SerializeMap(path, bl);
		
		bl.ClearBrushes();
		
		Deserializer.DeserializeMap(path);
		bl.AddBrush(new());
	}

	public override void _Process(double delta)
	{
	}
}
