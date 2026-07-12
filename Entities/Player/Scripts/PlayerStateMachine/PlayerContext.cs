namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine;

public class PlayerContext
{
    public bool KeyToInflateIsPressed;
    public bool KeyToFastDeflateJustPressed;
    public bool KeyToBlockJustPressed;
    public bool IsAttacking;
    public bool PlayerLost;
    public readonly float MaximumSpeedDeflating = 90f;
    public readonly float MaximumSpeedInflating = -90f;

    public PlayerScript Player;
}
