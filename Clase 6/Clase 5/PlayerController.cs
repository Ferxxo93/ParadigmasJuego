using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PlayerController
    {
        private DateTime timeLastShoot;
        private float timebetweetShoot = 0.4f;
        private int speed = 5;

        private Transform tranform;
        private Vector2 shootDirection = new Vector2(0, -1);

        public PlayerController(Transform transform)
        {
            this.tranform = transform;
        }

        public void Update()
        {
            if (Engine.GetKey(Engine.KEY_A))
            {
                tranform.Translate(new Vector2(-1,0), speed);
                shootDirection = new Vector2(-1, 0);
            }

            if (Engine.GetKey(Engine.KEY_D))
            {
                tranform.Translate(new Vector2(1, 0), speed);
                shootDirection = new Vector2(1, 0);
            }

            if (Engine.GetKey(Engine.KEY_W))
            {
                tranform.Translate(new Vector2(0,-1), speed);
                shootDirection = new Vector2(0, -1);

            }

            if (Engine.GetKey(Engine.KEY_S))
            {
                tranform.Translate(new Vector2(0, 1), speed);
                shootDirection = new Vector2(0, 1);
            }
            if (Engine.GetKey(Engine.KEY_K))
            {
                Shoot();

            }
        }
        private void Shoot()
        {
            if ((DateTime.Now - timeLastShoot).TotalSeconds > timebetweetShoot)
            {
                float startX = tranform.Position.x + tranform.Scale.x / 2;
                float startY = tranform.Position.y;
                GameManager.Instance.LevelController.AddBullet(startX, startY, shootDirection.x, shootDirection.y);
                timeLastShoot = DateTime.Now;
            }

        }


    }
}
