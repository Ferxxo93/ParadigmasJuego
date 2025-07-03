using System;
using System.Collections.Generic;

namespace MyGame
{
    public class Player : GameObject, IHealth
    {
        private Image playerImage = Engine.LoadImage("assets/Player.png");
        private PlayerController playerController;
        private int maxHealth = 100;
        private int currentHealth = 100;
        private Animation currentAnimation;
        private bool isDead = false;
        private IShoot shooter;
        private CheckCollisionBarrels checkCollisionBarrels;
        private bool flipX = false;
        private bool movingUp = false;
        private bool movingDown = false;
        private bool isInvincible = false;
        private float invincibilityTimer = 0f;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        // Evento para notificar cuando el jugador es destruido
        public event Action<Player> OnPlayerDestroyed;

        public Player(float positionX, float positionY) : base(positionX, positionY, new Vector2(30, 55))
        {
            playerController = new PlayerController(Transform, this);
            checkCollisionBarrels = new CheckCollisionBarrels(Transform);
            CreateAnimations();
        }

        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        private void CreateAnimations()
        {
            animations["up"] = LoadAnimation("assets/Player/Up/");
            animations["down"] = LoadAnimation("assets/Player/Down/");
            animations["left"] = LoadAnimation("assets/Player/Left/");
            animations["right"] = LoadAnimation("assets/Player/Right/");

            currentAnimation = animations["down"]; // Animación inicial
        }

        private Animation LoadAnimation(string folderPath)
        {
            List<Image> images = new List<Image>();

            for (int i = 0; i < 5; i++)
            {
                Image image = Engine.LoadImage($"{folderPath}{i}.png");
                images.Add(image);
            }

            return new Animation("move", 0.1f, images, true);
        }

        public void SetHealth(int value)
        {
            currentHealth = maxHealth;
        }

        public override void Update()
        {
            if (isDead) return;

            playerController.Update();

            Vector2 velocity = playerController.GetVelocity();

            if (Math.Abs(velocity.x) > Math.Abs(velocity.y))
            {
                if (velocity.x > 0) currentAnimation = animations["right"];
                else if (velocity.x < 0) currentAnimation = animations["left"];
            }
            else if (Math.Abs(velocity.y) > 0)
            {
                if (velocity.y > 0) currentAnimation = animations["down"];
                else if (velocity.y < 0) currentAnimation = animations["up"];
            }

            currentAnimation.Update();
            checkCollisionBarrels.CheckCollisionsBarrels();
            CheckCollisionsEnemies();

            if (isInvincible)
            {
                invincibilityTimer -= Time.DeltaTime;
                if (invincibilityTimer <= 0)
                {
                    isInvincible = false;
                }
            }
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
                    NotifyPlayerDestroyed();
                }
            }
        }

        public void GetDamage(int damage)
        {
            if (isInvincible) return;

            currentHealth -= damage;
            if (currentHealth <= 0)
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

        public void ActivateFastShoot(float duration)
        {
            shooter.ModifyFireRate(0.2f, duration);
        }

        public void ActivateInvincibility(float duration)
        {
            isInvincible = true;
            invincibilityTimer = duration;
        }
        public void SetInvincible(float seconds)
        {
            isInvincible = true;
            invincibilityTimer = seconds;
        }

        public void ActivateBomb()
        {
            GameManager.Instance.LevelController.ClearAllEnemies();
        }

        public void SetFireRateMultiplier(float multiplier)
        {
            playerController.SetFireRateMultiplier(multiplier);

        }
        private void NotifyPlayerDestroyed()
        {
            OnPlayerDestroyed?.Invoke(this);
        }
    }
}