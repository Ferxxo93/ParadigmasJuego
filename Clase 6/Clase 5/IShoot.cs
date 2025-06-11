using System;
using System.Collections.Generic;
using MyGame;

namespace MyGame
{
    public interface IShoot
    {
        void Shoot();
        void SetFireRateMultiplier(float multiplier);
        void ModifyFireRate(float newRate, float duration);
    }
}