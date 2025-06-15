using MyGame;
using System;
using System.Collections.Generic;


namespace MyGame
{
    public class BigEnemy : Enemy, IHealth
    {
        private BigEnemyMovement bigEnemyMovement;
        private static readonly Vector2 BigEnemySize = new Vector2(60, 44);
        public BigEnemy(float positionX, float positionY) : base(positionX, positionY)
        {
            Transform.Size = BigEnemySize;
            CreateAnimation();
            bigEnemyMovement = new BigEnemyMovement(Transform);
        }

        private void CreateAnimation()
        {
            List<Image> images = new List<Image>();

            for (int i = 5; i < 9; i++)
            {
                Image image = Engine.LoadImage($"assets/BigEnemy/Idle/{i}.png");
                images.Add(image);
            }

            currentAnimation = new Animation("idle", 0.1f, images, true);
        }

        public override void Update()
        {
            bigEnemyMovement.Update();
            base.currentAnimation.Update();
            CheckCollisionsBarrels();
        }

        public override void Render()
        {
            Engine.Draw(currentAnimation.CurrentImage, Transform.Position.x, Transform.Position.y);
        }
    }
}