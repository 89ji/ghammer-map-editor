using Godot;
using System;
using System.Collections.Generic;
using gHammerMapEditor.Types;

public partial class Pane3D : Node2D
{
Camera3D camera;
	Area2D area;
	private bool mouseIsInside;
	[Export] float CameraSpeed = 25;
	[Export] float MouseSensitivity = .005f;
	[Export] float MaxViewAngle = 1.5f;
	[Export] float MaxFOV = 120;
	[Export] float MinFOV = 30;
	
	private bool cameraMoveable;
	
	[Export] private Node3D BrushParent;
	[Export] private MeshInstance3D RefMesh;
	Dictionary<Brush, MeshInstance3D> brush2mesh = new();
	
	public override void _Ready()
	{
		camera = GetNode<Camera3D>("SubViewportContainer/SubViewport/3D Area/Camera");
		area = GetNode<Area2D>("Pane Area");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (mouseIsInside)
		{
			Vector3 movement = new(
				Input.GetActionStrength("right") - Input.GetActionStrength("left"),
				Input.GetActionStrength("up") - Input.GetActionStrength("down"),
				Input.GetActionStrength("back") - Input.GetActionStrength("fwd")
				);
			
			if (movement != Vector3.Zero) movement = movement.Normalized();
			movement = movement * CameraSpeed * (float)delta;
			if (Input.GetActionStrength("sprint") > 0) movement *= 1.5f;

			// Button is inside and clicking, do camera move stuff
			if (Input.IsMouseButtonPressed(MouseButton.Left))
			{
				cameraMoveable = true;
				Input.SetMouseMode(Input.MouseModeEnum.Captured);
			}

			// Inside but no click
			else
			{
				cameraMoveable = false;
				Input.SetMouseMode(Input.MouseModeEnum.Visible);
			}
			
			
			camera.Translate(movement);
		}
		
		// The mouse is not in the window
		else
		{
			Input.SetMouseMode(Input.MouseModeEnum.Visible);
		}
	}

	public void UpdateMouseState(bool isMouseInside)
	{
		mouseIsInside = isMouseInside;
	}

	public override void _Input(InputEvent @event)
	{
		if (!cameraMoveable) return;
		if (@event is InputEventMouseMotion mouseEvent)
		{
			var movement = mouseEvent.Relative * MouseSensitivity;
			var oldCameraRot = camera.Rotation;
			oldCameraRot.X -= movement.Y;
			oldCameraRot.Y -= movement.X;
			if (oldCameraRot.X > MaxViewAngle) oldCameraRot.X = MaxViewAngle;
			if (oldCameraRot.X < -MaxViewAngle) oldCameraRot.X = -MaxViewAngle;
			
			camera.Rotation = oldCameraRot;
		}
		else if (@event is InputEventMouseButton mouseButtonEvent)
		{
			if (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp)
			{
				camera.Fov -= 3;
				if (camera.Fov < MinFOV) camera.Fov = MinFOV;
			}
			else if (mouseButtonEvent.ButtonIndex == MouseButton.WheelDown)
			{
				camera.Fov += 3;
				if (camera.Fov > MaxFOV) camera.Fov = MaxFOV;
			}
		}
	}
	

	public void NotifyAddBrush(Brush b)
	{
		if(brush2mesh.ContainsKey(b)) throw new Exception("Brush already added!");
		
		MeshInstance3D mesh = (MeshInstance3D)RefMesh.Duplicate();
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

	public void Refresh()
	{
		foreach (var brush in brush2mesh.Keys)
		{
			brush2mesh[brush].Transform = brush.GetTransform().ToGDTransform();
		}
	}
	
}
