using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter
{
    class Missile
    {


        // Image representing the missile
        public Texture2D missileTexture;

        // Position of the missile relative to the upper left side of the screen
        public Vector2 missilePosition;

        // State of the missile
        public bool missileActive;

        // The amount of damage the missile can inflict to an enemy
        public int missileDamage;

        // Represents the viewable boundary of the game
        Viewport missileviewport;

        // Get the width of the missile ship
        public int missileWidth
        {
            get { return missileTexture.Width; }
        }

        // Get the height of the missile ship
        public int missileHeight
        {
            get { return missileTexture.Height; }
        }

        // Determines how fast the missile moves
        float missileMoveSpeed;


        public void Initialize(Viewport viewport, Texture2D texture, Vector2 position)
        {
            missileTexture = texture;
            missilePosition = position;
            this.missileviewport = viewport;

            missileActive = true;

            missileDamage = 2;

            missileMoveSpeed = 20f;
        }

        public void Update()
        {
            // misssiles always move to the right
            missilePosition.X += missileMoveSpeed;

            // Deactivate the bullet if it goes out of screen
            if (missilePosition.X + missileTexture.Width / 2 > missileviewport.Width)
                missileActive = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(missileTexture, missilePosition, null, Color.White, 0f,
            new Vector2(missileWidth / 2, missileHeight / 2), 1f, SpriteEffects.None, 0f);
        }



    }
}
