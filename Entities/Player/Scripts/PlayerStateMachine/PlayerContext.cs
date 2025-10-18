using Godot;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine;

public class PlayerContext
{
    public bool KeyToInflateIsPressed;
    public bool KeyToFastDeflateJustPressed;
    public bool KeyToBlockJustPressed;
    public bool IsAttacking;
    public bool PlayerLost;
    public byte BodySizeFrameNumber;
    public byte AttackingFrameNumber;
    public readonly float MaximumSpeedDeflating = 100f;
    public readonly float MaximumSpeedInflating = -100f;

    public PlayerScript Player;
}
