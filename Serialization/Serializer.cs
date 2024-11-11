using System.IO;
using System.Text;
using gHammerMapEditor.Types;

namespace gHammerMapEditor.Serialization;

public static class Serializer
{
	// Turns a brushlist and other stuff into a text file
	// Dest is the folder, name is the name of file, .map is added automatically
	public static void SerializeMap(string dest, BrushList brushes)
	{
		StringBuilder sb = new();

		string mapname = "The name of the map goes here";
		sb.Append($"Mapname: {mapname}\n");
		foreach (var brush in brushes)
		{
			if (brush is Brush b) sb.Append($"Brush: {b.GetTranslate} {b.GetRotation} {b.GetScale} {b.texture}\n");
			else if (brush is Entity e) sb.Append($"Entity: {e.GetTranslate} {e.GetRotation} {e.GetScale} {e.Type switch {Enums.EntityType.OmniLight => "Omni", Enums.EntityType.DirectLight => "Spot",	Enums.EntityType.Spawn => "Spawn", Enums.EntityType.Void => "Void"}}\n");

		}

		using StreamWriter sw = new(dest);

		sw.Write(sb.ToString());
	}

	public static void SerializeMap(string dest, string name, BrushList brushes)
	{
		string fullPath = Path.Combine(dest, name + ".map");
		SerializeMap(fullPath, brushes);
	}
}