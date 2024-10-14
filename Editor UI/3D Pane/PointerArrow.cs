using Godot;
using System;

public partial class PointerArrow : Node3D
{
	// Settable values
	Vector3 centroid;
	Vector3 direction;
	float size;

	float tailLength;
	MeshInstance3D head;
	MeshInstance3D tail;
	
	
	
	public override void _Ready()
	{
		head = GetNode<MeshInstance3D>("Head");
		tail = GetNode<MeshInstance3D>("Tail");
		
		
	}

	
	public override void _Process(double delta)
	{
		Position = centroid;
		Rotation = direction;


	}
}
