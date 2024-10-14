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
			sb.Append($"Brush: {brush.GetTranslate} {brush.GetRotation} {brush.GetScale}\n");
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