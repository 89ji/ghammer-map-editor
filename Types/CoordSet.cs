namespace gHammerMapEditor.Types;

public class CoordSet
{
	public readonly Coord3d A = new (0, 0, 0);
	public readonly Coord3d B = new(1, 0, 0);
	public readonly Coord3d C = new(0, 1, 0);
	public readonly Coord3d D = new(1, 1, 0);
	public readonly Coord3d E = new(0, 0, 1);
	public readonly Coord3d F = new(1, 0, 1);
	public readonly Coord3d G = new(0, 1, 1);
	public readonly Coord3d H = new(1, 1, 1);
}