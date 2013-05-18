using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Shooter
{
    class HeavyEnemy
    {

        // Animation representing the enemy
        public Animation heavyEnemyAnimation;

        // The heavyPosition of the enemy ship relative to the top left corner of thescreen
        public Vector2 heavyPosition;

        // The state of the Enemy Ship
        public bool heavyActive;

        // The hit points of the enemy, if this goes to zero the enemy dies
        public int heavyHealth;

        // The amount of heavyDamage the enemy inflicts on the player ship
        public int heavyDamage;

        // The amount of score the enemy will give to the player
        public int heavyValue;

        // Get the heavyWidth of the enemy ship
        public int heavyWidth
        {
            get { return heavyEnemyAnimation.FrameWidth; }
        }

        // Get the heavyHeight of the enemy ship
        public int heavyHeight
        {
            get { return heavyEnemyAnimation.FrameHeight; }
        }

        // The speed at which the enemy moves
        float heavyenemyMoveSpeed;

        public void Initialize(Animation animation, Vector2 vheavyPosition)
        {
            // Load the enemy ship texture
            heavyEnemyAnimation = animation;

            // Set the heavyPosition of the enemy
            heavyPosition = vheavyPosition;

            // We initialize the enemy to be heavyActive so it will be update in the game
            heavyActive = true;


            // Set the heavyHealth of the enemy
            heavyHealth = 500;

            // Set the amount of heavyDamage the enemy can do
            heavyDamage = 10;

            // Set how fast the enemy moves
            heavyenemyMoveSpeed = 1f;


            // Set the score heavyValue of the enemy
            heavyValue = 100;

        }


        public void Update(GameTime gameTime)
        {
            // The enemy always moves to the left so decrement it's xheavyPosition
            heavyPosition.X -= heavyenemyMoveSpeed;

            // Update the heavyPosition of the Animation
            heavyEnemyAnimation.Position = heavyPosition;

            // Update Animation
            heavyEnemyAnimation.Update(gameTime);

            // If the enemy is past the screen or its heavyHealth reaches 0 then deactivateit
            if (heavyPosition.X < -heavyWidth || heavyHealth <= 0)
            {
                // By setting the heavyActive flag to false, the game will remove this objet fromthe 
                // heavyActive game list
                heavyActive = false;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the animation
            heavyEnemyAnimation.Draw(spriteBatch);
        }


    }
}
