using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using gHammerMapEditor.Types;

public partial class CubeDrawer : Node2D
{
	private Brush brush;
	private Dimension2D dim;
	
	public static CubeDrawer Instantiate(Brush brush, Dimension2D dim)
	{
		var ret = new CubeDrawer();
		ret.brush = brush;
		ret.dim = dim;
		foreach (var Edge in Shapes.GetCubeEdges())
		{
			var line = new Line2D();
			line.Width = .25f;
			line.Antialiased = true;
			ret.AddChild(line);
			var point1 = brush.transformedPoints[Edge.Item1];
			var point2 = brush.transformedPoints[Edge.Item2];
			switch (dim)
			{
				case Dimension2D.XY:
					line.Points = new[]
					{
						new Vector2(point1.X, point1.Y),
						new Vector2(point2.X, point2.Y),
					};
					break;
				case Dimension2D.YZ:
					line.Points = new[]
					{
						new Vector2(point1.Y, point1.Z),
						new Vector2(point2.Y, point2.Z),
					};
					break;
				case Dimension2D.XZ:
					line.Points = new[]
					{
						new Vector2(point1.X, point1.Z),
						new Vector2(point2.X, point2.Z),
					};
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(dim), dim, null);
			}
		}
		return ret;
	}

	public void Refresh()
	{
		Shapes.GetCubeEdges().Zip<(Coord3d, Coord3d), Node, object>(GetChildren(), (edge, node) =>
		{
			Line2D line = (Line2D)node;
			var point1 = brush.transformedPoints[edge.Item1];
			var point2 = brush.transformedPoints[edge.Item2];
			
			switch (dim)
			{
				case Dimension2D.XY:
					line.Points = new[]
					{
						new Vector2(point1.X, point1.Y),
						new Vector2(point2.X, point2.Y),
					};
					break;
				case Dimension2D.YZ:
					line.Points = new[]
					{
						new Vector2(point1.Y, point1.Z),
						new Vector2(point2.Y, point2.Z),
					};
					break;
				case Dimension2D.XZ:
					line.Points = new[]
					{
						new Vector2(point1.X, point1.Z),
						new Vector2(point2.X, point2.Z),
					};
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(dim), dim, null);
			}
			return null;
		});
	}
}
