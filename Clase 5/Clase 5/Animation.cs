using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Animation
    {
        private string name;
        private float speed;
        private List<Image> images = new List<Image>();
        private bool isLoop;

        private float currentAnimationTime;
        private int currentFrame;

        public Image CurrentImage => images[currentFrame];

        public Animation(string name, float speed, List<Image> images, bool isLoop)
        {
            this.name = name;
            this.speed = speed;
            this.images = images;
            this.isLoop = isLoop;
        }

        public void Update()
        {
            currentAnimationTime += Program.DeltaTime;

            if(currentAnimationTime > speed)
            {
                currentFrame++;
                currentAnimationTime = 0;

                if (currentFrame > images.Count-1)
                {
                    if (isLoop)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame--;
                    }

                }

            }
        }
    }
}
