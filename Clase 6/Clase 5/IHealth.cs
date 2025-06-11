using System;
using System.Collections.Generic;
using MyGame;

namespace MyGame
{
    public interface IHealth
    {
        int MaxHealth { get; }
        int CurrentHealth { get; }
        void SetHealth(int value);
    }
}