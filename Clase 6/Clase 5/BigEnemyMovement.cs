using MyGame;
using System;

public class BigEnemyMovement : EnemyMovement
{
    private float slowSpeed = 0.8f; // Más lento que el speed original 1.5f

    public BigEnemyMovement(Transform transform) : base(transform)
    {
    }

    public override void Update()
    {
        Player player = GameManager.Instance.LevelController.Player1;
        if (player == null) return;

        Vector2 direction = player.Transform.Position - base.transform.Position;
        float distance = (float)Math.Sqrt(direction.x * direction.x + direction.y * direction.y);

        if (distance <= detectionRange)
        {
            direction.x /= distance;
            direction.y /= distance;

            base.transform.Translate(direction, slowSpeed);
        }
    }
}