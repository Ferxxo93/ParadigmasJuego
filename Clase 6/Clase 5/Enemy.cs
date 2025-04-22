using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy
    {
        private Image enemyImage = Engine.LoadImage("assets/enemy.png");

        private EnemyMovement enemyMovement;
        private Transform transform;
        private Animation currentAnimation;
        private float speed = 1.5f;

        private int health = 100;

        public Transform Transform => transform;

        public Enemy(float positionX, float positionY)
        {
            transform = new Transform(new Vector2(positionX, positionY), new Vector2(30, 22));  // Tamaño del enemigo
            enemyMovement = new EnemyMovement(transform);
            CreateAnimation();
        }

        public void CreateAnimation()
        {
            List<Image> images = new List<Image>();

            for (int i = 0; i < 4; i++)
            {
                Image image = Engine.LoadImage($"assets/Enemy/Idle/{i}.png");
                images.Add(image);
            }

            currentAnimation = new Animation("idle", 0.1f, images, true);
        }

        public void Update()
        {
            enemyMovement.Update();
            currentAnimation.Update(); 
            CheckCollisionsBarrels();
        }

        public void Render()
        {
            Engine.Draw(currentAnimation.CurrentImage, transform.Position.x, transform.Position.y);
            
        }

        public void GetDamage(int damage)
        {
            health -= damage;

            if (health < 0)
            {
                GameManager.Instance.LevelController.EnemyList.Remove(this);
            }
        }

        private void CheckCollisionsBarrels()
        {
            foreach (var barrel in GameManager.Instance.LevelController.BarrelList)
            {
                float DistanceX = Math.Abs((barrel.Transform.Position.x + barrel.Transform.Scale.x / 2) - (transform.Position.x + transform.Scale.x / 2));
                float DistanceY = Math.Abs((barrel.Transform.Position.y + barrel.Transform.Scale.y / 2) - (transform.Position.y + transform.Scale.y / 2));

                float sumHalfWidth = barrel.Transform.Scale.x / 2 + transform.Scale.x / 2;
                float sumHalfHeight = barrel.Transform.Scale.y / 2 + transform.Scale.y / 2;

                if (DistanceX < sumHalfWidth && DistanceY < sumHalfHeight)
                {
                    float overlapX = sumHalfWidth - DistanceX;
                    float overlapY = sumHalfHeight - DistanceY;

                    if (overlapX < overlapY)
                    {
                        if (transform.Position.x < barrel.Transform.Position.x)
                            transform.Position = new Vector2(transform.Position.x - overlapX, transform.Position.y);
                        else
                            transform.Position = new Vector2(transform.Position.x + overlapX, transform.Position.y);
                    }
                    else
                    {
                        if (transform.Position.y < barrel.Transform.Position.y)
                            transform.Position = new Vector2(transform.Position.x, transform.Position.y - overlapY);
                        else
                            transform.Position = new Vector2(transform.Position.x, transform.Position.y + overlapY);
                    }

                    break;
                }
            }
        }



        public bool CheckCollision(Bullet bullet)
        {
            float bulletX = bullet.Transform.Position.x;
            float bulletY = bullet.Transform.Position.y;
            float bulletRadius = bullet.Radius;  // Este sí está bien

           
            return bulletX + bulletRadius > transform.Position.x && bulletX - bulletRadius < transform.Position.x + transform.Size.x &&
                   bulletY + bulletRadius > transform.Position.y && bulletY - bulletRadius < transform.Position.y + transform.Size.y;
        }

        private class EnemyMovement
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

                Vector2 direction = player.Transform.Position - transform.Position;

                float distance = (float)Math.Sqrt(direction.x * direction.x + direction.y * direction.y);

                if (distance <= detectionRange && distance > 1f)
                {
                    direction.x /= distance;
                    direction.y /= distance;

                    transform.Translate(direction, speed);
                }
            }

        }
    }
}
