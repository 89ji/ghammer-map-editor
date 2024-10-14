using Godot;
using System;
using gHammerMapEditor.Types;
using gHammerMapEditor.Util;

public partial class PropertiesEditor : Node2D
{
	[Export] LineEdit transX;
	[Export] LineEdit transY;
	[Export] LineEdit transZ;
		
	[Export] LineEdit rotX;
	[Export] LineEdit rotY;
	[Export] LineEdit rotZ;
		
	[Export] LineEdit scaleX;
	[Export] LineEdit scaleY;
	[Export] LineEdit scaleZ;
	
	BrushList brushList = BrushList.Instance;
	MapObject currentBrush;
	
	public override void _Ready()
	{

	}
	
	public override void _Process(double delta)
	{
		UpdateValues();
	}

	void LoadValues()
	{
		var trans = currentBrush.GetTranslate;
		var rot = currentBrush.GetRotation;
		var scale = currentBrush.GetScale;
		
		transX.Text = trans.X.ToString();
		transY.Text = trans.Y.ToString();
		transZ.Text = trans.Z.ToString();
		
		rotX.Text = rot.X.toDeg().ToString();
		rotY.Text = rot.Y.toDeg().ToString();
		rotZ.Text = rot.Z.toDeg().ToString();
		
		scaleX.Text = scale.X.ToString();
		scaleY.Text = scale.Y.ToString();
		scaleZ.Text = scale.Z.ToString();
	}
	
	void UpdateValues()
	{
		if (currentBrush == null) return;
		try
		{
			System.Numerics.Vector3 editorTrans = new (transX.Text.ToFloat(), transY.Text.ToFloat(), transZ.Text.ToFloat());
			System.Numerics.Vector3 editorRot = new (rotX.Text.ToFloat().toRad(), rotY.Text.ToFloat().toRad(), rotZ.Text.ToFloat().toRad());
			System.Numerics.Vector3 editorScale = new (scaleX.Text.ToFloat(), scaleY.Text.ToFloat(), scaleZ.Text.ToFloat());
			
			currentBrush.TranslateTo(editorTrans);
			currentBrush.RotateTo(editorRot);
			currentBrush.ScaleTo(editorScale);
		}
		catch
		{
			
		}
	}

	void ClearEntries()
	{
		transX.Clear();
		transY.Clear();
		transZ.Clear();
		rotX.Clear();
		rotY.Clear();
		rotZ.Clear();
		scaleX.Clear();
		scaleY.Clear();
		scaleZ.Clear();
		transX.Editable = true;
	}

	public void UpdateBrush(MapObject newBrush)
	{
		currentBrush = newBrush;
		if (currentBrush == null) ClearEntries();
		else LoadValues();
	}
}
