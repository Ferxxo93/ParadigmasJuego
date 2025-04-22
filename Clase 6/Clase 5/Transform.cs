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
        private Vector2 scale; // ← Internamente sigue llamándose "scale"

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Scale // ← Sigue existiendo como antes
        {
            get { return scale; }
        }

        public Vector2 Size // ← NUEVO: esta propiedad es solo un alias para scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Transform(Vector2 position, Vector2 scale)
        {
            this.position = position;
            this.scale = scale;
        }

        public void Translate(Vector2 direction, float speed)
        {
            position.x += direction.x * speed;
            position.y += direction.y * speed;
        }
    }
}
