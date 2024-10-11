using Godot;
using System;
using System.Collections.Generic;
using gHammerMapEditor.Types;

public partial class Subpane2D : Node2D
{
	Camera2D camera;
	Node2D background;
	Dimension2D dimension;
	Vector3 projectionVector;
	

	public bool mouseIsInside;
	[Export] float CameraSpeed = 750f;
	[Export] float MinZoom = 0.1f;
	[Export] float MaxZoom = 10f;

	Dictionary<Brush, CubeDrawer> brush2draw = new();
	[Export] private Node2D BrushParent;
	[Export] private Node2D RefMesh;
	
	public override void _Ready()
	{
		camera = GetNode<Camera2D>("Camera");
		background = GetNode<Node2D>("Camera/Background");
	}
	
	public override void _Process(double delta)
	{
		if (mouseIsInside)
		{
			Vector2 movement = new(
				Input.GetActionStrength("right") - Input.GetActionStrength("left"),
				Input.GetActionStrength("back") - Input.GetActionStrength("fwd")
			);
			
			if (movement != Vector2.Zero) movement = movement.Normalized();
			movement = movement * CameraSpeed * (float)delta /camera.Zoom.X;
			if (Input.GetActionStrength("sprint") > 0) movement *= 1.5f;
			
			camera.Translate(movement);
		}
	}

	public void SetDimension(Dimension2D dimension)
	{
		this.dimension = dimension;
		GetNode<RichTextLabel>("Camera/Background/Face Label").Text = dimension switch
		{
			Dimension2D.XY => "top (x/y)",
			Dimension2D.YZ => "front (y/z)",
			Dimension2D.XZ => "side (x/z)",
			_ => throw new ArgumentOutOfRangeException()
		};
		projectionVector = dimension switch
		{
			Dimension2D.YZ => new Vector3(0, 1, 1),
			Dimension2D.XZ => new Vector3(1, 0, 1),
			Dimension2D.XY => new Vector3(1, 1, 0),
		};
	}
	
	public override void _Input(InputEvent @event)
	{
		if (!mouseIsInside) return;
		if (@event is InputEventMouseButton mouseButtonEvent)
		{
			if (mouseButtonEvent.ButtonIndex == MouseButton.WheelDown)
			{
				var oldCam = camera.Zoom;
				oldCam.X -= .1f;
				oldCam.Y -= .1f;
				if (oldCam.X < MinZoom)
				{
					oldCam.X = MinZoom;
					oldCam.Y = MinZoom;
				}
				camera.Zoom = oldCam;
			}
			else if (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp)
			{
				var oldCam = camera.Zoom;
				oldCam.X += .1f;
				oldCam.Y += .1f;
				if (oldCam.X > MaxZoom)
				{
					oldCam.X = MaxZoom;
					oldCam.Y = MaxZoom;
				}
				camera.Zoom = oldCam;
			}
		}
		var zoom = camera.Zoom;
		background.Scale = new Vector2(1/zoom.X, 1/zoom.Y);
	}

	public void NotifyAddBrush(Brush b)
	{
		if (brush2draw.ContainsKey(b)) throw new ArgumentException("Added brush already exists!");
		var cub = CubeDrawer.Instantiate(b, dimension);
		BrushParent.AddChild(cub);
		brush2draw.Add(b, cub);
	}

	public void NotifyDelBrush(Brush b)
	{
		if (!brush2draw.ContainsKey(b)) throw new ArgumentException("Deleted brush doesn't exist!");
		var cub = brush2draw[b];
		cub.QueueFree();
		brush2draw.Remove(b);
	}
	
	public void Refresh()
	{
		foreach (var cub in brush2draw.Values) cub.Refresh();
	}
	
}
