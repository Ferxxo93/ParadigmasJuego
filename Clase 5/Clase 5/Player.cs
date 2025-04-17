using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player
    {
        

        private PlayerController playerController;
        private Transform transform;

        private Animation currentAnimation;
        

        public Player(float positionX, float positionY)
        {
            transform = new Transform(new Vector2(positionX, positionY), new Vector2(100, 100));
            playerController = new PlayerController(transform);

            CreateAnimations();

        }

        private void CreateAnimations()
        {
            List<Image> images = new List<Image>();

            for (int i = 0; i < 4; i++)
            {
                Image image = Engine.LoadImage($"assets/Player/{i}.png");
                images.Add(image);
            }

            currentAnimation = new Animation("idle", 0.1f, images, true);
        }

        public void Update()
        {
            playerController.Update();
            currentAnimation.Update();
        }

        

        public void Render()
        {
            Engine.Draw(currentAnimation.CurrentImage, transform.Position.x, transform.Position.y);
        }
    }
}

// PascalCase  => Clases, métodos
// camelCase   => atributos
