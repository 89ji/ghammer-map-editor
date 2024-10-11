using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;

namespace gHammerMapEditor.Types;

static class Shapes
{
    private static readonly List<Coord3d> Coords = new (new Coord3d[]
    {
        new (0, 0, 0),
        new (0, 0, 1),
        new (1, 0, 1),
        new (1, 0, 1),
        new (0, 1, 1),
        new (0, 1, 1),
        new (1, 1, 1),
        new (1, 1, 1)
    });
    public static ReadOnlyCollection<Coord3d> GetCubePoints()
    {
        return Coords.AsReadOnly();
    }

    private static readonly List<(Coord3d, Coord3d)> CubeEdges = new (new (Coord3d,Coord3d)[]
        {
            (new (0, 0, 0), new (1, 0, 0)),
            (new (0, 0, 0), new (0, 1, 0)),
            (new (0, 0, 0), new (0, 0, 1)),
            (new (1, 1, 1), new (0, 1, 1)),
            (new (1, 1, 1), new (1, 0, 1)),
            (new (1, 1, 1), new (1, 1, 0)),
            (new (0, 1, 0), new (0, 1, 1)),
            (new (0, 1, 1), new (0, 0, 1)),
            (new (1, 0, 0), new (1, 1, 0)),
            (new (1, 0, 0), new (1, 0, 1)),
            (new (0, 0, 1), new (1, 0, 1)),
            (new (0, 1, 0), new (1, 1, 0))
        });
    public static ReadOnlyCollection<(Coord3d, Coord3d)> GetCubeEdges()
    {
        return CubeEdges.AsReadOnly();
    }
    
}