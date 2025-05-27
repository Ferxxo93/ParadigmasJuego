using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy : GameObject
    {
        private Image enemyImage = Engine.LoadImage("assets/enemy.png");
        private Animation currentAnimation;
        private int health = 100;
        private EnemyMovement enemyMovement;

        public Enemy(float positionX, float positionY) : base(positionX, positionY, new Vector2(30, 22))
        {
            enemyMovement = new EnemyMovement(Transform);
            CreateAnimation();
        }

        private void CreateAnimation()
        {
            List<Image> images = new List<Image>();

            for (int i = 0; i < 4; i++)
            {
                Image image = Engine.LoadImage($"assets/Enemy/Idle/{i}.png");
                images.Add(image);
            }

            currentAnimation = new Animation("idle", 0.1f, images, true);
        }

        public override void Update()
        {
            enemyMovement.Update();
            currentAnimation.Update();
            CheckCollisionsBarrels();
        }

        public override void Render()
        {
            Engine.Draw(currentAnimation.CurrentImage, Transform.Position.x, Transform.Position.y);
        }

        public bool CheckCollision(Bullet bullet)
        {
            float bulletX = bullet.Transform.Position.x;
            float bulletY = bullet.Transform.Position.y;
            float bulletRadius = bullet.Radius;

            return bulletX + bulletRadius > Transform.Position.x &&
                   bulletX - bulletRadius < Transform.Position.x + Transform.Scale.x &&
                   bulletY + bulletRadius > Transform.Position.y &&
                   bulletY - bulletRadius < Transform.Position.y + Transform.Scale.y;
        }

        public void GetDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                // Se notifica la eliminación al GameManager
                GameManager.Instance.NotifyEnemyDestroyed(this);
            }
        }

        private void CheckCollisionsBarrels()
        {
            foreach (var barrel in GameManager.Instance.LevelController.BarrelList)
            {
                float DistanceX = Math.Abs((barrel.Transform.Position.x + barrel.Transform.Scale.x / 2) - (Transform.Position.x + Transform.Scale.x / 2));
                float DistanceY = Math.Abs((barrel.Transform.Position.y + barrel.Transform.Scale.y / 2) - (Transform.Position.y + Transform.Scale.y / 2));

                float sumHalfWidth = barrel.Transform.Scale.x / 2 + Transform.Scale.x / 2;
                float sumHalfHeight = barrel.Transform.Scale.y / 2 + Transform.Scale.y / 2;

                if (DistanceX < sumHalfWidth && DistanceY < sumHalfHeight)
                {
                    float overlapX = sumHalfWidth - DistanceX;
                    float overlapY = sumHalfHeight - DistanceY;

                    if (overlapX < overlapY)
                    {
                        Transform.Position = Transform.Position.x < barrel.Transform.Position.x
                            ? new Vector2(Transform.Position.x - overlapX, Transform.Position.y)
                            : new Vector2(Transform.Position.x + overlapX, Transform.Position.y);
                    }
                    else
                    {
                        Transform.Position = Transform.Position.y < barrel.Transform.Position.y
                            ? new Vector2(Transform.Position.x, Transform.Position.y - overlapY)
                            : new Vector2(Transform.Position.x, Transform.Position.y + overlapY);
                    }
                    break;
                }
            }
        }
    }
}