using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter
{
    class RapidFirePowerUp : IPowerUps
    {
        override public void Initialize(Animation animation, Vector2 position, float df)
        {
            // Load the power up texture
            powerUpAnimation = animation;

            // Set the position of the power up
            powerUpPosition = position;

            // We initialize the power up to be active so it will be update in the game
            powerUpActive = true;

            // Set how fast the power up moves
            powerUpMoveSpeedX = 4f * df;
            powerUpMoveSpeedY = 2f * df;
        }

        public void changeDirection(GraphicsDevice gd)
        {
            Random random = new Random();
            if (random.Next(300) >= 295)
                powerUpMoveSpeedY = -powerUpMoveSpeedY;
            if (powerUpPosition.Y >= gd.Viewport.Height)
            {
                powerUpMoveSpeedY = ((float)Math.Sqrt(powerUpMoveSpeedY * powerUpMoveSpeedY));
            }
            else if (powerUpPosition.Y <= 0)
            {
                powerUpMoveSpeedY = -((float)Math.Sqrt(powerUpMoveSpeedY * powerUpMoveSpeedY));
            }
        }
    }
}
