using MyGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyGame;

public class LevelController
{
    private List<Enemy> enemyList = new List<Enemy>();
    private List<Bullet> activeBulletList = new List<Bullet>();
    private List<Barrel> barrelList = new List<Barrel>();
    private Image fondo = Engine.LoadImage("assets/fondo.png");
    private Player player1;
    private EnemySpawner enemySpawner; // Se encarga de generar enemigos
    private ObjectPool<Bullet> bulletPool; // Pool genérico para las balas
    private int screenWidth = 800;
    private int screenHeight = 600;

    public List<Enemy> EnemyList => enemyList;
    public List<Bullet> BulletList => activeBulletList;
    public List<Barrel> BarrelList => barrelList;
    public Player Player1 => player1;

    public LevelController()
    {
        private List<Enemy> enemyList = new List<Enemy>();
        private List<Bullet> bulletList = new List<Bullet>();
        private List<Barrel> barrelList = new List<Barrel>();
        private Image fondo = Engine.LoadImage("assets/fondo.png");
        static private EnemySpawner enemySpawner;
        private Player player1;
        public List<PowerUp> PowerUpList = new List<PowerUp>();

        public List<Enemy> EnemyList => enemyList;
         public List<Bullet> BulletList => bulletList;
         public List<Barrel> BarrelList => barrelList;
         public Player Player1 => player1;
        InitializeLevel();
    }

    public void InitializeLevel()
    {
        player1 = new Player(200, 200);
        enemySpawner = new EnemySpawner();
        bulletPool = new ObjectPool<Bullet>(() => new Bullet(), 20);

        // Se suscribe al evento de eliminación de enemigos
        GameManager.Instance.OnEnemyDestroyed += RemoveEnemy;

        // Se crean algunos barriles de ejemplo
        barrelList.Add(new Barrel(300, 250));
        barrelList.Add(new Barrel(330, 250));
        barrelList.Add(new Barrel(330, 275));
    }

    private void RemoveEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    public void Update()
    {
        player1.Update();
        enemySpawner.Update(); // Esto se encarga de spawnear enemigos

        // Sincronizamos la lista de enemigos (asumiendo que EnemySpawner agrega a GameManager.Instance.LevelController.EnemyList)
        // O, alternativamente, el EnemySpawner podría agregar directamente a "enemyList"; ajusta según tu implementación.
        // Para este ejemplo, asumiremos que enemySpawner agrega directamente a "enemyList".
        // Si no fuese el caso, podrías hacer: enemyList = GameManager.Instance.LevelController.EnemyList;

        foreach (var enemy in enemyList)
            enemy.Update();

        // Actualizamos las balas activas y revisamos colisiones con enemigos
        for (int i = activeBulletList.Count - 1; i >= 0; i--)
        {
            Bullet bullet = activeBulletList[i];
            bullet.Update();

        public void InitializeLevel()
        {
            enemySpawner = new EnemySpawner();
            player1 = new Player(200, 200);

            PowerUpList.Add(new PowerUp(400, 400, PowerUpType.FastShoot));
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

        public void AddBullet(float x, float y, float dirX, float dirY)
        {
            Bullet bullet = new Bullet(x, y, dirX, dirY);
            bulletList.Add(bullet);
        }

        public bool AllEnemiesEliminated()
        {
            return enemyList.Count == 0;
        }
        public void Update()
        {
            player1.Update();
            enemySpawner.Update();

            foreach (var powerUp in PowerUpList)
            {
                powerUp.Update(GameManager.Instance.Player);
            }

            // Actualizar enemigos
            for (int i = 0; i < enemyList.Count; i++)
            // Si la bala sale de pantalla, la desactivamos y la regresamos al pool
            if (bullet.IsOffScreen(screenWidth, screenHeight))
            {
                bullet.Deactivate();
                bulletPool.ReturnObject(bullet);
                activeBulletList.RemoveAt(i);
            }
            else
            {
                // Revisamos colisión con cada enemigo
                foreach (var enemy in enemyList)
                {
                    if (enemy.CheckCollision(bullet))
                    {
                        enemy.GetDamage(50); // Aplica un daño arbitrario
                        bullet.Deactivate();
                        bulletPool.ReturnObject(bullet);
                        activeBulletList.RemoveAt(i);
                        break;
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

            foreach (var powerUp in PowerUpList)
            {
                powerUp.Render();
            }

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

        public void SpawnPowerUp(float x, float y)
        {
            PowerUpType tipoAleatorio = PowerUpFactory.GetRandomPowerUpType();
            PowerUp powerUp = new PowerUp(x, y, tipoAleatorio);
            PowerUpList.Add(powerUp);
        }
        public void ClearAllEnemies()
        {
            EnemyList.Clear();
        }
        }

        foreach (var barrel in barrelList)
            barrel.Update();
    }

    public void Render()
    {
        Engine.Clear();
        Engine.Draw(fondo, 0, 0);
        player1.Render();

        foreach (var enemy in enemyList)
            enemy.Render();

        foreach (var bullet in activeBulletList)
            bullet.Render();

        foreach (var barrel in barrelList)
            barrel.Render();

        Engine.Show();
    }

    // Método público para disparar una bala, el cual usa el pool
    public void AddBullet(float posX, float posY, float dirX, float dirY)
    {
        SpawnBullet(posX, posY, dirX, dirY);
    }

    private void SpawnBullet(float posX, float posY, float dirX, float dirY)
    {
        Bullet bullet = bulletPool.GetObject();
        bullet.Reset(posX, posY, dirX, dirY);
        activeBulletList.Add(bullet);
    }
}