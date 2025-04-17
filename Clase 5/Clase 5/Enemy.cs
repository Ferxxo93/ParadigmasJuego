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
            transform = new Transform(new Vector2(positionX, positionY), new Vector2(60,60));
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

        public void GetDamage(int damage)
        {
            health -= damage;

            if (health < 0)
            {
                Program.EnemyList.Remove(this);
            }
        }
    }
}