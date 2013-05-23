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
        List<Enemy> enemies;
        List<HeavyEnemy> heavyEnemies;
        List<Diagonal> diagonals;
        List<HealthPowerUp> healthPowerUps;
        List<MissilePowerUp> missilePowerUps;
        List<RapidFirePowerUp> rapidFirePowerUps;
        List<Laser> projectiles;
        List<Missile> missiles;
        Rectangle r1, r2, r3, r4;
        public int missileCount;
        public TimeSpan fireTime = TimeSpan.FromSeconds(0.15f);
        int rapidFireTimer = 60;
        bool rapidFireActive = false;

        public void UpdateVariables(List<Enemy> enemies, List<HeavyEnemy> heavyEnemies, List<Diagonal> diagonals, List<HealthPowerUp> healthPowerUps,
            List<MissilePowerUp> missilePowerUps, List<RapidFirePowerUp> rapidFirePowerUps, List<Laser> projectiles, Player player, List<Missile> missiles, int missileCount)
        {
            this.enemies = enemies;
            this.heavyEnemies = heavyEnemies;
            this.projectiles = projectiles;
            this.player = player;
            this.missiles = missiles;
            this.diagonals = diagonals;
            this.healthPowerUps = healthPowerUps;
            this.missilePowerUps = missilePowerUps;
            this.rapidFirePowerUps = rapidFirePowerUps;
            this.missileCount = missileCount;
            this.fireTime = fireTime;

            if (rapidFireActive == true)
            {
                rapidFireTimer--;
                if (rapidFireTimer <= 0)
                {
                    rapidFireActive = false;
                    fireTime = TimeSpan.FromSeconds(0.15f);
                }
            }
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
                (int)enemies[i].Position.Y - enemies[i].Height / 2,
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
                    r3 = new Rectangle((int)projectiles[k].position.X -
                    projectiles[k].projectileWidth / 2, (int)projectiles[k].position.Y -
                    projectiles[k].projectileHeight / 2, projectiles[k].projectileWidth, projectiles[k].projectileHeight);

                    if (r3.Intersects(r2))
                    {
                        enemies[i].Health -= projectiles[k].damage;
                        projectiles[k].active = false;
                    }
                }
                for (int k = 0; k < missiles.Count; k++)
                {
                    r3 = new Rectangle((int)missiles[k].position.X -
                    missiles[k].projectileWidth / 2, (int)missiles[k].position.Y -
                    missiles[k].projectileHeight / 2, missiles[k].projectileWidth, missiles[k].projectileHeight);

                    if (r3.Intersects(r2))
                    {
                        enemies[i].Health -= missiles[k].damage;
                        missiles[k].active = false;
                    }
                }

            }

            for (int i = 0; i < diagonals.Count; i++)
            {
                r2 = new Rectangle((int)diagonals[i].Position.X,
                (int)diagonals[i].Position.Y - diagonals[i].Height / 2,
                diagonals[i].Width,
                diagonals[i].Height);

                // Determine if the two objects collided with each
                // other
                if (r1.Intersects(r2))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= (int)diagonals[i].Damage;

                    // Since the enemy collided with the player
                    // destroy it
                    diagonals[i].Health = 0;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                        player.Active = false;

                }
                for (int k = 0; k < projectiles.Count; k++)
                {
                    r3 = new Rectangle((int)projectiles[k].position.X -
                    projectiles[k].projectileWidth / 2, (int)projectiles[k].position.Y -
                    projectiles[k].projectileHeight / 2, projectiles[k].projectileWidth, projectiles[k].projectileHeight);

                    if (r3.Intersects(r2))
                    {
                        diagonals[i].Health -= projectiles[k].damage;
                        projectiles[k].active = false;
                    }
                }
                for (int k = 0; k < missiles.Count; k++)
                {
                    r3 = new Rectangle((int)missiles[k].position.X -
                    missiles[k].projectileWidth / 2, (int)missiles[k].position.Y -
                    missiles[k].projectileHeight / 2, missiles[k].projectileWidth, missiles[k].projectileHeight);

                    if (r3.Intersects(r2))
                    {
                        diagonals[i].Health -= missiles[k].damage;
                        missiles[k].active = false;
                    }
                }

            }

            for (int i = 0; i < heavyEnemies.Count; i++)
            {
                r2 = new Rectangle((int)heavyEnemies[i].Position.X,
                (int)heavyEnemies[i].Position.Y - heavyEnemies[i].Width / 2,
                heavyEnemies[i].Width,
                heavyEnemies[i].Height);

                // Determine if the two objects collided with each
                // other
                if (r1.Intersects(r2))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= (int)heavyEnemies[i].Damage;

                    // Since the enemy collided with the player
                    // destroy it
                    heavyEnemies[i].Health = 0;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                        player.Active = false;
                }
                for (int k = 0; k < projectiles.Count; k++)
                {
                    r3 = new Rectangle((int)projectiles[k].position.X -
                    projectiles[k].projectileWidth / 2, (int)projectiles[k].position.Y -
                    projectiles[k].projectileHeight / 2, projectiles[k].projectileWidth, projectiles[k].projectileHeight);

                    if (r3.Intersects(r2))
                    {
                        heavyEnemies[i].Health -= projectiles[k].damage;
                        projectiles[k].active = false;
                    }
                }
                for (int k = 0; k < missiles.Count; k++)
                {
                    r3 = new Rectangle((int)missiles[k].position.X -
                    missiles[k].projectileWidth / 2, (int)missiles[k].position.Y -
                    missiles[k].projectileHeight / 2, missiles[k].projectileWidth, missiles[k].projectileHeight);

                    if (r3.Intersects(r2))
                    {
                        heavyEnemies[i].Health -= missiles[k].damage;
                        missiles[k].active = false;
                    }
                }

            }

            for (int i = 0; i < healthPowerUps.Count; i++)
            {
                r4 = new Rectangle((int)healthPowerUps[i].powerUpPosition.X + healthPowerUps[i].Height + 20,
                (int)healthPowerUps[i].powerUpPosition.Y + healthPowerUps[i].Height / 2,
                healthPowerUps[i].Width,
                healthPowerUps[i].Height);

                // Determine if the two objects collided with each other
                if (r1.Intersects(r4))
                {
                    healthPowerUps[i].powerUpActive = false;
                    player.Health = player.Health + 30;
                }
            }

                for (int k = 0; k < missilePowerUps.Count; k++)
                {
                    r4 = new Rectangle((int)missilePowerUps[k].powerUpPosition.X + missilePowerUps[k].Height + 20,
                    (int)missilePowerUps[k].powerUpPosition.Y + missilePowerUps[k].Height / 2,
                    missilePowerUps[k].Width,
                    missilePowerUps[k].Height);

                    // Determine if the two objects collided with each other
                    if (r1.Intersects(r4))
                    {
                        missilePowerUps[k].powerUpActive = false;
                        missileCount = missileCount + 3;
                    }

                }

                for (int k = 0; k < rapidFirePowerUps.Count; k++)
                {
                    r4 = new Rectangle((int)rapidFirePowerUps[k].powerUpPosition.X + rapidFirePowerUps[k].Height + 20,
                    (int)rapidFirePowerUps[k].powerUpPosition.Y + rapidFirePowerUps[k].Height / 2,
                    rapidFirePowerUps[k].Width,
                    rapidFirePowerUps[k].Height);

                    // Determine if the two objects collided with each other
                    if (r1.Intersects(r4))
                    {
                        rapidFirePowerUps[k].powerUpActive = false;
                        fireTime = TimeSpan.FromSeconds(0.075f);
                        rapidFireActive = true;
                        rapidFireTimer = 300;
                    }

                }
            }
        }
    }
