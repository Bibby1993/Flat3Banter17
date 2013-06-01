using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shooter
{

    public class WaveTimer
    {

        enum waveCount { one, two, three, four, five, six, seven, eight, nine, ten };
        waveCount waveState;
        public int enemiesLeft, heavyEnemiesLeft, diagonalsLeft;
        public TimeSpan enemySpawnTime, previousEnemySpawnTime;
        public TimeSpan heavyEnemySpawnTime, previousheavyEnemySpawnTime;
        public TimeSpan diagonalEnemySpawnTime, previousDiagonalEnemySpawnTime;

        //spawntimes for enemies

        // Set the time keepers to zero



    
        //Constructors
        public WaveTimer()
        {
            waveState = waveCount.one;
            enemiesLeft = 10;
            heavyEnemiesLeft = 10;
            diagonalsLeft = 5;
            previousheavyEnemySpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            // Used to determine how fast heavy enemy respawns
            heavyEnemySpawnTime = TimeSpan.FromSeconds(5.0f);

            diagonalEnemySpawnTime = TimeSpan.FromSeconds(10.0f);
        }
        public void update(int time)         
        {
            /*If there are no enemies, heavy enemies or diagonals left to spawn & none left on the screen
             * then move on to the next wave number. When the next wave number is first changed to reset
             * the number of enemies left to spawn to a different number.
            */
        }

}
}
