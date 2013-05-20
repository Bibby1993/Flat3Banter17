using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter
{
    class Projectile : IProjectile
    {
        override public void Initialize(Viewport viewport, Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position= position;
            this.viewport = viewport;

            active = true;

            damage = 2;

            projectileMoveSpeed = 20f;
        }
    }
}
