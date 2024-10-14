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
	Dictionary<Brush, Node3D> brush2mesh = new();
	BrushList brushList;
	
	[Signal] public delegate void TargetUpdatedEventHandler();
	Crosshair crossMan;
	public Brush lookingBrush;
	
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

	public void NotifyAddBrush(Brush b)
	{
		if(brush2mesh.ContainsKey(b)) throw new Exception("Brush already added!");

		var mesh = refCube.Instantiate<Node3D>();
		mesh.Transform = b.GetTransform().ToGDTransform();
		mesh.Visible = true;
		BrushParent.AddChild(mesh);
		brush2mesh.Add(b, mesh);
	}

	public void NotifyDelBrush(Brush b)
	{
		if(!brush2mesh.ContainsKey(b)) throw new Exception("Brush doesn't exist!");
		var mesh = brush2mesh[b];
		mesh.QueueFree();
		brush2mesh.Remove(b);
	}

	private void Refresh()
	{
		List<Brush> oldBrushList = brush2mesh.Keys.ToList();
		
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
