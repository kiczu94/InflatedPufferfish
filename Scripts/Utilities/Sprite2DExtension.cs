using Godot;

namespace Pong_1.Scripts.Utilities;

public static class Sprite2DExtension
{
    public static float GetEffectiveTextureHeight(this Sprite2D sprite2D)
    {
        return sprite2D.Texture.GetHeight() * sprite2D.GlobalScale.Y;
    }
}
