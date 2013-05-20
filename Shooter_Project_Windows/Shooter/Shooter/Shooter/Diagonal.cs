using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Shooter
{
    class Diagonal : IEnemy
    {
        public override void Initialize(Animation animation, Vector2 position, float df)
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
            enemyMoveSpeedX = 2f * df;
            enemyMoveSpeedY = 3f*df;


            // Set the score value of the enemy
            Value = 500 * df;

        }

        public void changeDirection(GraphicsDevice gd) {
            Random random = new Random();
            if (random.Next(300) >= 295)
             enemyMoveSpeedY = -enemyMoveSpeedY;
            if (Position.Y>=gd.Viewport.Height)
            {
                enemyMoveSpeedY = ((float)Math.Sqrt(enemyMoveSpeedY*enemyMoveSpeedY));
            }
            else if (Position.Y <= 0)
            {
                enemyMoveSpeedY = -((float)Math.Sqrt(enemyMoveSpeedY * enemyMoveSpeedY));
            }
        }
    }
}
