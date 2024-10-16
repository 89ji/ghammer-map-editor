using System.Collections.Generic;
using System.Numerics;

namespace gHammerMapEditor.Types;

public class Brush : MapObject
{
	public readonly Dictionary<Coord3d, Coord3d> TransformedPoints = new();
	private readonly RefCube refCube = new();

	public Brush(Transform transform)
	{
		foreach (var coord in refCube.Vertexes) TransformedPoints.Add(coord, coord);
		CalculateTransform();
	}

	public Brush()
	{
		foreach (var coord in refCube.Vertexes) TransformedPoints.Add(coord, coord);
		CalculateTransform();
	}

	private void CalculateTransform()
	{
		foreach (var coord in refCube.Vertexes)
		{
			var tCoord = transform * coord;
			TransformedPoints[coord] = tCoord;
		}
	}
	
	public void TranslateBy(Vector3 translation)
	{
		transform.TranslateBy(translation);
		CalculateTransform();
	}

	public void RotateBy(Vector3 rotate)
	{
		transform.RotateBy(rotate);
		CalculateTransform();
	}

	public void ScaleBy(Vector3 scale)
	{
		transform.ScaleBy(scale);
		CalculateTransform();
	}

	public void TranslateTo(Vector3 translation)
	{
		transform.TranslateTo(translation);
		CalculateTransform();
	}
	
	public void RotateTo(Vector3 rotate)
	{
		transform.RotateTo(rotate);
		CalculateTransform();
	}

	public void ScaleTo(Vector3 scale)
	{
		transform.ScaleTo(scale);
		CalculateTransform();
	}
	

	public List<Coord3d> GetNormals()
	{
		List<Coord3d> normals = new();
		foreach (var face in refCube.Faces)
		{
			var A = TransformedPoints[face.Item1] - TransformedPoints[face.Item2];
			var B = TransformedPoints[face.Item2] - TransformedPoints[face.Item3];

			var norm1 = A.Cross(B);
			
			normals.Add(norm1.Normalize());
		}
		return normals;
	}
	
	public Vector3 GetTranslate => transform.Translation;
	public Vector3 GetRotation => transform.Rotation;
	public Vector3 GetScale => transform.Scale;
	public Coord3d GetCentroid()
	{
		float X = 0;
		float Y = 0;
		float Z = 0;
		foreach (var point in TransformedPoints.Values)
		{
			X += point.X;
			Y += point.Y;
			Z += point.Z;
		}
		return new Coord3d(X/8, Y/8, Z/8);
	}

	public Transform GetTransform() => transform;
	public override void OnSelect()
	{
		refCube
		throw new System.NotImplementedException();
	}

	public override void OnDeselect()
	{
		throw new System.NotImplementedException();
	}
}