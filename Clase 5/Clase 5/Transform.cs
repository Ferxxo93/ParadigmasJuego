using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Transform
    {

        private Vector2 position;

        public Vector2 Position => position;

        private Vector2 scale;

        public Vector2 Scale => scale;

        public Transform(Vector2 position, Vector2 scale) 
        { 
            this.position = position;
            this.scale = scale;
        }

        public void Translate(Vector2 direction, int speed)
        {
            position.x += direction.x * speed;
            position.y += direction.y * speed;
        }
    }
}
