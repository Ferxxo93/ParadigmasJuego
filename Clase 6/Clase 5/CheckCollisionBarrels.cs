using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class CheckCollisionBarrels
    {
        private Transform transform;
        public CheckCollisionBarrels(Transform transform)
        {
            this.transform = transform;
        }
        public bool CheckCollisionsBarrels()
        {
            foreach (var barrel in GameManager.Instance.LevelController.BarrelList)
            {
                float DistanceX = Math.Abs((barrel.Transform.Position.x + barrel.Transform.Scale.x / 2) - (transform.Position.x + transform.Scale.x / 2));
                float DistanceY = Math.Abs((barrel.Transform.Position.y + barrel.Transform.Scale.y / 2) - (transform.Position.y + transform.Scale.y / 2));

                float sumHalfWidth = barrel.Transform.Scale.x / 2 + transform.Scale.x / 2;
                float sumHalfHeight = barrel.Transform.Scale.y / 2 + transform.Scale.y / 2;

                if (DistanceX < sumHalfWidth && DistanceY < sumHalfHeight)
                {
                    float overlapX = sumHalfWidth - DistanceX;
                    float overlapY = sumHalfHeight - DistanceY;

                    if (overlapX < overlapY)
                    {
                        if (transform.Position.x < barrel.Transform.Position.x)
                            transform.Position = new Vector2(transform.Position.x - overlapX, transform.Position.y);
                        else
                            transform.Position = new Vector2(transform.Position.x + overlapX, transform.Position.y);
                    }
                    else
                    {
                        if (transform.Position.y < barrel.Transform.Position.y)
                            transform.Position = new Vector2(transform.Position.x, transform.Position.y - overlapY);
                        else
                            transform.Position = new Vector2(transform.Position.x, transform.Position.y + overlapY);
                    }

                    return true;
                }
            }
            return false;
        }
    }
}
