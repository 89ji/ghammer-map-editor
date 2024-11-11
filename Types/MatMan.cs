using System;
using gHammerMapEditor.Enums;
using Godot;

namespace gHammerMapEditor.Types;

public partial class MatMan : Node
{
	[Export] Material ConcFloor;
	[Export] Material ConcWall;
	[Export] Material Glass;
	[Export] Material ConcDark;
	[Export] Material ConcLight;
	[Export] Material Metal;
	[Export] Material Brick;
	[Export] Material IndoorConcFloor;
	[Export] Material Carpet;
	[Export] Material Ceiling;
	[Export] Material BrickWall;
	[Export] Material Dirt;
	[Export] Material Crate;

	public Material GetMat(Textures tex)
	{
		return tex switch
		{
			Textures.ConcFloor => ConcFloor,
			Textures.ConcWall => ConcWall,
			Textures.Glass => Glass,
			Textures.ConcDark => ConcDark,
			Textures.ConcLight => ConcLight,
			Textures.Metal => Metal,
			Textures.Brick => Brick,
			Textures.IndoorConcFloor => IndoorConcFloor,
			Textures.Carpet => Carpet,
			Textures.Ceiling => Ceiling,
			Textures.BrickWall => BrickWall,
			Textures.Dirt => Dirt,
			Textures.Crate => Crate,
			_ => throw new ArgumentOutOfRangeException(nameof(tex), tex, null)
		};
	}
}