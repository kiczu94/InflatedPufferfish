using Godot;

namespace InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine;

internal class EnemyContext
{
    public bool isDead;

    public bool isEating;

    public Enemy enemy;

    public Vector2 enemySpeed = new(-90, 0);

    public int animationFrame;
}
