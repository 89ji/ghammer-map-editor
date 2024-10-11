using System.Collections.Generic;
using System.Numerics;

namespace gHammerMapEditor.Types;

public class Brush
{
	private readonly Transform transform;

	//List<Face> faces;
	readonly HashSet<Coord3d> coordSet;
	public Dictionary<Coord3d, Coord3d> transformedPoints = new();

	public Brush(Transform transform)
	{
		this.transform = transform;
		coordSet = new HashSet<Coord3d>();
		foreach (var coord in Shapes.GetCubePoints()) coordSet.Add(coord);
	}

	public void CalculateTransform()
	{
		transformedPoints.Clear();

		foreach (var coord in coordSet)
		{
			var tCoord = transform * coord;
			transformedPoints.Add(coord, tCoord);
		}
	}
	public void Translate(Vector3 translation)
	{
		transform.TranslateBy(translation);
		CalculateTransform();
	}

	public void Rotate(Vector3 rotate)
	{
		transform.RotateBy(rotate);
		CalculateTransform();
	}

	public void Scale(Vector3 scale)
	{
		transform.ScaleBy(scale);
		CalculateTransform();
	}
	
	public Vector3 GetTranslate => transform.Translation;
	public Vector3 GetRotation => transform.Rotation;
	public Vector3 GetScale => transform.Scale;

	public Transform GetTransform() => transform;
}