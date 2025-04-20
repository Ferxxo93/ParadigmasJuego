using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player
    {
        

        private PlayerController playerController;
        private Transform transform;
        private int health = 100;
        private Animation currentAnimation;
        private bool isDead = false;       

        public Player(float positionX, float positionY)
        {
            transform = new Transform(new Vector2(positionX, positionY), new Vector2(100, 100));
            playerController = new PlayerController(transform);

            CreateAnimations();
            

        }

        private void CreateAnimations()
        {
            List<Image> images = new List<Image>();

            for (int i = 0; i < 4; i++)
            {
                Image image = Engine.LoadImage($"assets/player/{i}.png");
                images.Add(image);
            }

            currentAnimation = new Animation("idle", 0.1f, images, true);
        }

        public void Update()
        {
            if (isDead) return;
            playerController.Update();
            currentAnimation.Update();
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            for (int i = 0; i < GameManager.Instance.LevelController.EnemyList.Count; i++)
            {
                Enemy enemy = GameManager.Instance.LevelController.EnemyList[i];

                float DistanceX = Math.Abs((enemy.Transform.Position.x + enemy.Transform.Scale.x / 2) - (transform.Position.x + transform.Scale.x / 2));
                float DistanceY = Math.Abs((enemy.Transform.Position.y + enemy.Transform.Scale.y / 2) - (transform.Position.y + transform.Scale.y / 2));

                float sumHalfWidth = enemy.Transform.Scale.x / 2 + transform.Scale.x / 2;
                float sumHalfHeight = enemy.Transform.Scale.y / 2 + transform.Scale.y / 2;

                if (DistanceX < sumHalfWidth && DistanceY < sumHalfHeight)
                {
                    GameManager.Instance.ChangeGameStatus(gameStatus.lose);
                }
            }
        }

        public void Render()
        {
            if (isDead)
            Engine.Draw(currentAnimation.CurrentImage, transform.Position.x, transform.Position.y);
        }

        public void GetDamage(int damage)
        {
            health -= damage;

            if (health < 0)
            {
                isDead = true;
                GameManager.Instance.ChangeGameStatus(gameStatus.lose);
            }
        }
    }
}

// PascalCase  => Clases, métodos
// camelCase   => atributos
