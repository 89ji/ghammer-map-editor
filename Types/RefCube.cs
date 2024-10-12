using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;

namespace gHammerMapEditor.Types;

class RefCube
{
    public readonly CoordSet Coords;
    public readonly List<Coord3d> Vertexes;
    public readonly List<(Coord3d, Coord3d)> Edges;
    public readonly List<(Coord3d, Coord3d, Coord3d, Coord3d)> Faces;

    public RefCube()
    {
        Coords = new CoordSet();
        Vertexes = new List<Coord3d>(new [] { Coords.A, Coords.B, Coords.C, Coords.D, Coords.E, Coords.F, Coords.G, Coords.H });
        Edges = new List<(Coord3d, Coord3d)>(new []
        {
            (Coords.A, Coords.B),
            (Coords.B, Coords.D),
            (Coords.C, Coords.D),
            (Coords.A, Coords.C),
            (Coords.C, Coords.G),
            (Coords.A, Coords.E),
            (Coords.B, Coords.F),
            (Coords.D, Coords.H),
            (Coords.E, Coords.F),
            (Coords.F, Coords.H),
            (Coords.G, Coords.H),
            (Coords.G, Coords.E)
        });
        // TODO: Reorganize the face point order so that the normals are consistent
        Faces = new List<(Coord3d, Coord3d, Coord3d, Coord3d)>(new[]
        {
            (Coords.A, Coords.E, Coords.G, Coords.C),
            (Coords.A, Coords.C, Coords.D, Coords.B),
            (Coords.A, Coords.B, Coords.F, Coords.E),
            (Coords.E, Coords.F, Coords.G, Coords.H),
            (Coords.F, Coords.G, Coords.D, Coords.B),
            (Coords.C, Coords.D, Coords.H, Coords.G),
        });
    }
}