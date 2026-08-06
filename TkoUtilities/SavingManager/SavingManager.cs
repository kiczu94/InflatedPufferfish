#nullable enable
using System.Text.Json;
using System.Threading.Tasks;
using Godot;

namespace TkoUtilities.SavingManager;

public partial class SavingManager : Node
{
    public static async Task SaveGameAsync<T>(T saveableEntity) where T : class, ISaveableEntity
    {
        var json = JsonSerializer.Serialize(saveableEntity);
        var path = ProjectSettings.GlobalizePath("user://savegame.json");
        await File.WriteAllTextAsync(path, json);
    }

    public static async Task<T?> LoadGameAsync<T>() where T : class, ISaveableEntity
    {
        var path = ProjectSettings.GlobalizePath("user://savegame.json");
        if (File.Exists(path))
        {
            var json = await File.ReadAllTextAsync(path);
            var obj = JsonSerializer.Deserialize<T>(json);
            return obj;
        }

        return null;
    }
}
