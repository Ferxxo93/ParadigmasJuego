using System;
using MyGame;
using System.Collections.Generic;

namespace MyGame
{
    public interface IPowerUp
    {
        void Apply(Player player);
        bool IsActive { get; }
        void Update();
        void Render();
        void SetPosition(float x, float y);
        Vector2 GetPosition();
    }
}