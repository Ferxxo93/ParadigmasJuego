using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGame
{
    public class LevelController
    {
        private List<Enemy> enemyList = new List<Enemy>();
        private List<Bullet> bulletList = new List<Bullet>();
        private List<Barrel> barrelList = new List<Barrel>();
        private Image fondo = Engine.LoadImage("assets/fondo.png");
        static private EnemySpawner enemySpawner;
        private Player player1;

         public List<Enemy> EnemyList => enemyList;
         public List<Bullet> BulletList => bulletList;
         public List<Barrel> BarrelList => barrelList;

        public LevelController()
        {
            InitializeLevel();
        }

        public void InitializeLevel()
        {
            enemySpawner = new EnemySpawner();
            player1 = new Player(200, 200);

            barrelList.Add(new Barrel(300, 250));
            barrelList.Add(new Barrel(330, 250));
            barrelList.Add(new Barrel(330, 275));
            barrelList.Add(new Barrel(300, 275));
            barrelList.Add(new Barrel(600, 250));
            barrelList.Add(new Barrel(630, 250));
            barrelList.Add(new Barrel(630, 275));
            barrelList.Add(new Barrel(600, 275));
            barrelList.Add(new Barrel(300, 500));
            barrelList.Add(new Barrel(330, 500));
            barrelList.Add(new Barrel(330, 525));
            barrelList.Add(new Barrel(300, 525));
            barrelList.Add(new Barrel(600, 500));
            barrelList.Add(new Barrel(630, 500));
            barrelList.Add(new Barrel(630, 525));
            barrelList.Add(new Barrel(600, 525));

        }

        public bool AllEnemiesEliminated()
        {
            return enemyList.Count == 0;
        }
        public void Update()
        {
            player1.Update();
            enemySpawner.Update();
            // Actualizar enemigos
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Update();

                // Eliminar enemigo si colisiona con una bala (ejemplo de eliminación)
                for (int j = 0; j < bulletList.Count; j++)
                {
                    if (enemyList[i].CheckCollision(bulletList[j])) // Verifica colisión entre enemigo y bala
                    {
                        enemyList.RemoveAt(i); // Elimina el enemigo
                        bulletList.RemoveAt(j); // Elimina la bala
                        i--; // Ajusta el índice para no omitir elementos
                        break; // Sale del bucle de balas una vez que el enemigo ha sido eliminado
                    }
                }
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
            }

            // Actualizar barriles
            for (int i = 0; i < barrelList.Count; i++)
            {
                barrelList[i].Update();
            }
        }

        public  void Render()
        {
            Engine.Clear();
            Engine.Draw(fondo, 0, 0);
            player1.Render();

            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Render();
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Render();
            }

            for (int i =0; i < barrelList.Count; i++)
            {
                barrelList[i].Render();
            }
            Engine.Show();
        }

         public void AddBullet(float posX, float posY, float dirX, float dirY)
        {
            bulletList.Add(new Bullet(posX, posY, dirX, dirY));
        }

    }
}
