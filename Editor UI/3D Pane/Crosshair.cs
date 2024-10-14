using Godot;
using System;

public partial class Crosshair : Node3D
{
	[Export] MeshInstance3D dot;
	[Export] RayCast3D raycast;
	Camera3D parent;
	bool Enabled;
	public Node3D collisionObject;
	[Signal] public delegate void TargetUpdatedEventHandler();
	public override void _Ready()
	{
		parent = GetParent<Camera3D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!Enabled) return;
		if (!raycast.IsColliding())
		{
			dot.Hide();
			return;
		}
		
		dot.Show();
		var newObj = (Node3D)raycast.GetCollider();
		if (newObj != collisionObject)
		{
			collisionObject = newObj;
			EmitSignal(SignalName.TargetUpdated);
			
		}
		
		var colPt = raycast.GetCollisionPoint();
		var distance = parent.Position.DistanceTo(colPt);

		float scaledDistance = 1 + 1f * Mathf.Pow(distance, .5f);
		dot.Scale = new Vector3(scaledDistance, scaledDistance, scaledDistance);
		
		var newDotPos = dot.Position;
		newDotPos.Z = - Mathf.Abs(distance) + .5f;
		dot.Position = newDotPos;
	}

	public void Toggle(bool enabled)
	{
		Enabled = enabled;
		if (enabled)
		{
			dot.Show();
			raycast.Enabled = true;
		}
		else
		{
			dot.Hide();
			raycast.Enabled = false;
		}
	}
	
}
