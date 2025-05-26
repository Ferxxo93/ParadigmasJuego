using System;
using System.Threading.Tasks;
using MyGame;

namespace MyGame
{
    public class PlayerController
    {
        private DateTime timeLastShoot;
        private float timeBetweenShoot = 0.4f;
        private int speed = 5;
        private float defaultFireRate = 0.4f;
        private float fireRateMultiplier = 1f;

        private Transform transform;
        private Vector2 shootDirection = new Vector2(0, -1);

        private Player player;
        
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

            if (Engine.GetKey(Engine.KEY_A) && position.x > 0)
            {
                transform.Translate(new Vector2(-1, 0), speed);
                shootDirection = new Vector2(-1, 0);
                movedLeft = true;
            }

            if (Engine.GetKey(Engine.KEY_D) && position.x < 1000)
            {
                transform.Translate(new Vector2(1, 0), speed);
                shootDirection = new Vector2(1, 0);
                movedRight = true;
            }

            if (Engine.GetKey(Engine.KEY_W) && position.y > 0)
            {
                transform.Translate(new Vector2(0, -1), speed);
                shootDirection = new Vector2(0, -1);
                movedUp = true;
            }

            if (Engine.GetKey(Engine.KEY_S) && position.y < 710)
            {
                transform.Translate(new Vector2(0, 1), speed);
                shootDirection = new Vector2(0, 1);
                movedDown = true;
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

        private void Shoot()
        {
            if ((DateTime.Now - timeLastShoot).TotalSeconds > timeBetweenShoot/ fireRateMultiplier)
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
    }
}
