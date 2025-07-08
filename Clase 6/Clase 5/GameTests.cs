using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public static class GameTests
    {
        public static void RunAllTests()
        {
            try
            {
                Test_LevelController_Initialization();
                Test_LevelController_AddBullet();
                Test_LevelController_AllEnemiesEliminated();

                Console.WriteLine("TODOS LOS TESTS PASARON");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ERROR EN TEST: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static void Test_LevelController_Initialization()
        {
            var controller = new LevelController();

            if (controller.Player1 == null)
                throw new Exception("Player1 no se inicializó.");

            if (controller.BarrelList.Count != 16)
                throw new Exception($"Se esperaban 16 barriles, se encontraron {controller.BarrelList.Count}.");
        }

        private static void Test_LevelController_AddBullet()
        {
            var controller = new LevelController();
            controller.AddBullet(200, 200, 1, 0);

            if (controller.BulletList.Count != 1)
                throw new Exception("No se añadió la bala a la lista.");
        }

        private static void Test_LevelController_AllEnemiesEliminated()
        {
            var controller = new LevelController();

            if (!controller.AllEnemiesEliminated())
                throw new Exception("Debería no haber enemigos al inicio.");
        }
    }
}