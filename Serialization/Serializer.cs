using System.IO;
using System.Text;
using gHammerMapEditor.Types;

namespace gHammerMapEditor.Serialization;

public static class Serializer
{
	// Turns a brushlist and other stuff into a text file
	// Dest is the folder, name is the name of file, .map is added automatically
	public static void SerializeMap(string dest, BrushList mapObjects)
	{
		StringBuilder sb = new();
		
		string mapname = "The name of the map goes here";
		sb.Append($"Mapname: {mapname}\n");
		foreach (var mapObj in mapObjects)
		{
			switch (mapObj)
			{
				case Brush b:
					sb.Append($"Brush: {b.GetTranslate} {b.GetRotation} {b.GetScale}\n");
					break;
				case Entity e:
					sb.Append($"Entity: {e.GetTranslate} {e.GetRotation} {e.GetScale}\n");
					break;
			}
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