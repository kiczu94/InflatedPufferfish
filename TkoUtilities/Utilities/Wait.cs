namespace InflatedPufferfish.TkoUtilities.Utilities;

public static class Wait
{
    public static async Task For(int milliseconds, Action action, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.Delay(milliseconds, cancellationToken);

            action.Invoke();
        }
        catch (TaskCanceledException)
        {
        }
    }
}
