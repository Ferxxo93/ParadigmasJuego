using MyGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
public class LevelController
{
    private List<Enemy> enemyList = new List<Enemy>();
    private List<Bullet> activeBulletList = new List<Bullet>();
    private List<Barrel> barrelList = new List<Barrel>();
    private List<PowerUp> PowerUpList = new List<PowerUp>();
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
        InitializeLevel();
    }

    public void InitializeLevel()
    {
        player1 = new Player(200, 200);
        enemySpawner = new EnemySpawner();
        bulletPool = new ObjectPool<Bullet>(() => new Bullet(), 20);

        // Se suscribe al evento de eliminación de enemigos
        GameManager.Instance.OnEnemyDestroyed += RemoveEnemy;


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
        barrelList.Add(new Barrel(330, 530));
        barrelList.Add(new Barrel(300, 530));

        barrelList.Add(new Barrel(600, 500));
        barrelList.Add(new Barrel(630, 500));
        barrelList.Add(new Barrel(630, 530));
        barrelList.Add(new Barrel(600, 530));
    }

    private void RemoveEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    public void Update()
    {
        player1.Update();
        enemySpawner.Update(); // Esto se encarga de spawnear enemigos

        foreach (var enemy in enemyList)
            enemy.Update();

        // Actualiza las balas activas y revisamos colisiones con enemigos
        for (int i = activeBulletList.Count - 1; i >= 0; i--)
        {
            Bullet bullet = activeBulletList[i];
            bullet.Update();

            // Si la bala sale de pantalla, la desactiva y la regresa al pool
            if (bullet.IsOffScreen(screenWidth, screenHeight))
            {
                bullet.Deactivate();
                bulletPool.ReturnObject(bullet);
                activeBulletList.RemoveAt(i);
            }
            else
            {
                // Revisa colisión con cada enemigo
                foreach (var enemy in enemyList)
                {
                    if (enemy.CheckCollision(bullet))
                    {
                        enemy.GetDamage(50);
                        bullet.Deactivate();
                        bulletPool.ReturnObject(bullet);
                        activeBulletList.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        foreach (var powerUp in PowerUpList)
        {
            powerUp.Update(GameManager.Instance.Player);
        }

        foreach (var barrel in barrelList)
            barrel.Update();
    }
    public bool AllEnemiesEliminated()
    {
        return enemyList.Count == 0;
    }

    public void Render()
    {
        Engine.Clear();
        Engine.Draw(fondo, 0, 0);
        player1.Render();

        foreach (var powerUp in PowerUpList)
        {
            powerUp.Render();
        }

        foreach (var enemy in enemyList)
            enemy.Render();

        foreach (var bullet in activeBulletList)
            bullet.Render();

        foreach (var barrel in barrelList)
            barrel.Render();
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