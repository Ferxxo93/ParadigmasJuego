using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Bullet
    {
        private Transform transform;
        private Vector2 direction;
        private float speed = 500f;
        private float radius = 5f;
        private Image bulletImage = Engine.LoadImage("assets/bullet.png");

        public bool Active { get; private set; }

        public Transform Transform => transform;
        public float Radius => radius;

        public Bullet()
        {
            transform = new Transform(new Vector2(0, 0), new Vector2(radius * 2, radius * 2));
            Active = false;
        }

        // Reinicializa la bala para su reutilización
        public void Reset(float x, float y, float dirX, float dirY)
        {
            transform.Position = new Vector2(x, y);
            direction = new Vector2(dirX, dirY);
            Active = true;
        }

        public void Update()
        {
            if (!Active) return;

            Vector2 pos = transform.Position;
            pos.x += direction.x * speed * Time.DeltaTime;
            pos.y += direction.y * speed * Time.DeltaTime;
            transform.Position = pos;
        }

        public void Render()
        {
            if (!Active) return;
            Engine.Draw(bulletImage, transform.Position.x, transform.Position.y);
        }

        // Verifica si la bala se ha salido de la pantalla
        public bool IsOffScreen(int screenWidth, int screenHeight)
        {
            return transform.Position.x < -radius ||
                   transform.Position.x > screenWidth + radius ||
                   transform.Position.y < -radius ||
                   transform.Position.y > screenHeight + radius;
        }

        public void Deactivate()
        {
            Active = false;
        }
    }
}

