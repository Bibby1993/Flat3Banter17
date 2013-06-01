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
        int enemiesLeft, heavyEnemiesLeft, diagonalsLeft;
    
        //Constructors
        public WaveTimer()
        {
            waveState = waveCount.one;
            enemiesLeft = 10;
            heavyEnemiesLeft = 10;
            diagonalsLeft = 5;
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
