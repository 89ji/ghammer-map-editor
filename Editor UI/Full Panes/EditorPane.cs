using Godot;
using System;
using System.Collections.Generic;
using gHammerMapEditor.Types;

public partial class EditorPane : Node2D
{
	private BrushList brushes;
	[Export] Pane2D Top;
	[Export] Pane2D Side;
	[Export] Pane2D Front;
	[Export] Pane3D View;
	[Export] double refreshRate = 0.1f;
	double lastRefreshTime = 0;
 	public override void _Ready()
    {
	    brushes = new BrushList(new []{Top, Side, Front}, View);
		
		brushes.AddBrush(new(new (new(5, 10, 15), new (10, 5, 0), new(20, 100, 10))));
		//brushes.Add(new(new (null, new (0, -10, 0), new(10, 1, 10))));
	}

	public override void _Process(double delta)
	{
		lastRefreshTime += delta;
		if (lastRefreshTime > refreshRate)
		{
			lastRefreshTime = 0;
			Top.subpane.Refresh();
			Side.subpane.Refresh();
			Front.subpane.Refresh();
			View.Refresh();
		}

		foreach (var brush in brushes) brush.Rotate(new((float)(.25f*delta), (float)(.4*delta), 0));
		
	}
}
