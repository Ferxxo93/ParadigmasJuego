using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Net.NetworkInformation;
using Tao.Sdl;



namespace MyGame
{

    class Program
    {
        static private List<Enemy> enemyList = new List<Enemy>();
        static private List<Bullet> bulletList = new List<Bullet>();
        static private Image fondo = Engine.LoadImage("assets/fondo.png");
        static private Player player1;
        static private float deltaTime;
        static private DateTime initialTime;
        static private float timeLastFrame;

        static public List<Enemy> EnemyList => enemyList;
        static public List<Bullet> BulletList => bulletList;

        static public float DeltaTime => deltaTime;

        static void Main(string[] args)
        {
            Engine.Initialize();
            InitializeLevel();

            initialTime = DateTime.Now;

            while (true)
            {
                float currentTime = (float) (DateTime.Now - initialTime).TotalSeconds;
                deltaTime = currentTime - timeLastFrame;
                timeLastFrame = currentTime;

                Update();
                Render();
            }

        }

        static public void InitializeLevel()
        {
            player1 = new Player(0, 0);
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

        static void Update()
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
        static void Render()
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

        static public void AddBullet(float posX, float posY)
        {
            bulletList.Add(new Bullet(posX + 48,posY));
        }
    }
}