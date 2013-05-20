using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter
{
    abstract class IEnemy
    {

        // Animation representing the enemy
        public Animation EnemyAnimation;

        // The position of the enemy ship relative to the top left corner of thescreen
        public Vector2 Position;

        // The state of the Enemy Ship
        public bool Active;

        // The hit points of the enemy, if this goes to zero the enemy dies
        public float Health;

        // The amount of damage the enemy inflicts on the player ship
        public float Damage;

        // The amount of score the enemy will give to the player
        public float Value;

        // Get the width of the enemy ship
        public int Width
        {
            get { return EnemyAnimation.FrameWidth; }
        }

        // Get the height of the enemy ship
        public int Height
        {
            get { return EnemyAnimation.FrameHeight; }
        }

        // The speed at which the enemy moves
        public float enemyMoveSpeedX;
        public float enemyMoveSpeedY;

        
        public abstract void Initialize(Animation animation, Vector2 position, float df);


        public void Update(GameTime gameTime)
        {
            // The enemy always moves to the left so decrement it's xposition
            Position.X -= enemyMoveSpeedX;
            Position.Y -= enemyMoveSpeedY;

            // Update the position of the Animation
            EnemyAnimation.Position = Position;

            // Update Animation
            EnemyAnimation.Update(gameTime);

            // If the enemy is past the screen or its health reaches 0 then deactivateit
            if (Position.X < -Width || Health <= 0)
            {
                // By setting the Active flag to false, the game will remove this objet fromthe 
                // active game list
                Active = false;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the animation
            EnemyAnimation.Draw(spriteBatch);
        }


    }
}
