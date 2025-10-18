using Godot;

namespace InflatedPufferfish.TkoUtilities.Pooling;

public class Pool<T> where T : Node2D
{
    HashSet<T> visibleObjects = [];
    HashSet<T> notVisibleObjects = [];

    public T GetFromPool(Func<T> createMethod)
    {
        if (notVisibleObjects.Count > 0)
        {
            var returnedObject = notVisibleObjects.First();
            notVisibleObjects.Remove(returnedObject);
            visibleObjects.Add(returnedObject);
            returnedObject.SetVisible(true);
            return returnedObject;
        }

        var newlyCreatedObject = createMethod.Invoke();
        visibleObjects.Add(newlyCreatedObject);
        return newlyCreatedObject;
    }

    public void AddToPool(ulong id)
    {
        var poolObject = visibleObjects.SingleOrDefault(x => x.GetInstanceId() == id);
        if (poolObject == null)
        {
            return;
        }
        poolObject.SetVisible(false);
        visibleObjects.Remove(poolObject);
        notVisibleObjects.Add(poolObject);
    }
}
