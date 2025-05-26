using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public abstract class GameObject
    {
        public Transform Transform { get; protected set; }

        public GameObject(float posX, float posY, Vector2 size)
        {
            Transform = new Transform(new Vector2(posX, posY), size);
        }

        public abstract void Update();
        public abstract void Render();
    }
}
