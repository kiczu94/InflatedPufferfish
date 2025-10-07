using Godot;

namespace TkoUtilities.Utilities;

public static class CollisionExtensions
{
    public static ICollection<StringName> GetColliderGroups(this Node2D node, KinematicCollision2D collision)
    {
        var collider = collision.GetCollider();
        var colliderPath = collider.Call("get_path").ToString();
        var colliderNode = node.GetNode(colliderPath);

        return colliderNode.GetGroups();
    }

    public static Node GetColliderNode(this Node2D node, KinematicCollision2D collision)
    {
        var collider = collision.GetCollider();
        var colliderPath = collider.Call("get_path").ToString();

        return node.GetNode(colliderPath);
    }

    public static string GetColliderPath(this Node2D node, KinematicCollision2D collision)
    {
        var collider = collision.GetCollider();

        return collider.Call("get_path").ToString();
    }
}
