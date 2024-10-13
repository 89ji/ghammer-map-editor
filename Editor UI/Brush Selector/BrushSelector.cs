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
	Dictionary<Brush, int> brush2id = new();
	public Brush SelectedBrush { get; private set; }
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
					SelectedBrush = brush;
					break;
				}
			}
		}
	}
	
	
	private void Refresh()
	{
		List<Brush> oldBrushList = brush2id.Keys.ToList();
		
		foreach (var brush in brushList)
		{
			// Dict has brush
			if (brush2id.ContainsKey(brush))
			{
				oldBrushList.Remove(brush);
			}


			// Brush is new
			else AddBrushToList(brush);
		}
		
		// Cleanup old brushes
		foreach (var brush in oldBrushList)
		{
			if(SelectedBrush == brush) SelectedBrush = null;
			RemoveBrushFromList(brush);
		}
	}

	void AddBrushToList(Brush brush)
	{
		var idx = brushUI.AddItem(brush.GetScale.X.ToString());
		brush2id.Add(brush, idx);
	}

	void RemoveBrushFromList(Brush brush)
	{
		brushUI.RemoveItem(brush2id[brush]);
		brush2id.Remove(brush);
	}
}
