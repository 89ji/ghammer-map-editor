using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using gHammerMapEditor.Types;
using gHammerMapEditor.Util;

public partial class Pane3D : Node2D
{
	[Export] Node3D BrushParent;
	[Export] PackedScene refCube;
	[Export] PackedScene refEntity;
	Dictionary<MapObject, Node3D> brush2mesh = new();
	BrushList brushList;
	
	[Signal] public delegate void TargetUpdatedEventHandler();
	Crosshair crossMan;
	public MapObject lookingBrush;
	
	public override void _Ready()
	{
		crossMan = GetNode<Crosshair>("SubViewportContainer/SubViewport/3D Area/Camera/Crosshair");
		brushList = BrushList.Instance;
		crossMan.Toggle(true);
	}
	
	
	public override void _Process(double delta)
	{
		Refresh();
	}

	public void NotifyAddBrush(MapObject b)
	{
		if(brush2mesh.ContainsKey(b)) throw new Exception("Brush already added!");
		Node3D obj;

		switch (b)
		{
			case Brush brush:
				obj = refCube.Instantiate<Node3D>();
				break;
			case Entity entity:
				obj = refEntity.Instantiate<Node3D>();
				((RefEntity)obj).SetEntityType(entity.Type);
				break;
			default:
				throw new Exception("Unknown mapobj type!");
		}
		
		obj.Transform = b.GetTransform().ToGDTransform();
		obj.Visible = true;
		BrushParent.AddChild(obj);
		brush2mesh.Add(b, obj);
	}

	public void NotifyDelBrush(MapObject b)
	{
		if(!brush2mesh.ContainsKey(b)) throw new Exception("Brush doesn't exist!");
		var mesh = brush2mesh[b];
		mesh.QueueFree();
		brush2mesh.Remove(b);
	}

	private void Refresh()
	{
		List<MapObject> oldBrushList = brush2mesh.Keys.ToList();
		
		foreach (var brush in brushList)
		{
			// Dict has brush
			if (brush2mesh.ContainsKey(brush))
			{
				brush2mesh[brush].Transform = brush.GetTransform().ToGDTransform();
				oldBrushList.Remove(brush);
			}

			// Brush is new
			else NotifyAddBrush(brush);
		}
		
		// Cleanup old brushes
		foreach (var brush in oldBrushList) NotifyDelBrush(brush);
		
	}

	void TargetUpdatedHandler()
	{
		foreach (var brush in brush2mesh.Keys) if (brush2mesh[brush] == crossMan.collisionObject) lookingBrush = brush;
		EmitSignal(SignalName.TargetUpdated);
	}
}
