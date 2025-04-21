using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player
    {

        private Image playerImage = Engine.LoadImage("assets/Player.png");
        private PlayerController playerController;
        private Transform transform;
        private int health = 100;
        private Animation currentAnimation;
        private bool isDead = false;       

        public Player(float positionX, float positionY)
        {
            transform = new Transform(new Vector2(positionX, positionY), new Vector2(200, 200));
            playerController = new PlayerController(transform);

            CreateAnimations();
            

        }

        private void CreateAnimations()
        {
            List<Image> images = new List<Image>();

            for (int i = 0; i < 5; i++)
            {
                Image image = Engine.LoadImage($"assets/Player/Move/{i}.png");
                images.Add(image);
            }

            currentAnimation = new Animation("idle", 0.1f, images, true);
        }

        public void Update()
        {
            if (isDead) return;
            playerController.Update();
            currentAnimation.Update();
            CheckCollisionsBarrels();
            CheckCollisionsEnemies();
        }

        private void CheckCollisionsEnemies()
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
        private void CheckCollisionsBarrels()
        {
            for (int i = 0; i < GameManager.Instance.LevelController.BarrelList.Count; i++)
            {
                Barrel barrel = GameManager.Instance.LevelController.BarrelList[i];

                float DistanceX = Math.Abs((barrel.Transform.Position.x + barrel.Transform.Scale.x / 2) - (transform.Position.x + transform.Scale.x / 2));
                float DistanceY = Math.Abs((barrel.Transform.Position.y + barrel.Transform.Scale.y / 2) - (transform.Position.y + transform.Scale.y / 2));

                float sumHalfWidth = barrel.Transform.Scale.x / 2 + transform.Scale.x / 2;
                float sumHalfHeight = barrel.Transform.Scale.y / 2 + transform.Scale.y / 2;

                if (DistanceX < sumHalfWidth && DistanceY < sumHalfHeight)
                {
                    float overlapX = sumHalfWidth - DistanceX;
                    float overlapY = sumHalfHeight - DistanceY;

                    // Corregir la posición del personaje dependiendo de la colisión
                    if (overlapX < overlapY)
                    {
                        // Si la colisión es más horizontal, corregir la posición en el eje X
                        if (transform.Position.x < barrel.Transform.Position.x)
                        {
                            // Ajusta la posición en X para no atravesar el barril
                            transform.Position = new Vector2(transform.Position.x - overlapX, transform.Position.y);
                        }
                        else
                        {
                            // Ajusta la posición en X para no atravesar el barril
                            transform.Position = new Vector2(transform.Position.x + overlapX, transform.Position.y);
                        }
                    }
                    else
                    {
                        // Si la colisión es más vertical, corregir la posición en el eje Y
                        if (transform.Position.y < barrel.Transform.Position.y)
                        {
                            // Ajusta la posición en Y para no atravesar el barril
                            transform.Position = new Vector2(transform.Position.x, transform.Position.y - overlapY);
                        }
                        else
                        {
                            // Ajusta la posición en Y para no atravesar el barril
                            transform.Position = new Vector2(transform.Position.x, transform.Position.y + overlapY);
                        }
                    }

                    // Este paso asegura que el personaje no sigue moviéndose dentro del barril
                    // Por lo tanto, si ya corregiste la posición en uno de los ejes, no se moverá más allá.
                    break;
                }
            }
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
                isDead = true;
                GameManager.Instance.ChangeGameStatus(gameStatus.lose);
            }
        }
    }
}