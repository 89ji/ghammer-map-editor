using Godot;
using System;
using System.Collections.Generic;
using gHammerMapEditor.Enums;

public partial class RefEntity : StaticBody3D
{
	[Export] Sprite3D OLight;
	[Export] Sprite3D DLight;
	[Export] Sprite3D Spawn;
	[Export] Sprite3D Void;
	[Export] MeshInstance3D Pointer;
	List<Sprite3D> RefList;

	double CurrentTime = 0;
	public override void _Process(double delta)
	{
		CurrentTime += delta;
		Pointer.RotateObjectLocal(Vector3.Up, (float)(delta * 3));
		Pointer.Position = new Vector3(0, 0, (float)(.1f * Mathf.Sin(CurrentTime * 4) + .75f));
			
			
	}

	public void SetEntityType(EntityType entityType)
	{
		RefList = new List<Sprite3D>(new[] { OLight, DLight, Spawn, Void});
		
		RefList.ForEach(sprite3D => sprite3D.Hide());
		switch (entityType)
		{
			case EntityType.OmniLight:
				OLight.Show();
				break;
			case EntityType.DirectLight:
				DLight.Show();
				break;
			case EntityType.Spawn:
				Spawn.Show();
				break;
			case EntityType.Void:
				Void.Show();
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
		}
	}
}
