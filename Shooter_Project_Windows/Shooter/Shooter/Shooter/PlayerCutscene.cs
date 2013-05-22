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
    class PlayerCutscene
    {
        Vector2 position;
        float viewportWidth, viewportHeight;
        int cutsceneSection = 0;
        int timer = 0;
        public bool progress = false;

        public void Initialize(float vpw, float vph, float tsaw, float tsah)
        {
            viewportHeight = vph;
            viewportWidth = vpw;
            position.X = vpw;
            position.Y = (3*tsah) / 2;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void Update()
        {
            switch (cutsceneSection)
            {
                case 0:
                    {
                        //cutscene1: Ships enter the screen
                        position.X--;

                        if (position.X == (viewportWidth) / 2)
                        {
                            cutsceneSection++;
                        }
                        break;
                    }
                case 1:
                    {
                        timer++;
                        if (timer >= 190)
                        {
                            cutsceneSection++;
                        }
                        break;
                    }

                case 2:
                    {
                        position.Y--;
                        position.X--;
                        if (position.Y == ((2*viewportHeight) / 5) + 15)
                        {
                            cutsceneSection++;
                        }
                        break;
                    }

                case 3:
                    {
                        position.X--;
                        //TODO fix so that it's dependent upon the size of the transport ship texture size.
                        if (position.X <= 0)
                        {
                            position.X = 0;
                            timer++;
                            if (timer >= 360)
                            {
                                progress = true;
                            }
                        }
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
