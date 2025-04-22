using System;
using System.Collections.Generic;

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

        private bool flipX = false;
        private bool movingUp = false;
        private bool movingDown = false;

        public Transform Transform => transform;

        public Player(float positionX, float positionY)
        {
            transform = new Transform(new Vector2(positionX, positionY), new Vector2(30, 55));
            playerController = new PlayerController(transform,this);
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

        public void SetFlip(bool flip)
        {
            flipX = flip;
        }

        public void SetVerticalDirection(bool up, bool down)
        {
            movingUp = up;
            movingDown = down;
        }


        public void Render()
        {
            Engine.Draw(currentAnimation.CurrentImage, transform.Position.x, transform.Position.y, flipX, false);
        }

        private void CheckCollisionsEnemies()
        {
            foreach (var enemy in GameManager.Instance.LevelController.EnemyList)
            {
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
