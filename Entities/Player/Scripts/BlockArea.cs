using Godot;
using System;

public partial class BlockArea : Area2D
{
    private PlayerScript playerScript;

    public override void _Ready()
    {
        playerScript = GetParent<PlayerScript>();
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }
}
