using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Shooter
{
    class HeavyEnemy : Enemy
    {
     

        // The speed at which the enemy moves
        float enemyMoveSpeed;

        //Ignore error about needing 'new'
        public void Initialize(Animation animation, Vector2 position, float df)
        {
            // Load the enemy ship texture
            EnemyAnimation = animation;

            // Set the position of the enemy
            Position = position;

            // We initialize the enemy to be active so it will be update in the game
            Active = true;


            // Set the health of the enemy
            Health = 50 * df;

            // Set the amount of damage the enemy can do
            Damage = 30 * df;

            // Set how fast the enemy moves
            enemyMoveSpeed = 1f * df;


            // Set the score value of the enemy
            Value = 500 * df;

        }
    }
}
