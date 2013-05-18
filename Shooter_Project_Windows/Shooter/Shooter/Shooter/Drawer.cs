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
        SpriteBatch spriteBatch;
        List<Projectile> projectiles;

        public void UpdateVariables(List<Enemy> enemies, List<HeavyEnemy> heavyEnemies, List<Projectile> projectiles, SpriteBatch spriteBatch)
        {
            this.enemies = enemies;
            this.heavyEnemies = heavyEnemies;
            this.spriteBatch = spriteBatch;
            this.projectiles = projectiles;
        }

        public void DrawAll()
        {
            DrawBackgrounds();
            DrawAllEnemies();
            DrawPlayerAndProjectiles();
        }

        private void DrawAllEnemies()
        {
            // Draw the Enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }

            // Draw the Heavy Enemies
            for (int i = 0; i < heavyEnemies.Count; i++)
            {
                heavyEnemies[i].Draw(spriteBatch);
            }
        }

        private void DrawPlayerAndProjectiles()
        {
            // Draw the Projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(spriteBatch);
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
