using Godot;

namespace TkoUtilities.Utilities;

public static class AudioStreamPlayerExtension
{
    public static void PlaySound(this AudioStreamPlayer streamPlayer, AudioStreamWav sound)
    {
        if (!streamPlayer.Playing)
        {
            streamPlayer.Stream = sound;
            streamPlayer.Play();
        }
    }
}
