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
        private float speed = 500f; // Velocidad de la bala
        private float radius = 5f; // Radio de la bala
        private Image bulletImage = Engine.LoadImage("assets/bullet.png");

        public Transform Transform => transform;
        public float Radius => radius;

        public Bullet(float x, float y, float dirX, float dirY)
        {
            transform = new Transform(new Vector2(x, y), new Vector2(radius * 2, radius * 2)); // Tamaño visual de la bala
            direction = new Vector2(dirX, dirY);
        }

        public void Update()
        {
            Vector2 pos = transform.Position; // Copiamos la posición actual
            pos.x += direction.x * speed * Time.DeltaTime;
            pos.y += direction.y * speed * Time.DeltaTime;
            transform.Position = pos; // Volvemos a asignarla
            
        }


        public void Render()
        {
            Engine.Draw(bulletImage, transform.Position.x, transform.Position.y);
        }
    }
}

