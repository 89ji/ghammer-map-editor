using Godot;
using System;
using System.Collections.Generic;
using gHammerMapEditor.Types;

public partial class Pane2D : Node2D
{
	public Subpane2D subpane {  get; private set; }
	[Export] private Dimension2D dimension;
	
	public override void _Ready()
	{
		subpane = GetNode<Subpane2D>("SubViewportContainer/SubViewport/SubPane");
		subpane.SetDimension(dimension);
	}
	
	public void UpdateMouseState(bool isMouseInside)
	{
		subpane.mouseIsInside = isMouseInside;
	}
}
