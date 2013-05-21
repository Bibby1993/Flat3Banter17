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
    class TransportShip
    {
        Vector2 position;
        float viewportWidth, viewportHeight;
        int cutsceneSection = 0;
        int timer=0;

        public void Initialize(float vpw, float vph)
        {
            viewportHeight = vph;
            viewportWidth = vpw;
            position.X = vpw;
            position.Y = ((2*vph) / 5) - 10;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void Update()
        {
        switch(cutsceneSection)
            {
                case 0:
                    {
                        //cutscene1: Ships enter the screen
                        position.X--;

                        if (position.X == (viewportWidth)/2)
                        {
                            cutsceneSection++;
                        }
                        break;
                    }
                case 1: 
                    {
                        timer++;
                        if (timer >= 60)
                        {
                            cutsceneSection++;
                        }
                        break;
                    }
                case 2:
                    {
                        position.X--;
                        //TODO fix so that it's dependent upon the size of the transport ship texture size.
                        if (position.X <= 0)
                            //gamestate update
                            ;
                        break;
                    }

                    default:
                    {
                                break;
                    }
            }

        }

    }
}
