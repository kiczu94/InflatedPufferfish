using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class PlayerAnimationPlayer : AnimationPlayer
{
    private void OnAnimationFinished(StringName animationName) =>
        EventBus<AnimationFinished>.Raise(new AnimationFinished(animationName.ToString()));
}
