using System;
using System.Threading.Tasks;
using MyGame;

namespace MyGame
{
    public class PlayerController : IShoot
    {
        private DateTime timeLastShoot;
        private float timeBetweenShoot = 0.4f;
        private int speed = 20000;
        private float defaultFireRate = 0.4f;
        private float fireRateMultiplier = 1f;

        private Transform transform;
        private Vector2 shootDirection = new Vector2(0, -1);

        private Player player;

        private Vector2 lastVelocity = new Vector2(0, 0);

        public PlayerController(Transform transform, Player player)
        {
            this.transform = transform;
            this.player = player;
        }

        public void Update()
        {
            bool movedLeft = false;
            bool movedRight = false;
            bool movedUp = false;
            bool movedDown = false;
            Vector2 position = transform.Position;
            Vector2 scale = transform.Scale;

            Vector2 input = new Vector2(0, 0); // Para calcular la dirección final

            if (Engine.GetKey(Engine.KEY_A) && position.x > 0)
            {
                input.x -= 1;
                shootDirection = new Vector2(-1, 0);
                movedLeft = true;
            }

            if (Engine.GetKey(Engine.KEY_D) && position.x < 1000)
            {
                input.x += 1;
                shootDirection = new Vector2(1, 0);
                movedRight = true;
            }

            if (Engine.GetKey(Engine.KEY_W) && position.y > 0)
            {
                input.y -= 1;
                shootDirection = new Vector2(0, -1);
                movedUp = true;
            }

            if (Engine.GetKey(Engine.KEY_S) && position.y < 710)
            {
                input.y += 1;
                shootDirection = new Vector2(0, 1);
                movedDown = true;
            }

            if (input.x != 0 || input.y != 0)
            {
                input = Normalize(input);
                transform.Translate(input, speed * Time.DeltaTime);
                lastVelocity = input; // 🔁 Guardar la dirección actual
            }
            else
            {
                lastVelocity = new Vector2(0, 0); // 🔁 Detenido
            }

            // Flip visual solo para izquierda/derecha
            if (movedLeft)
                player.SetFlip(true);
            else if (movedRight)
                player.SetFlip(false);

            // Marcar si se está moviendo verticalmente (por si querés usarlo visualmente)
            player.SetVerticalDirection(movedUp, movedDown);

            if (Engine.GetKey(Engine.KEY_K))
                Shoot();
        }

        public void Shoot()
        {
            if ((DateTime.Now - timeLastShoot).TotalSeconds > timeBetweenShoot * fireRateMultiplier)
            {
                float startX = transform.Position.x;
                float startY = transform.Position.y;
                GameManager.Instance.LevelController.AddBullet(startX, startY, shootDirection.x, shootDirection.y);
                timeLastShoot = DateTime.Now;
            }
        }

        public void ModifyFireRate(float newRate, float duration)
        {
            timeBetweenShoot = newRate;
            Task.Run(async () =>
            {
                await Task.Delay((int)(duration * 1000));
                timeBetweenShoot = defaultFireRate;
            });
        }

        public void SetFireRateMultiplier(float multiplier)
        {
            fireRateMultiplier = multiplier;
        }

        // 🔁 Nuevo método para que Player pueda consultar dirección actual
        public Vector2 GetVelocity()
        {
            return lastVelocity;
        }

        // 🔁 Normaliza vector (evita moverse más rápido en diagonal)
        private Vector2 Normalize(Vector2 v)
        {
            float length = (float)Math.Sqrt(v.x * v.x + v.y * v.y);
            return length == 0 ? new Vector2(0, 0) : new Vector2(v.x / length, v.y / length);
        }
    }
}
