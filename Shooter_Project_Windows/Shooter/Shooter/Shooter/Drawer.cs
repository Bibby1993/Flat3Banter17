﻿using System;
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
    class Drawer
    {
        List<Enemy> enemies;
        List<HeavyEnemy> heavyEnemies;
        SpriteBatch spriteBatch;
        List<Laser> projectiles;
        List<Missile> missiles;
        Player player;
        List<Animation> explosions;
        Texture2D mainBackground;
        ParallaxingBackground bgLayer1;
        ParallaxingBackground bgLayer2;


        public void UpdateVariables(List<Enemy> enemies, List<HeavyEnemy> heavyEnemies, List<Laser> projectiles,
            List<Animation> explosions, SpriteBatch spriteBatch, Player player, Texture2D mainBackground,
            ParallaxingBackground bgLayer1, ParallaxingBackground bgLayer2, List<Missile> missiles)
        {
            this.enemies = enemies;
            this.heavyEnemies = heavyEnemies;
            this.spriteBatch = spriteBatch;
            this.projectiles = projectiles;
            this.explosions = explosions;
            this.player = player;
            this.mainBackground = mainBackground;
            this.bgLayer1 = bgLayer1;
            this.bgLayer2 = bgLayer2;
            this.missiles = missiles;
        }

        public void DrawAll()
        {
            spriteBatch.Begin();
            DrawBackgrounds();
            DrawAllEnemies();
            DrawPlayerAndProjectiles();
            spriteBatch.End();
        }

        private void DrawAllEnemies()
        {
            // Draw the Heavy Enemies
            for (int i = 0; i < heavyEnemies.Count; i++)
            {
                heavyEnemies[i].Draw(spriteBatch);
            }

            // Draw the Enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }
        }

        private void DrawPlayerAndProjectiles()
        {
            // Draw the Projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(spriteBatch);
            }

            for (int i = 0; i < missiles.Count; i++)
            {
                missiles[i].Draw(spriteBatch);
            }
                    

            // Draw the Player
            player.Draw(spriteBatch);

            // Draw the explosions
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Draw(spriteBatch);
            }
        }

        private void DrawBackgrounds()
        {
            spriteBatch.Draw(mainBackground, Vector2.Zero, Color.White);
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);
        }
    }
}
