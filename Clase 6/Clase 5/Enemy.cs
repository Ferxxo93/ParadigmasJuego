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

        private int health = 100;

        public Transform Transform => transform;

        public Enemy(float positionX, float positionY)
        {
            transform = new Transform(new Vector2(positionX, positionY), new Vector2(5, 5));  // Tamaño del enemigo
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
        }

        public void Render()
        {
            Engine.Draw(currentAnimation.CurrentImage, transform.Position.x, transform.Position.y);
        }

        // Método para recibir daño
        public void GetDamage(int damage)
        {
            health -= damage;

            if (health < 0)
            {
                GameManager.Instance.LevelController.EnemyList.Remove(this);
            }
        }

        // Método para verificar colisiones con balas
        public bool CheckCollision(Bullet bullet)
        {
            float bulletX = bullet.Transform.Position.x;
            float bulletY = bullet.Transform.Position.y;
            float bulletRadius = bullet.Radius;  // Este sí está bien

            // Comprobar si el círculo de la bala se superpone con el rectángulo del enemigo
            return bulletX + bulletRadius > transform.Position.x && bulletX - bulletRadius < transform.Position.x + transform.Size.x &&
                   bulletY + bulletRadius > transform.Position.y && bulletY - bulletRadius < transform.Position.y + transform.Size.y;
        }

    }
}
