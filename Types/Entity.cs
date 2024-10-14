using System.Numerics;
using gHammerMapEditor.Enums;

namespace gHammerMapEditor.Types;

public class Entity : MapObject
{
	public EntityType Type { set; get; }

	public Entity(EntityType entityType)
	{
		Type = entityType;
	}
	
	public override void ScaleBy(Vector3 scale)
	{
		return;
	}

	public override void ScaleTo(Vector3 scale)
	{
		return;
	}
}