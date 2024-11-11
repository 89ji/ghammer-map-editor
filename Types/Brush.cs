using System.Collections.Generic;
using System.Numerics;
using gHammerMapEditor.Enums;

namespace gHammerMapEditor.Types;

public class Brush : MapObject
{
	public readonly Dictionary<Coord3d, Coord3d> TransformedPoints = new();
	private readonly RefCube refCube = new();
	public Textures texture = Textures.Crate;

	public Brush(Transform transform)
	{
		this.transform = transform;
		foreach (var coord in refCube.Vertexes) TransformedPoints.Add(coord, coord);
		CalculateTransform();
	}

	public Brush(Transform transform, Textures tex)
	{
		this.transform = transform;
		texture = tex;
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
	
	public new void TranslateBy(Vector3 translation)
	{
		transform.TranslateBy(translation);
		CalculateTransform();
	}

	public new void RotateBy(Vector3 rotate)
	{
		transform.RotateBy(rotate);
		CalculateTransform();
	}

	public new void ScaleBy(Vector3 scale)
	{
		transform.ScaleBy(scale);
		CalculateTransform();
	}

	public new void TranslateTo(Vector3 translation)
	{
		transform.TranslateTo(translation);
		CalculateTransform();
	}
	
	public new void RotateTo(Vector3 rotate)
	{
		transform.RotateTo(rotate);
		CalculateTransform();
	}

	public new void ScaleTo(Vector3 scale)
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

	public override void OnSelect()
	{
		return;
		throw new System.NotImplementedException();
	}

	public override void OnDeselect()
	{
		return;
		throw new System.NotImplementedException();
	}
}
