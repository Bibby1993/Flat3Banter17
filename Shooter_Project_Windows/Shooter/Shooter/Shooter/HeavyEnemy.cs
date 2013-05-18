using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Shooter
{
    class Enemy
    {

        // Animation representing the enemy
        public Animation HeavyEnemyAnimation;

        // The HeavyPosition of the enemy ship relative to the top left corner of thescreen
        public Vector2 HeavyPosition;

        // The state of the Enemy Ship
        public bool HeavyActive;

        // The hit points of the enemy, if this goes to zero the enemy dies
        public int HeavyHealth;

        // The amount of HeavyDamage the enemy inflicts on the player ship
        public int HeavyDamage;

        // The amount of score the enemy will give to the player
        public int HeavyValue;

        // Get the HeavyWidth of the enemy ship
        public int HeavyWidth
        {
            get { return HeavyEnemyAnimation.FrameWidth; }
        }

        // Get the HeavyHeight of the enemy ship
        public int HeavyHeight
        {
            get { return HeavyEnemyAnimation.FrameHeight; }
        }

        // The speed at which the enemy moves
        float HeavyenemyMoveSpeed;

        public void Initialize(Animation animation, Vector2 HeavyPosition)
        {
            // Load the enemy ship texture
            HeavyEnemyAnimation = animation;

            // Set the HeavyPosition of the enemy
            HeavyPosition = HeavyPosition;

            // We initialize the enemy to be HeavyActive so it will be update in the game
            HeavyActive = true;


            // Set the HeavyHealth of the enemy
            HeavyHealth = 500;

            // Set the amount of HeavyDamage the enemy can do
            HeavyDamage = 10;

            // Set how fast the enemy moves
            HeavyenemyMoveSpeed = 1f;


            // Set the score HeavyValue of the enemy
            HeavyValue = 100;

        }


        public void Update(GameTime gameTime)
        {
            // The enemy always moves to the left so decrement it's xHeavyPosition
            HeavyPosition.X -= HeavyenemyMoveSpeed;

            // Update the HeavyPosition of the Animation
            HeavyEnemyAnimation.Position = HeavyPosition;

            // Update Animation
            HeavyEnemyAnimation.Update(gameTime);

            // If the enemy is past the screen or its HeavyHealth reaches 0 then deactivateit
            if (HeavyPosition.X < -HeavyWidth || HeavyHealth <= 0)
            {
                // By setting the HeavyActive flag to false, the game will remove this objet fromthe 
                // HeavyActive game list
                HeavyActive = false;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the animation
            HeavyEnemyAnimation.Draw(spriteBatch);
        }


    }
}
