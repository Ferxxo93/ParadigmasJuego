using MyGame;
using System;
using System.Collections.Generic;

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