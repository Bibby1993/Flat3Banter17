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
    class Collision
    {
        Player player;
        List<EnemyObject> enemies;
        List<HeavyEnemyObject> heavyEnemies;
        List<Projectile> projectiles;
        List<Missile> missiles;
        Rectangle r1, r2, r3;

        public void UpdateVariables(List<EnemyObject> enemies, List<HeavyEnemyObject> heavyEnemies, List<Projectile> projectiles,
            Player player, List<Missile> missiles)
        {
            this.enemies = enemies;
            this.heavyEnemies = heavyEnemies;           
            this.projectiles = projectiles;
            this.player = player;
            this.missiles = missiles;
        }

        public void collision()
        {
            r1 = new Rectangle((int)player.Position.X,
            (int)player.Position.Y,
            player.Width,
            player.Height);

            for (int i = 0; i < enemies.Count; i++)
            {
                r2 = new Rectangle((int)enemies[i].Position.X,
                (int)enemies[i].Position.Y - enemies[i].Height / 2 ,
                enemies[i].Width,
                enemies[i].Height);

                // Determine if the two objects collided with each
                // other
                if (r1.Intersects(r2))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= (int)enemies[i].Damage;

                    // Since the enemy collided with the player
                    // destroy it
                    enemies[i].Health = 0;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                        player.Active = false;
                }
                for (int k = 0; k < projectiles.Count; k++)
                {
                    r3 = new Rectangle((int)projectiles[k].Position.X -
                    projectiles[k].Width / 2, (int)projectiles[k].Position.Y -
                    projectiles[k].Height / 2, projectiles[k].Width, projectiles[k].Height);

                    if (r3.Intersects(r2))
                    {
                        enemies[i].Health -= projectiles[k].Damage;
                        projectiles[k].Active = false;
                    }
                }
                for (int k = 0; k < missiles.Count; k++)
                {
                    r3 = new Rectangle((int)missiles[k].missilePosition.X -
                    missiles[k].missileWidth / 2, (int)missiles[k].missilePosition.Y -
                    missiles[k].missileHeight / 2, missiles[k].missileWidth, missiles[k].missileHeight);

                    if (r3.Intersects(r2))
                    {
                        enemies[i].Health -= missiles[k].missileDamage;
                        missiles[k].missileActive = false;
                    }
                }

            }
            for (int i = 0; i < heavyEnemies.Count; i++)
            {
                r2= new Rectangle((int)heavyEnemies[i].heavyPosition.X,
                (int)heavyEnemies[i].heavyPosition.Y - heavyEnemies[i].heavyWidth / 2,
                heavyEnemies[i].heavyWidth,
                heavyEnemies[i].heavyHeight);

                // Determine if the two objects collided with each
                // other
                if (r1.Intersects(r2))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= (int)heavyEnemies[i].heavyDamage;

                    // Since the enemy collided with the player
                    // destroy it
                    heavyEnemies[i].heavyHealth = 0;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                        player.Active = false;
                }
                for (int k = 0; k < projectiles.Count; k++)
                {
                    r3 = new Rectangle((int)projectiles[k].Position.X -
                    projectiles[k].Width / 2, (int)projectiles[k].Position.Y -
                    projectiles[k].Height / 2, projectiles[k].Width, projectiles[k].Height);

                    if (r3.Intersects(r2))
                    {
                        heavyEnemies[i].heavyHealth -= projectiles[k].Damage;
                        projectiles[k].Active = false;
                    }
                }
                for (int k = 0; k < missiles.Count; k++)
                {
                    r3 = new Rectangle((int)missiles[k].missilePosition.X -
                    missiles[k].missileWidth / 2, (int)missiles[k].missilePosition.Y -
                    missiles[k].missileHeight / 2, missiles[k].missileWidth, missiles[k].missileHeight);

                    if (r3.Intersects(r2))
                    {
                        heavyEnemies[i].heavyHealth -= missiles[k].missileDamage;
                        missiles[k].missileActive = false;
                    }
                }

            }
        }

    }
}
