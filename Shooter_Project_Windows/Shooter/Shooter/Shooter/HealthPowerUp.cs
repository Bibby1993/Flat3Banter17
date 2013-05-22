using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter
{
    class HealthPowerUp : IPowerUps

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
            powerUpMoveSpeedX = 6f * df;
        }
    }
}
