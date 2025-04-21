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
        public Image Image;

        public Barrel(float posX, float posY)
        {
            Transform = new Transform(new Vector2(posX, posY), new Vector2(32, 32));
            Image = Engine.LoadImage("assets/Barrel.png");
        }

        public void Update()
        {

        }

        public void Render()
        {
            Engine.Draw(Image, Transform.Position.x, Transform.Position.y);
        }
    }

}
