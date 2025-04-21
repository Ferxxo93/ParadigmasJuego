using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Time
    {
        static private float deltaTime;
        static public DateTime initialTime;
        static private float timeLastFrame;
        static public float DeltaTime => deltaTime;

        public Time()
        {
            initialTime = DateTime.Now;
        }

        static public void Initialize()
        {

            initialTime = DateTime.Now;
        }

        public static void Update()
        {
            float currentTime = (float)(DateTime.Now - initialTime).TotalSeconds;
            deltaTime = currentTime - timeLastFrame;
            timeLastFrame = currentTime;
        }
    }
}
