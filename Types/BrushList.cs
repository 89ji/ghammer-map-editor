using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace gHammerMapEditor.Types;

public class BrushList : IEnumerable<Brush>
{
	public static readonly BrushList Instance = new();
	List<Brush> brushes = new();

	private BrushList()
	{
	}

	public void AddBrush(Brush brush)
	{
		brushes.Add(brush);
	}

	public void DeleteBrush(Brush brush)
	{
		brushes.Remove(brush);
	}

	public IEnumerator<Brush> GetEnumerator()
	{
		return brushes.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return brushes.GetEnumerator();
	}
}