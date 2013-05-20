using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Shooter
{
    abstract class IProjectile
    {
        // Image representing the missile
        public Texture2D texture;

        // Position of the missile relative to the upper left side of the screen
        public Vector2 position;

        // State of the missile
        public bool active;

        // The amount of damage the missile can inflict to an enemy
        public int damage;

        // Represents the viewable boundary of the game
        public Viewport viewport;

        // Get the width of the missile ship
        public int projectileWidth
        {
            get { return texture.Width; }
        }

        // Get the height of the missile ship
        public int projectileHeight
        {
            get { return texture.Height; }
        }

        // Determines how fast the missile moves
        public float projectileMoveSpeed;


        abstract public void Initialize(Viewport viewport, Texture2D texture, Vector2 position);
  
        public void Update()
        {
            // missiles always move to the right
            position.X += projectileMoveSpeed;

            // Deactivate the bullet if it goes out of screen
            if (position.X + texture.Width / 2 > viewport.Width)
                active = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f,
            new Vector2(projectileWidth / 2, projectileHeight / 2), 1f, SpriteEffects.None, 0f);
        }




    }
}
