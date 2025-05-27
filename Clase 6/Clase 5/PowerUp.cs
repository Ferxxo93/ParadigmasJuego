using System;
using System.Collections.Generic;
using MyGame;

namespace MyGame
{
    public class FastShootPowerUp : IPowerUp
    {
        private Vector2 position;
        private bool active = true;
        private Image image = Engine.LoadImage("assets/powerups/fastshoot.png");

        public bool IsActive => active;

        public void Apply(Player player)
        {
            player.SetFireRateMultiplier(0.5f); // Dispara el doble de rápido
            active = false;
        }

        public void Update() { }

        public void Render()
        {
            if (active)
                Engine.Draw(image, position.x, position.y);
        }

        public void SetPosition(float x, float y)
        {
            position = new Vector2(x, y);
        }

        public Vector2 GetPosition() => position;
    }

    public class InvincibilityPowerUp : IPowerUp
    {
        private Vector2 position;
        private bool active = true;
        private Image image = Engine.LoadImage("assets/powerups/shield.png");

        public bool IsActive => active;

        public void Apply(Player player)
        {
            player.SetInvincible(3.0f); // 3 segundos de invencibilidad
            active = false;
        }

        public void Update() { }

        public void Render()
        {
            if (active)
                Engine.Draw(image, position.x, position.y);
        }

        public void SetPosition(float x, float y)
        {
            position = new Vector2(x, y);
        }

        public Vector2 GetPosition() => position;
    }

    public class BombPowerUp : IPowerUp
    {
        private Vector2 position;
        private bool active = true;
        private Image image = Engine.LoadImage("assets/powerups/bomb.png");

        public bool IsActive => active;

        public void Apply(Player player)
        {
            GameManager.Instance.LevelController.ClearAllEnemies();
            active = false;
        }

        public void Update() { }

        public void Render()
        {
            if (active)
                Engine.Draw(image, position.x, position.y);
        }

        public void SetPosition(float x, float y)
        {
            position = new Vector2(x, y);
        }

        public Vector2 GetPosition() => position;
    }

    public enum PowerUpType
    {
        FastShoot,
        Invincibility,
        Bomb
    }

    public static class PowerUpFactory
    {
        public static PowerUpType GetRandomPowerUpType()
        {
            Array values = Enum.GetValues(typeof(PowerUpType));
            Random random = new Random();
            return (PowerUpType)values.GetValue(random.Next(values.Length));
        }
        public static IPowerUp CreatePowerUp(PowerUpType type)
        {
            switch (type)
            {
                case PowerUpType.FastShoot: return new FastShootPowerUp();
                case PowerUpType.Invincibility: return new InvincibilityPowerUp();
                case PowerUpType.Bomb: return new BombPowerUp();
                default: throw new Exception("Unknown PowerUpType");
            }
        }
        public static IPowerUp CreateRandomPowerUp()
        {
            Array values = Enum.GetValues(typeof(PowerUpType));
            Random rand = new Random();
            PowerUpType randomType = (PowerUpType)values.GetValue(rand.Next(values.Length));
            return CreatePowerUp(randomType);
        }
    }

    public class PowerUp
    {
        private Transform transform;
        private IPowerUp effect;
        private Image image;
        private float radius = 30f;

        public bool Collected { get; private set; }
        public Transform Transform => transform;

        public PowerUp(float x, float y, PowerUpType type)
        {
            transform = new Transform(new Vector2(x, y), new Vector2(radius * 2, radius * 2));
            effect = PowerUpFactory.CreatePowerUp(type);
            image = Engine.LoadImage($"assets/powerups/{type}.png");
        }

        public void Update(Player player)
        {
            if (Collected) return;

            float distX = Math.Abs(player.Transform.Position.x + player.Transform.Scale.x / 2 - (transform.Position.x + radius));
            float distY = Math.Abs(player.Transform.Position.y + player.Transform.Scale.y / 2 - (transform.Position.y + radius));

            float sumHalfWidth = player.Transform.Scale.x / 2 + radius;
            float sumHalfHeight = player.Transform.Scale.y / 2 + radius;

            if (distX < sumHalfWidth && distY < sumHalfHeight)
            {
                Collected = true;
                effect.Apply(player);
            }
        }

        public void Render()
        {
            if (!Collected)
                Engine.Draw(image, transform.Position.x, transform.Position.y);
        }
    }
}
