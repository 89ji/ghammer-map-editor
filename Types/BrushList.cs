using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace gHammerMapEditor.Types;

public class BrushList : IEnumerable<Brush>
{
	List<Brush> brushList;
	
	List<Pane2D> pane2DList;
	Pane3D pane3d;

	public BrushList(IEnumerable<Pane2D> PaneList, Pane3D pane3d)
	{
		this.pane3d = pane3d;
		pane2DList = PaneList.ToList();
		brushList = new List<Brush>();
	}

	public void AddBrush(Brush brush)
	{
		brushList.Add(brush);
		foreach (var pane in pane2DList) pane.subpane.NotifyAddBrush(brush);
		pane3d.NotifyAddBrush(brush);
		brush.CalculateTransform();
	}

	public void DeleteBrush(Brush brush)
	{
		brushList.Remove(brush);
		foreach (var pane in pane2DList) pane.subpane.NotifyDelBrush(brush);
		pane3d.NotifyDelBrush(brush);
	}

	public IEnumerator<Brush> GetEnumerator()
	{
		return brushList.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return brushList.GetEnumerator();
	}
}