using Godot;

public partial class SceneManager : Node
{
    public static void ChangeScene(string scenePath)
    {
        var sceneTree = Engine.GetMainLoop() as SceneTree; 
        sceneTree.ChangeSceneToFile(scenePath);
    }
}
