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
        static void Main(string[] args)
        {
            bool runTests = true;
            if (runTests)
            {
                Console.WriteLine("=== EJECUTANDO TESTS ===");
                GameTests.RunAllTests();
                Console.WriteLine("=======================");
            }

           
            Engine.Initialize();
            Time.Initialize();
            GameManager.Instance.Initialize();

            while (true)
            {
                Time.Update();
                GameManager.Instance.Update();
                GameManager.Instance.Render();
            }
        }
    }
}