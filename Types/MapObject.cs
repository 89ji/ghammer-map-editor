using System.Collections.Generic;

namespace gHammerMapEditor.Types;

using System.Numerics;

public abstract class MapObject
{
	public readonly Transform transform;

	public MapObject(Transform transform)
	{
		this.transform = transform;
	}

	public MapObject()
	{
		transform = new();
	}
	

	public void TranslateBy(Vector3 translation)
	{
		transform.TranslateBy(translation);
	}

	public void RotateBy(Vector3 rotate)
	{
		transform.RotateBy(rotate);
	}

	public virtual void ScaleBy(Vector3 scale)
	{
		transform.ScaleBy(scale);
	}

	public void TranslateTo(Vector3 translation)
	{
		transform.TranslateTo(translation);
	}

	public void RotateTo(Vector3 rotate)
	{
		transform.RotateTo(rotate);
	}

	public virtual void ScaleTo(Vector3 scale)
	{
		transform.ScaleTo(scale);
	}

	public Vector3 GetTranslate => transform.Translation;
	public Vector3 GetRotation => transform.Rotation;
	public Vector3 GetScale => transform.Scale;
	public Transform GetTransform() => transform;

	// Called when the map object is selected in the editor
	public abstract void OnSelect();
	
	// Called when the map object is deselected in the editor
	public abstract void OnDeselect();
}