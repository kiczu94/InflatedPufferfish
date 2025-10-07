using Godot;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine;

public class PlayerContext
{
    public bool KeyToInflateIsPressed;
    public bool KeyToFastDeflateJustPressed;
    public bool KeyToBlockJustPressed;
    public bool IsAttacking;
    public byte BodySizeFrameNumber;
    public byte AttackingFrameNumber;

    public CharacterBody2D player;
}
