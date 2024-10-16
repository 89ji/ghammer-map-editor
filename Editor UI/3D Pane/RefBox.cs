using Godot;
using System;
using gHammerMapEditor.Editor_UI._3D_Pane;

public partial class RefBox : StaticBody3D, ISelectable
{
	[Export] MeshInstance3D Cube;
	[Export] MeshInstance3D Sheen;
	Material Material;
	Texture2D Texture;
	public override void _Ready()
	{
		Sheen.Hide();
		Material = Cube.Mesh.SurfaceGetMaterial(0);
		var image = Image.LoadFromFile("C:\\Users\\Yasuda\\Documents\\Projects\\gHammer Editor\\Resources\\Textures\\poserAnakin.jpg");
		Texture = ImageTexture.CreateFromImage(image);
		Material.Set("albedo_texture", Texture);
	}

	public void Select()
	{
		Sheen.Show();
	}

	public void Deselect()
	{
		Sheen.Hide();
	}
}
