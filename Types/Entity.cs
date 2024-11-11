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

	public override void OnSelect()
	{
<<<<<<< HEAD
		mapObj.Select();
=======
		return;
		throw new System.NotImplementedException();
>>>>>>> 14f623163bc330b0543876a34674a9b9dc013702
	}

	public override void OnDeselect()
	{
<<<<<<< HEAD
		mapObj.Select();
=======
		return;
		throw new System.NotImplementedException();
>>>>>>> 14f623163bc330b0543876a34674a9b9dc013702
	}
}