using Godot;
using System;
using System.Collections.Generic;
using gHammerMapEditor.Types;
using Vector3 = System.Numerics.Vector3;

public partial class EditorPane : Node2D
{
	private BrushList brushes;
	[Export] Pane3D View;

 	public override void _Ready()
    {
	    brushes = BrushList.Instance;
		
		//brushes.AddBrush(new(new (new(5, 10, 15), new (10, 5, 0), new(20, 100, 10))));
		//brushes.AddBrush(new(new (null, new (0, -10, 0), new(10, 1, 10))));
		brushes.AddBrush(new(new Transform(null, null, new Vector3(10, 10, 10))));
	}

	public override void _Process(double delta)
	{
		//foreach (var brush in brushes) brush.Rotate(new((float)(.01f*delta), (float)(.01*delta), 0));
	}
}
