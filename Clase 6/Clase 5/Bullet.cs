using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Bullet
    {
        private Image bulletImage = Engine.LoadImage("assets/bullet.png");

        private float posX = 0;
        private float posY = 0;

        private float scaleX = 4;
        private float scaleY = 10;

        private float speed = 2;

        public Bullet(float positionX, float positionY)
        {
            posX = positionX;
            posY = positionY;
        }

        public void Update()
        {
            posY -= speed;
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            for (int i = 0; i < GameManager.Instance.LevelController.EnemyList.Count; i++)
            {
                Enemy enemy = GameManager.Instance.LevelController.EnemyList[i];

                float DistanceX = Math.Abs((enemy.Transform.Position.x + enemy.Transform.Scale.x /2) - posX + (scaleX /2));
                float DistanceY = Math.Abs((enemy.Transform.Position.y + enemy.Transform.Scale.y / 2) - posY + (scaleY / 2));

                float sumHalfWidth = enemy.Transform.Scale.x / 2 + scaleX / 2;
                float sumHalfHeight = enemy.Transform.Scale.y / 2 + scaleY / 2;

                if (DistanceX < sumHalfWidth && DistanceY < sumHalfHeight)
                {
                    //Program.EnemyList.Remove(enemy);
                    enemy.GetDamage(60);
                    GameManager.Instance.LevelController.BulletList.Remove(this);
                }
            }
        }

        public void Render()
        {
            Engine.Draw(bulletImage, posX, posY);
        }
    }
}
