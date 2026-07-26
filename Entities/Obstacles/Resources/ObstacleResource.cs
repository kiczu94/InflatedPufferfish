using Godot;

[GlobalClass]
public partial class ObstacleResource : Resource
{
    [Export]
    public Vector2[] Points;

    [Export]
    public int Height;

    [Export]
    public int Width;

    [Export]
    public Texture2D Texture;
}
