using Godot;
using System;

public partial class BrushButtons : Node2D
{
	[Signal] public delegate void ButtonPressedEventHandler(int plusOrMinus);
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void TriggerUpperHandler(bool isAdd)
	{
		EmitSignal(SignalName.ButtonPressed, isAdd);
	}
}
