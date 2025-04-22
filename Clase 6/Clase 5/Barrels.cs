using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Barrel
    {
        public Transform Transform;
        private static Image barrelImage = Engine.LoadImage("assets/barrel.png");

        public Barrel(float posX, float posY)
        {
            Transform = new Transform(new Vector2(posX, posY), new Vector2(30, 22));
        }

        public void Update()
        {
        }

        public void Render()
        {
            Engine.Draw(barrelImage, Transform.Position.x, Transform.Position.y);
        }

    }
}

