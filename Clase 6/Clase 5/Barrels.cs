using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Barrel : GameObject
    {
        private static Image barrelImage = Engine.LoadImage("assets/barrel.png");

        public Barrel(float posX, float posY) : base(posX, posY, new Vector2(30, 22))
        {
        }

        public override void Update()
        {
        }

        public override void Render()
        {
            Engine.Draw(barrelImage, Transform.Position.x, Transform.Position.y);
        }
    }
}

