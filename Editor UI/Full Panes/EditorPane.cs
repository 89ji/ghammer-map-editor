using Godot;
using System;
using System.Collections.Generic;
using gHammerMapEditor.Types;
using Vector3 = System.Numerics.Vector3;

public partial class EditorPane : Node2D
{
	private BrushList brushes;
	[Export] Pane3D View;
	[Export] BrushSelector selector;
	[Export] PropertiesEditor properties;
	

	Brush currentBrush;		// The current selected brush
	Brush hovredBrush;		// The brush hovered by the camera
	
 	public override void _Ready()
    {
	    brushes = BrushList.Instance;
		
		//brushes.AddBrush(new(new (new(5, 10, 15), new (10, 5, 0), new(20, 100, 10))));
		//brushes.AddBrush(new(new (null, new (0, -10, 0), new(10, 1, 10))));
		//brushes.AddBrush(new(new Transform(null, null, new Vector3(10, 10, 10))));
	}

	public override void _Process(double delta)
	{
		if (Input.IsMouseButtonPressed(MouseButton.Right))
		{
			selector.ChangeSelection(hovredBrush);
		}
	}

	void ButtonPressed(bool isAdd)
	{
		if(isAdd) brushes.AddBrush(new());
		else brushes.DeleteBrush(currentBrush);
	}

	void OnNewBrushSelected()
	{
		currentBrush = selector.SelectedBrush;
		properties.UpdateBrush(currentBrush);
	}

	void OnTargetUpdatedHandler()
	{
		hovredBrush = View.lookingBrush;
	}
}
