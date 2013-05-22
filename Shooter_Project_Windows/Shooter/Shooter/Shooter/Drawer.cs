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
    class Drawer
    {
        List<Enemy> enemies;
        List<HeavyEnemy> heavyEnemies;
        List<Diagonal> diagonals;
        List<HealthPowerUp> healthPowerUps;
        List<MissilePowerUp> missilePowerUps;
        SpriteBatch spriteBatch;
        List<Laser> projectiles;
        List<Missile> missiles;
        Player player;
        List<Animation> explosions;
        Texture2D mainBackground;
        Texture2D healthBar;
        ParallaxingBackground bgLayer1;
        ParallaxingBackground bgLayer2;
        int healthBarWidth;

        public void UpdateVariables(List<Enemy> enemies, List<HeavyEnemy> heavyEnemies, List<Diagonal> diagonals, List<HealthPowerUp> healthPowerUps, List<MissilePowerUp> missilePowerUps,
            List<Laser> projectiles, List<Animation> explosions, SpriteBatch spriteBatch, Player player, Texture2D mainBackground, Texture2D healthBar, 
            ParallaxingBackground bgLayer1, ParallaxingBackground bgLayer2, List<Missile> missiles)
        {
            this.enemies = enemies;
            this.heavyEnemies = heavyEnemies;
            this.diagonals = diagonals;
            this.healthPowerUps = healthPowerUps;
            this.missilePowerUps = missilePowerUps;
            this.spriteBatch = spriteBatch;
            this.projectiles = projectiles;
            this.explosions = explosions;
            this.player = player;
            this.mainBackground = mainBackground;
            this.bgLayer1 = bgLayer1;
            this.bgLayer2 = bgLayer2;
            this.missiles = missiles;
            this.healthBar = healthBar;

        }

        public void DrawAll()
        {
            spriteBatch.Begin();
            DrawBackgrounds();
            DrawAllEnemies();
            DrawPlayerAndProjectiles();
            DrawAllPowerUps();
            spriteBatch.End();
        }

        public void DrawSomeBackgrounds()
        {
            spriteBatch.Begin();
            DrawBackgrounds();
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

            for (int i = 0; i < diagonals.Count; i++)
            {
                diagonals[i].Draw(spriteBatch);
            }

        }

        private void DrawAllPowerUps()
        {
            // Draw the Health Power Ups
            for (int i = 0; i < healthPowerUps.Count; i++)
            {
                healthPowerUps[i].Draw(spriteBatch);
            }

            // Draw the Missile Power Ups
            for (int k = 0; k < missilePowerUps.Count; k++)
            {
                missilePowerUps[k].Draw(spriteBatch);
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

             // Draw the health bar that lowers
            spriteBatch.Draw(healthBar, new Vector2(healthBarWidth/2 - player.Health/2), new Rectangle(0, 46, healthBar.Width, 41), Color.Gray);

            //Draw the health
            spriteBatch.Draw(healthBar, new Vector2(healthBarWidth / 2 - player.Health / 2), new Rectangle(0, 46, (int)(healthBar.Width*((double)player.Health/100)), 41), Color.Red);
             
            spriteBatch.Draw(healthBar, new Vector2(healthBarWidth / 2 - player.Health / 2), new Rectangle(0, 0, healthBar.Width, 41), Color.White);

        }

        private void DrawBackgrounds()
        {
            spriteBatch.Draw(mainBackground, Vector2.Zero, Color.White);
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);
        }
    }
}
