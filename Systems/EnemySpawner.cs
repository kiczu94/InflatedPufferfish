using Godot;
using InflatedPufferfish.Events;
using InflatedPufferfish.TkoUtilities.Utilities;
using TkoUtilities.EventBus;

public partial class EnemySpawner : Node
{
    private bool timeToSpawn = true;

    private PackedScene enemy;
    
    private EventBinding<EnemyOutOfFieldView> enemyOutOfFieldViewEventBinding;
    private EventBinding<SpawnEnemyEvent> spawnEnemyEventBinding;
    private Pool<Enemy> enemyPool = new();

    public override void _Ready()
    {
        enemy = ResourceLoader.Load("res://Entities/Enemies/Enemy.tscn") as PackedScene;
        enemyOutOfFieldViewEventBinding = new EventBinding<EnemyOutOfFieldView>(OnEnemyOutOfFieldViewEvent);
        spawnEnemyEventBinding = new EventBinding<SpawnEnemyEvent>(OnSpawnEnemyEvent);
        EventBus<SpawnEnemyEvent>.Register(spawnEnemyEventBinding);
        EventBus<EnemyOutOfFieldView>.Register(enemyOutOfFieldViewEventBinding);
        base._Ready();
    }

    private void OnEnemyOutOfFieldViewEvent(EnemyOutOfFieldView @event)
    {
        enemyPool.AddToPool(@event.Id);
    }

    private void OnSpawnEnemyEvent(SpawnEnemyEvent @event)
    {
        var enemy = enemyPool.GetFromPool(SpawnEnemy);
        enemy.SetPosition(new Vector2(320, @event.positionY));
        enemy.SetIsDead();
    }

    private Enemy SpawnEnemy()
    {
        var enemy = this.enemy.Instantiate() as Enemy;
        AddChild(enemy);
        return enemy;
    }
}
