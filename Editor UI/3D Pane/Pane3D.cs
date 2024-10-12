using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using gHammerMapEditor.Types;
using gHammerMapEditor.Util;

public partial class Pane3D : Node2D
{
	[Export] private Node3D BrushParent;
	[Export] private MeshInstance3D refMesh;
	Dictionary<Brush, MeshInstance3D> brush2mesh = new();
	BrushList brushList;
	
	public override void _Ready()
	{
		brushList = BrushList.Instance;
	}
	
	
	public override void _Process(double delta)
	{
		Refresh();
	}

	public void NotifyAddBrush(Brush b)
	{
		if(brush2mesh.ContainsKey(b)) throw new Exception("Brush already added!");
		
		MeshInstance3D mesh = (MeshInstance3D)refMesh.Duplicate();
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
				foreach (var normal in brush.GetNormals())
				{
					
					MeshInstance3D mesh = (MeshInstance3D)refMesh.Duplicate();
					mesh.Translate((brush.GetTranslate.ToGDVec3() + normal.ToGDVector3() * 10));
					mesh.Visible = true;
				}
				
				
				brush2mesh[brush].Transform = brush.GetTransform().ToGDTransform();
				oldBrushList.Remove(brush);
			}

			// Brush is new
			else NotifyAddBrush(brush);
		}
		
		// Cleanup old brushes
		foreach (var brush in oldBrushList) NotifyDelBrush(brush);
	}
	
}
