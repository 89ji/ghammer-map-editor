using Godot;
using System;
using System.Collections.Generic;
using gHammerMapEditor.Enums;
using gHammerMapEditor.Types;
using Vector3 = System.Numerics.Vector3;
using gHammerMapEditor.Serialization;
using System.IO;
using gHammerMapEditor.Util;

public partial class EditorPane : Node2D
{
	private BrushList brushes;
	[Export] Pane3D View;
	[Export] BrushSelector selector;
	[Export] PropertiesEditor properties;
	

	MapObject currentBrush;		// The current selected brush
	MapObject hovredBrush;		// The brush hovered by the camera

	string SavePath;

	float NudgeAmount = 1;
	
 	public override void _Ready()
    {
	    brushes = BrushList.Instance;
		
		//brushes.AddBrush(new(new (new(5, 10, 15), new (10, 5, 0), new(20, 100, 10))));
		//brushes.AddBrush(new(new (null, new (0, -10, 0), new(10, 1, 10))));
		//brushes.AddBrush(new(new Transform(null, null, new Vector3(10, 10, 10))));
		//brushes.AddMapObject(new Entity(EntityType.Void));
	}

	public override void _Process(double delta)
	{
		if (Input.IsMouseButtonPressed(MouseButton.Right))
		{
			selector.ChangeSelection(hovredBrush);
		}
	}

	void AddBrush()
	{
		brushes.AddMapObject(new Brush());
	}

	void AddLight()
	{
		brushes.AddMapObject(new Entity(EntityType.OmniLight));
	}

	void AddSpotlight()
	{
		brushes.AddMapObject(new Entity(EntityType.DirectLight));
	}

	void Delete()
	{
		brushes.DeleteMapObject(currentBrush);
	}

	void Duplicate()
	{
		switch (currentBrush) 
		{
			case Brush b:
				brushes.AddMapObject(new Brush(new Transform(b.GetRotation, b.GetTranslate, b.GetScale), b.texture));
				break;
			case Entity e:
				Entity dupe = new(e.Type);
				dupe.transform = new(e.GetRotation, e.GetTranslate, e.GetScale);
				brushes.AddMapObject(dupe);
				break;
			default:
				throw new Exception("Duplicatino failed!");
		}
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

	void LoadMap(string path)
	{
		SavePath = path;
		Deserializer.DeserializeMap(path);
	}

	void SaveMap()
	{
		Serializer.SerializeMap(SavePath, brushes);
	}

	void ReloadMap()
	{
		selector.Reset();
		Serializer.SerializeMap(SavePath, brushes);
		Deserializer.DeserializeMap(SavePath);
	}

	void AdjustNudge(string Nudge)
	{
		float val;
		try
		{
			val = Nudge.ToFloat();
			NudgeAmount = val;
		}
		catch (Exception)
		{
		}
	}

	void TransBy(int dim, bool neg)
	{
		Vector3 trans = new();
		switch (dim)
		{
			case 0:
				trans.X = (neg ? -1 : 1) * NudgeAmount;
				break;
			case 1:
				trans.Y = (neg ? -1 : 1) * NudgeAmount;
				break;
			case 2:
				trans.Z = (neg ? -1 : 1) * NudgeAmount;
				break;

		}
		currentBrush.TranslateBy(trans);
	}

	void RotBy(int dim, bool neg)
	{
		Vector3 trans = new();
		switch (dim)
		{
			case 0:
				trans.X = (neg ? -1 : 1) * NudgeAmount.toRad();
				break;
			case 1:
				trans.Y = (neg ? -1 : 1) * NudgeAmount.toRad();
				break;
			case 2:
				trans.Z = (neg ? -1 : 1) * NudgeAmount.toRad();
				break;

		}
		currentBrush.RotateBy(trans);
	}

	void ScaleBy(int dim, bool neg)
	{
		Vector3 trans = new();
		switch (dim)
		{
			case 0:
				trans.X = (neg ? -1 : 1) * NudgeAmount;
				break;
			case 1:
				trans.Y = (neg ? -1 : 1) * NudgeAmount;
				break;
			case 2:
				trans.Z = (neg ? -1 : 1) * NudgeAmount;
				break;

		}
		currentBrush.ScaleBy(trans);
	}

	void SetMaterial(int texIdAsInt)
	{
		if(currentBrush == null) return;
		Textures tex = (Textures) texIdAsInt;
		if (currentBrush is Brush b)
		{
			b.texture = tex;
			View.UpdateCurrentTexture(tex, b);
		}
	}
}
