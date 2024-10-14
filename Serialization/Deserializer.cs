using System;
using System.Collections.Generic;
using System.IO;
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
		
		System.Numerics.Vector3? translation = new(numbers[0].ToFloat(), numbers[1].ToFloat(), numbers[2].ToFloat());
		System.Numerics.Vector3 rotation = new(numbers[3].ToFloat(), numbers[4].ToFloat(), numbers[5].ToFloat());
		System.Numerics.Vector3 scale = new(numbers[6].ToFloat(), numbers[7].ToFloat(), numbers[8].ToFloat());
		
		return new Brush(new Transform(translation, rotation, scale));
	}
}