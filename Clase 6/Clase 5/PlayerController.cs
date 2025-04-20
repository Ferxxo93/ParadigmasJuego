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
        private float timebetweetShoot = 1;
        private int speed = 1;

        private Transform tranform;

        public PlayerController(Transform transform)
        {
            this.tranform = transform;
        }

        public void Update()
        {
            if (Engine.GetKey(Engine.KEY_A))
            {
                tranform.Translate(new Vector2(-1,0), speed);
            }

            if (Engine.GetKey(Engine.KEY_D))
            {
                tranform.Translate(new Vector2(1, 0), speed);
            }

            if (Engine.GetKey(Engine.KEY_W))
            {
                tranform.Translate(new Vector2(0,-1), speed);
            }

            if (Engine.GetKey(Engine.KEY_S))
            {
                tranform.Translate(new Vector2(0, 1), speed);
            }
            if (Engine.GetKey(Engine.KEY_ESP))
            {
                Shoot();

            }
        }
        private void Shoot()
        {
            if ((DateTime.Now - timeLastShoot).TotalSeconds > timebetweetShoot)
            {
                GameManager.Instance.LevelController.AddBullet(tranform.Position.x, tranform.Position.y);
                timeLastShoot = DateTime.Now;
            }

        }


    }
}
