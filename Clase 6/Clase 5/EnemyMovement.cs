using MyGame;
using System;

public class EnemyMovement
{
    private Transform transform;
    private float speed = 1.5f;
    private float detectionRange = 500f;

    public EnemyMovement(Transform transform)
    {
        this.transform = transform;
    }

    public void Update()
    {
        Player player = GameManager.Instance.LevelController.Player1;
        if (player == null) return;

        Vector2 direction = player.Transform.Position - transform.Position;
        float distance = (float)Math.Sqrt(direction.x * direction.x + direction.y * direction.y);

        if (distance <= detectionRange)
        {
            direction.x /= distance;
            direction.y /= distance;

            transform.Translate(direction, speed);
        }
    }
}