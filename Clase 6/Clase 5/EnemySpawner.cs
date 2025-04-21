using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EnemySpawner
    {
        private Random random = new Random();
        private float spawnInterval = 2f;
        private float lastSpawnTime = 0;

        private int screenWidth = 800;
        private int screenHeight = 600;

        public void Update()
        {
            if ((float)(DateTime.Now - Time.initialTime).TotalSeconds - lastSpawnTime > spawnInterval)
            {
                SpawnEnemy();
                lastSpawnTime = (float)(DateTime.Now - Time.initialTime).TotalSeconds;
            }
        }

        private void SpawnEnemy()
        {
            int side = random.Next(4); // 0: top, 1: bottom, 2: left, 3: right
            float x = 0;
            float y = 0;

            switch (side)
            {
                case 0: // Top
                    x = random.Next(0, screenWidth);
                    y = -30;
                    break;
                case 1: // Bottom
                    x = random.Next(0, screenWidth);
                    y = screenHeight + 30;
                    break;
                case 2: // Left
                    x = -30;
                    y = random.Next(0, screenHeight);
                    break;
                case 3: // Right
                    x = screenWidth + 30;
                    y = random.Next(0, screenHeight);
                    break;
            }

            GameManager.Instance.LevelController.EnemyList.Add(new Enemy(x, y));
        }
    }
}
