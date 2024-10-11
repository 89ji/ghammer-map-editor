using System.Collections.Generic;

namespace gHammerMapEditor.Types;

class Face
{
    List<Coord3d> Verticies;

    public Face(List<Coord3d> vertexList)
    {
        Verticies = vertexList;
    }
}