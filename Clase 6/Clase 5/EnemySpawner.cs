using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EnemySpawner
    {
        private Random random = new Random();
        private float spawnInterval = 2f;
        private float lastSpawnTime = 0;
        private int maxEnemies = 10; // Límite de enemigos simultáneos
        private Stopwatch stopwatch = new Stopwatch();

        private int screenWidth = 800;
        private int screenHeight = 600;

        public EnemySpawner()
        {
            stopwatch.Start();

            // Suscribirse al evento de eliminación de enemigos
            GameManager.Instance.OnEnemyDestroyed += HandleEnemyDestroyed;
        }

        public void Update()
        {
            if (GameManager.Instance.LevelController.EnemyList.Count >= maxEnemies)
                return; // No generar más enemigos si se alcanzó el límite

            float currentTime = (float)stopwatch.Elapsed.TotalSeconds;

            if (currentTime - lastSpawnTime > spawnInterval)
            {
                SpawnEnemy();
                lastSpawnTime = currentTime;
            }
        }

        private void SpawnEnemy()
        {
            int side = random.Next(4); // 0: top, 1: bottom, 2: left, 3: right
            float x = 0;
            float y = 0;

            switch (side)
            {
                case 0: x = random.Next(0, screenWidth); y = -30; break;
                case 1: x = random.Next(0, screenWidth); y = screenHeight + 30; break;
                case 2: x = -30; y = random.Next(0, screenHeight); break;
                case 3: x = screenWidth + 30; y = random.Next(0, screenHeight); break;
            }

            Enemy newEnemy;
            if (random.NextDouble() < 0.3)
            {
                newEnemy = new BigEnemy(x, y);
                Console.WriteLine($"BigEnemy generado en ({x}, {y})");
            }
            else
            {
                newEnemy = new Enemy(x, y);
                Console.WriteLine($"Enemy generado en ({x}, {y})");
            }

            GameManager.Instance.LevelController.EnemyList.Add(newEnemy);
            Console.WriteLine($"Enemigo generado en ({x}, {y})");
        }

        private void HandleEnemyDestroyed(Enemy enemy)
        {
            Console.WriteLine($"Enemigo eliminado: {enemy}. Ajustando spawn.");
            spawnInterval *= 0.95f; // Reducir ligeramente el intervalo de spawn para aumentar dificultad
        }
    }
}