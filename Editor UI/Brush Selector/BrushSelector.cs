using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using gHammerMapEditor.Types;
using gHammerMapEditor.Util;

public partial class BrushSelector : Node2D
{
	BrushList brushList = BrushList.Instance;
	ItemList brushUI;
	Dictionary<MapObject, int> brush2id = new();
	public MapObject SelectedBrush { get; private set; }
	[Signal] public delegate void BrushSelectedEventHandler();
	
	public override void _Ready()
	{
		brushUI = GetNode<ItemList>("Control/ItemList");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Refresh();
		var selection = brushUI.GetSelectedItems();
		if (selection.Length > 0)
		{
			foreach (var brush in brushList)
			{
				if (brush2id[brush] == selection[0])
				{
					if (SelectedBrush != brush)
					{
						
						SelectedBrush = brush;
						EmitSignal(SignalName.BrushSelected);
					}
					break;
				}
			}
		}
	}
	
	
	private void Refresh()
	{
		List<MapObject> oldBrushList = brush2id.Keys.ToList();
		
		foreach (var brush in brushList)
		{
			// Dict has brush
			if (brush2id.ContainsKey(brush))
			{
				oldBrushList.Remove(brush);
			}


			// Brush is new
			else AddObjectToList(brush);
		}
		
		// Cleanup old brushes
		foreach (var brush in oldBrushList)
		{
			if(SelectedBrush == brush) SelectedBrush = null;
			RemoveObjectFromList(brush);
		}
	}

	void AddObjectToList(MapObject brush)
	{
		var idx = brushUI.AddItem(brush.GetScale.X.ToString());
		brush2id.Add(brush, idx);
	}

	void RemoveObjectFromList(MapObject brush)
	{
		brushUI.RemoveItem(brush2id[brush]);
		brush2id.Remove(brush);
	}

	public void ChangeSelection(MapObject newSelection)
	{
		int newId = brush2id[newSelection];
		brushUI.Select(newId);
		SelectedBrush = newSelection;
		EmitSignal(SignalName.BrushSelected);
	}
}
