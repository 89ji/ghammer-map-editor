using gHammerMapEditor.Enums;
using gHammerMapEditor.Types;
using Godot;
using System;

public partial class MaterialSelector : Node2D
{
	[Export] OptionButton options;
	[Signal] public delegate void OnMaterialSelectEventHandler(int texAsInt);

	public override void _Ready()
	{
		foreach (Textures tex in Enum.GetValues(typeof(Textures)))
		{
			options.AddItem(tex.ToString());
		}
	}

	public void OnSelect(int index)
	{
		string chosen = options.GetItemText(index);
		foreach (Textures tex in Enum.GetValues(typeof(Textures)))
		{
			if (tex.ToString().Equals(chosen)) EmitSignal(SignalName.OnMaterialSelect, (int)tex );
		}
	}
}
