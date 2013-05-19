using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter
{
    class Enemy :IEnemy
    {
        override public void Initialize(Animation animation, Vector2 position, float df)
        {
            // Load the enemy ship texture
            EnemyAnimation = animation;

            // Set the position of the enemy
            Position = position;

            // We initialize the enemy to be active so it will be update in the game
            Active = true;


            // Set the health of the enemy
            Health = 5 * df;

            // Set the amount of damage the enemy can do
            Damage = 5 * df;

            // Set how fast the enemy moves
            enemyMoveSpeed = 6f * df;


            // Set the score value of the enemy
            Value = 100 * df;

        }
    }
}
