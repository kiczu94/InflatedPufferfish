using System;
using System.Threading.Tasks;

namespace Pong_1.Scripts.EventBus;

public interface IEventBinding<T>
{
    public Action<T> onEvent { get; set; }

    public Action onEventNoArgs { get; set; }

    public Func<T, Task> onEventAsync { get; set; }

    public Func<Task> onEventNoArgsAsync { get; set; }

    public void Add(Action<T> action);
    
    public void Add(Action action);

    public void Add(Func<T, Task> func) => this.onEventAsync += func;

    public void Add(Func<Task> func) => this.onEventNoArgsAsync += func;

    public void Remove(Action<T> action);

    public void Remove(Action action);

    public void Remove(Func<T, Task> func);

    public void Remove(Func<Task> func);
}

public class EventBinding<T> : IEventBinding<T> where T : IEvent
{
    public Action<T> onEvent = _ => { };

    public Action onEventNoArgs = () => { };

    public Func<T, Task> onEventAsync = async (T) => await Task.CompletedTask;

    public Func<Task> onEventNoArgsAsync = async () => await Task.CompletedTask;

    Action<T> IEventBinding<T>.onEvent { get => onEvent; set => onEvent = value; }

    Action IEventBinding<T>.onEventNoArgs { get => onEventNoArgs; set => onEventNoArgs = value; }
    
    Func<T, Task> IEventBinding<T>.onEventAsync { get => onEventAsync; set => onEventAsync = value; }
    
    Func<Task> IEventBinding<T>.onEventNoArgsAsync { get => onEventNoArgsAsync; set => onEventNoArgsAsync =value; }

    public EventBinding(Action<T> onEvent) { this.onEvent = onEvent; }

    public EventBinding(Action onEventNoArgs) { this.onEventNoArgs = onEventNoArgs; }

    public EventBinding(Func<T, Task> func) { this.onEventAsync = func; }

    public EventBinding(Func<Task> func) { this.onEventNoArgsAsync = func; }

    public void Add(Action<T> action) => this.onEvent += action;

    public void Add(Action action) => this.onEventNoArgs += action;

    public void Add(Func<T, Task> func) => this.onEventAsync += func;

    public void Add(Func<Task> func) => this.onEventNoArgsAsync += func;

    public void Remove(Action<T> action) => this.onEvent -= action;

    public void Remove(Action action) => this.onEventNoArgs -= action;

    public void Remove(Func<T, Task> func) => this.onEventAsync -= func;

    public void Remove(Func<Task> func) => this.onEventNoArgsAsync -= func;

}
