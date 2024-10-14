using Godot;
using System;
using System.Collections.Generic;
using gHammerMapEditor.Enums;
using gHammerMapEditor.Types;
using Vector3 = System.Numerics.Vector3;

public partial class EditorPane : Node2D
{
	private BrushList brushes;
	[Export] Pane3D View;
	[Export] BrushSelector selector;
	[Export] PropertiesEditor properties;
	

	MapObject currentBrush;		// The current selected brush
	MapObject hovredBrush;		// The brush hovered by the camera
	
 	public override void _Ready()
    {
	    brushes = BrushList.Instance;
		
		//brushes.AddBrush(new(new (new(5, 10, 15), new (10, 5, 0), new(20, 100, 10))));
		//brushes.AddBrush(new(new (null, new (0, -10, 0), new(10, 1, 10))));
		//brushes.AddBrush(new(new Transform(null, null, new Vector3(10, 10, 10))));
		brushes.AddMapObject(new Entity(EntityType.Void));
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
		if(isAdd) brushes.AddMapObject(new Brush());
		else brushes.DeleteMapObject(currentBrush);
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
