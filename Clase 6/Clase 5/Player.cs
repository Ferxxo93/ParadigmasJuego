using System;
using System.Collections.Generic;

namespace MyGame
{
    public class Player : GameObject
    {
        private Image playerImage = Engine.LoadImage("assets/Player.png");
        private PlayerController playerController;
        private int health = 100;
        private Animation currentAnimation;
        private bool isDead = false;

        private bool flipX = false;
        private bool movingUp = false;
        private bool movingDown = false;

        // Evento para notificar cuando el jugador es destruido
        public event Action<Player> OnPlayerDestroyed;

        public Player(float positionX, float positionY) : base(positionX, positionY, new Vector2(30, 55))
        {
            playerController = new PlayerController(Transform, this);
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

        public override void Update()
        {
            if (isDead) return;

            playerController.Update();
            currentAnimation.Update();
            CheckCollisionsBarrels();
            CheckCollisionsEnemies();
        }

        public override void Render()
        {
            Engine.Draw(currentAnimation.CurrentImage, Transform.Position.x, Transform.Position.y);
        }

        private void CheckCollisionsEnemies()
        {
            foreach (var enemy in GameManager.Instance.LevelController.EnemyList)
            {
                float DistanceX = Math.Abs((enemy.Transform.Position.x + enemy.Transform.Scale.x / 2) - (Transform.Position.x + Transform.Scale.x / 2));
                float DistanceY = Math.Abs((enemy.Transform.Position.y + enemy.Transform.Scale.y / 2) - (Transform.Position.y + Transform.Scale.y / 2));

                float sumHalfWidth = enemy.Transform.Scale.x / 2 + Transform.Scale.x / 2;
                float sumHalfHeight = enemy.Transform.Scale.y / 2 + Transform.Scale.y / 2;

                if (DistanceX < sumHalfWidth && DistanceY < sumHalfHeight)
                {
                    GameManager.Instance.ChangeGameStatus(gameStatus.lose);
                    NotifyPlayerDestroyed(); // Activa el evento cuando el jugador pierde
                }
            }
        }

        public void GetDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                isDead = true;
                GameManager.Instance.ChangeGameStatus(gameStatus.lose);
                NotifyPlayerDestroyed(); // Activa el evento cuando el jugador muere
            }
        }

        public void SetFlip(bool flip)
        {
            flipX = flip;
        }

        public void SetVerticalDirection(bool up, bool down)
        {
            movingUp = up;
            movingDown = down;
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

        private void NotifyPlayerDestroyed()
        {
            OnPlayerDestroyed?.Invoke(this);
        }
    }
}