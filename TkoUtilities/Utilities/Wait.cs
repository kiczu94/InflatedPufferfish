namespace InflatedPufferfish.TkoUtilities.Utilities;

public static class Wait
{
    public static async Task For(int miliseconds, Action action)
    {
       await Task.Delay(miliseconds);
        action.Invoke();
    }
}
