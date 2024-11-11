using System;
using System.Collections.Generic;
using System.IO;
using gHammerMapEditor.Enums;
using gHammerMapEditor.Types;
using Godot;

namespace gHammerMapEditor.Serialization;

public static class Deserializer
{
	// Loads a .map from a path and places the brushes into the brushlist singleton
	// NOTICE: Currently clears the brushlist before appending
	public static void DeserializeMap(string src)
	{
		if (!File.Exists(src)) throw new FileNotFoundException();
		if (Path.GetExtension(src) != ".map") throw new Exception("Wrong file format");

		BrushList brushes = BrushList.Instance;
		brushes.Clear();

		string mapname;

		using StreamReader sr = new StreamReader(src);
		while (!sr.EndOfStream)
		{
			string line = sr.ReadLine();
			var tokens = line.Split(':');
			switch (tokens[0])
			{
				case "Mapname":
					mapname = ReadMapname(tokens[1]);
					break;
				case "Brush":
					brushes.AddMapObject(ReadBrush(tokens[1]));
					break;
				case "Entity":
					brushes.AddMapObject(ReadEnt(tokens[1]));
					break;
				case @"//":
					break;
				default:
					throw new Exception("Unknown token");
			}
		}
	}

	static string ReadMapname(string line)
	{
		return line.Trim();
	}

	static Brush ReadBrush(string line)
	{
		line = line.Trim().Replace("<", "").Replace(">", "").Replace(",", "");
		var numbers = line.Split(' ');

		System.Numerics.Vector3 translation = new(numbers[0].ToFloat(), numbers[1].ToFloat(), numbers[2].ToFloat());
		System.Numerics.Vector3 rotation = new(numbers[3].ToFloat(), numbers[4].ToFloat(), numbers[5].ToFloat());
		System.Numerics.Vector3 scale = new(numbers[6].ToFloat(), numbers[7].ToFloat(), numbers[8].ToFloat());
		Textures texture = numbers[9] switch
		{
			"ConcFloor" => Textures.ConcFloor,
			"ConcWall" => Textures.ConcWall,
			"Glass" => Textures.Glass,
			"ConcDark" => Textures.ConcDark,
			"ConcLight" => Textures.ConcLight,
			"Metal" => Textures.Metal,
			"Brick" => Textures.Brick,
			"IndoorConcFloor" => Textures.IndoorConcFloor,
			"Carpet" => Textures.Carpet,
			"Ceiling" => Textures.Ceiling,
			"BrickWall" => Textures.BrickWall,
			"Dirt" => Textures.Dirt,
			"Crate" => Textures.Crate,
			_ => throw new NotImplementedException($"Texture name {numbers[9]} not found!"),
		};
		return new Brush(new Transform(rotation, translation, scale), texture);
	}

	static Entity ReadEnt(string line)
	{
		line = line.Trim().Replace("<", "").Replace(">", "").Replace(",", "");
		var numbers = line.Split(' ');

		System.Numerics.Vector3? translation = new(numbers[0].ToFloat(), numbers[1].ToFloat(), numbers[2].ToFloat());
		System.Numerics.Vector3 rotation = new(numbers[3].ToFloat(), numbers[4].ToFloat(), numbers[5].ToFloat());
		Enums.EntityType type = numbers[6] switch
		{
			"Omni" => EntityType.OmniLight,
			"Spot" => EntityType.DirectLight,
			_ => throw new NotImplementedException(),
		};

		var ret = new Entity(type);
		ret.TranslateTo(translation.Value);
		ret.RotateTo(rotation);
		return ret;
	}
}