using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter
{
    abstract class IPowerUps
    {

        // Animation representing the Power up
        public Animation powerUpAnimation;

        // The position of the power up relative to the top left corner of thescreen
        public Vector2 powerUpPosition;

        // The state of the power up
        public bool powerUpActive;

        // Get the width of the power up
        public int Width
        {
            get { return powerUpAnimation.FrameWidth; }
        }

        // Get the height of the power up
        public int Height
        {
            get { return powerUpAnimation.FrameHeight; }
        }

        // The speed at which the power up moves
        public float powerUpMoveSpeedX;
        public float powerUpMoveSpeedY;


        public abstract void Initialize(Animation animation, Vector2 position, float df);


        public void Update(GameTime gameTime)
        {
            // The power up always moves to the left so decrement it's xposition
            powerUpPosition.X -= powerUpMoveSpeedX;
            powerUpPosition.Y -= powerUpMoveSpeedY;

            // Update the position of the Animation
            powerUpAnimation.Position = powerUpPosition;

            // Update Animation
            powerUpAnimation.Update(gameTime);

            // If the power up is past the screen then deactivate it
            if (powerUpPosition.X < -Width)
            {
                // By setting the Active flag to false, the game will remove this object from the active game list
                powerUpActive = false;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the animation
            powerUpAnimation.Draw(spriteBatch);
        }


    }
}
