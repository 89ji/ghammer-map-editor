using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace gHammerMapEditor.Types;

public class BrushList : IEnumerable<MapObject>
{
	public static readonly BrushList Instance = new();
	List<MapObject> brushes = new();

	private BrushList()
	{
	}

	public void AddMapObject(MapObject brush)
	{
		brushes.Add(brush);
	}

	public void DeleteMapObject(MapObject brush)
	{
		brushes.Remove(brush);
	}

	public IEnumerator<MapObject> GetEnumerator() =>  brushes.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => brushes.GetEnumerator();
	public void Clear() => brushes.Clear();

}