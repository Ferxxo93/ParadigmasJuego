using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LevelController
    {
         private List<Enemy> enemyList = new List<Enemy>();
         private List<Bullet> bulletList = new List<Bullet>();
         private Image fondo = Engine.LoadImage("assets/fondo.png");
         private Player player1;

         public List<Enemy> EnemyList => enemyList;
         public List<Bullet> BulletList => bulletList;

        public LevelController()
        {
            InitializeLevel();
        }

        public void InitializeLevel()
        {
            player1 = new Player(400, 400);
            enemyList.Add(new Enemy(0, 0));
            enemyList.Add(new Enemy(0, 200));
            enemyList.Add(new Enemy(0, 400));
            enemyList.Add(new Enemy(100, 0));
            enemyList.Add(new Enemy(100, 200));
            enemyList.Add(new Enemy(100, 400));
            enemyList.Add(new Enemy(200, 0));
            enemyList.Add(new Enemy(200, 200));
            enemyList.Add(new Enemy(200, 400));

        }

        public  void Update()
        {

            player1.Update();

            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Update();
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
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
            Engine.Show();
        }

         public void AddBullet(float posX, float posY)
        {
            bulletList.Add(new Bullet(posX + 48, posY));
        }

    }
}
