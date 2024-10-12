using Godot;
using System;

public partial class CameraManager3D : Camera3D
{
	Area2D area;
	private bool mouseIsInside;
	[Export] float CameraSpeed = 25;
	[Export] float MouseSensitivity = .005f;
	[Export] float MaxViewAngle = 1.5f;
	[Export] float MaxFOV = 120;
	[Export] float MinFOV = 30;
	[Export] float SprintMultiplier = 3f;
	
	private bool cameraMoveable;
	
	
	public override void _Ready()
	{
	}
	
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
			if (Input.GetActionStrength("sprint") > 0) movement *= SprintMultiplier;

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
			
			
			Translate(movement);
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
			var oldCameraRot = Rotation;
			oldCameraRot.X -= movement.Y;
			oldCameraRot.Y -= movement.X;
			if (oldCameraRot.X > MaxViewAngle) oldCameraRot.X = MaxViewAngle;
			if (oldCameraRot.X < -MaxViewAngle) oldCameraRot.X = -MaxViewAngle;
			
			Rotation = oldCameraRot;
		}
		else if (@event is InputEventMouseButton mouseButtonEvent)
		{
			if (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp)
			{
				Fov -= 3;
				if (Fov < MinFOV) Fov = MinFOV;
			}
			else if (mouseButtonEvent.ButtonIndex == MouseButton.WheelDown)
			{
				Fov += 3;
				if (Fov > MaxFOV) Fov = MaxFOV;
			}
		}
	}
}
