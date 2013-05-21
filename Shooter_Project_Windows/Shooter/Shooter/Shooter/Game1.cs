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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        enum gameState {startScreen, playing, endScreen, cutscene};
        gameState state;

        // Represents the player 
        Player player;

        // A movement speed for the player
        float playerMoveSpeed, difficultyFactor;

        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        // Image used to display the static background
        Texture2D mainBackground, mainMenu, endMenu;
        Texture2D projectileTexture, missileTexture;
        Texture2D enemyTexture, heavyEnemyTexture, diagonalTexture;
        Texture2D explosionTexture;
        Texture2D healthBar;

        // Parallaxing Layers
        ParallaxingBackground bgLayer1, bgLayer2;

        // Enemies
        List<Enemy> enemies;
        List<HeavyEnemy> heavyEnemies;
        List<Diagonal> diagonals;
        List<Animation> explosions;
        List<Laser> projectiles;
        List<Missile> missiles;

        // The rate at which the enemies appear
        TimeSpan enemySpawnTime, previousSpawnTime;
        TimeSpan heavyEnemySpawnTime, previousheavyEnemySpawnTime;
        TimeSpan previousFireTime, fireTime, fireTimex2, fireTimex5;
        TimeSpan transportHealTime, previousTransportHealTime;
        TimeSpan waveTime, previousWaveTime;
        
        // The sound that is played when a laser is fired
        SoundEffect laserSound;
        SoundEffect explosionSound, explosionSound2;
        Song gameplayMusic;
        Song cryingSound;

        //Number that holds the player score
        int score, lastScore, missileCount, secondTimer, transportShipHealth, healthBarWidth, waveCounter;

        // The font used to display UI elements
        SpriteFont font;

        // A random number generator
        Random random;
        Drawer drawer;
        Collision collision;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //============================================================================================================================

        protected override void Initialize()
        {
            explosions = new List<Animation>();
            diagonals = new List<Diagonal>();
            state = gameState.startScreen;
            drawer = new Drawer();
            collision = new Collision();

            //Initialize Health Bar
            healthBarWidth = GraphicsDevice.Viewport.Width;

            //Initial values of variables
            score = 0;
            missileCount = 3;
            secondTimer = 60;
            difficultyFactor = 1.0f;
            transportShipHealth = 300;

            projectiles = new List<Laser>();

            missiles = new List<Missile>();

            // Set the laser to fire every quarter second
            fireTime = TimeSpan.FromSeconds(.15f);
            transportHealTime = TimeSpan.FromSeconds(1.1);

            //Set the laser to fire twice as fast
            fireTimex2 = TimeSpan.FromSeconds(.075f);

            //Set the laser to fire stupid as fast
            fireTimex5 = TimeSpan.FromSeconds(.03f);

            // Initialize the enemies list
            enemies = new List<Enemy>();

            // Initialize the heavyEnemies list
            heavyEnemies = new List<HeavyEnemy>();

            // Set the time keepers to zero
            previousSpawnTime = TimeSpan.Zero;

            // Set the time keepers to zero
            previousheavyEnemySpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            // Used to determine how fast enemy respawns
            heavyEnemySpawnTime = TimeSpan.FromSeconds(1.0f);

            // Initialize our random number generator
            random = new Random();

            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();

            // Initialize the player class
            player = new Player();

            // Set a constant player move speed
            playerMoveSpeed = 12.0f;
            base.Initialize();
        }

        //============================================================================================================================

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the player resources
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("shipAnimation");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 1, 30, Color.White, 1f, true);

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y
            + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, playerPosition);

            // Load the parallaxing background
            bgLayer1.Initialize(Content, "bglayer1", GraphicsDevice.Viewport.Width, -1);
            bgLayer2.Initialize(Content, "bglayer2", GraphicsDevice.Viewport.Width, -2);

            enemyTexture = Content.Load<Texture2D>("smallShip");
            heavyEnemyTexture = Content.Load<Texture2D>("bigShip");
            diagonalTexture = Content.Load<Texture2D>("diagonal");
            healthBar = Content.Load<Texture2D>("Health Bar");

            projectileTexture = Content.Load<Texture2D>("laser");
            missileTexture = Content.Load<Texture2D>("rocket");

            explosionTexture = Content.Load<Texture2D>("explosion");

            // Load the music
            gameplayMusic = Content.Load<Song>("sound/gameMusic");
            cryingSound = Content.Load<Song>("sound/crying_loud_male");

            // Load the laser and explosion sound effect
            laserSound = Content.Load<SoundEffect>("sound/laserFire");
            explosionSound = Content.Load<SoundEffect>("sound/explosion");
            explosionSound2 = Content.Load<SoundEffect>("sound/explosion-04");

            // Start the music right away
            PlayMusic(gameplayMusic);

            // Load the score font
            font = Content.Load<SpriteFont>("gameFont");

            mainBackground = Content.Load<Texture2D>("mainbackground");

            mainMenu = Content.Load<Texture2D>("mainMenu");
            endMenu = Content.Load<Texture2D>("endMenu");
        }


        //============================================================================================================================

        protected override void Update(GameTime gameTime)
        {
            if (state == gameState.startScreen)
            {
                currentKeyboardState = Keyboard.GetState();
                currentGamePadState = GamePad.GetState(PlayerIndex.One);
                currentGamePadState.Triggers.Right.Equals(3);
                if (currentKeyboardState.IsKeyDown(Keys.Enter)|| currentGamePadState.Buttons.A == ButtonState.Pressed)
                {

                    state=gameState.cutscene
                        ;
                }
            }

            else if (state == gameState.cutscene)
            {
            }

            else
            {
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                // Save the previous state of the keyboard and game pad so we can determinesingle key/button presses
                previousGamePadState = currentGamePadState;
                previousKeyboardState = currentKeyboardState;

                // Read the current state of the keyboard and gamepad and store it
                currentKeyboardState = Keyboard.GetState();
                currentGamePadState = GamePad.GetState(PlayerIndex.One);

                //Update the player
                UpdatePlayer(gameTime);

                // Update the parallaxing background
                bgLayer1.Update();
                bgLayer2.Update();

                // Update the enemies
                UpdateEnemies(gameTime);

                // Update the Heavy enemies
                UpdateHeavyEnemies(gameTime);

                // Update diagonals
                UpdateDiagonals(gameTime);

                // Update the collision
                collision.UpdateVariables(enemies, heavyEnemies, diagonals, projectiles, player, missiles);
                collision.collision();

                // Update the weapons
                UpdateProjectiles(gameTime);
                UpdateMissiles();

                //Check for missiles
                CheckforSecond();

                // Update the explosions
                UpdateExplosions(gameTime);

                checkShip(gameTime);

                base.Update(gameTime);
            }
        }

        //============================================================================================================================

        protected override void Draw(GameTime gameTime)
        {
            if (state == gameState.startScreen)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(mainMenu, Vector2.Zero, Color.White);
                spriteBatch.DrawString(font, "Last score: " + lastScore, new Vector2((GraphicsDevice.Viewport.Width+127)/3, GraphicsDevice.Viewport.Height-200), Color.White);
                spriteBatch.DrawString(font, "Press enter or A on gamepad to start", new Vector2((GraphicsDevice.Viewport.Width-310)/3, GraphicsDevice.Viewport.Height-310), Color.White);

                spriteBatch.End();
            }
            else if (state == gameState.cutscene)
            {
            }

            else if (state == gameState.playing)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                drawer.UpdateVariables(enemies, heavyEnemies, diagonals, projectiles, explosions,
                    spriteBatch, player, mainBackground, healthBar, bgLayer1, bgLayer2, missiles);
                // Start drawing
                drawer.DrawAll();
                // Draw the score
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
                spriteBatch.DrawString(font, "Missiles: " + missileCount, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 300, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
                spriteBatch.DrawString(font, "Transport Health: " + transportShipHealth, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 500, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);

                // Draw the player health
                spriteBatch.DrawString(font, "Health: " + player.Health, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), healthColor(player.Health));

                if (state == gameState.endScreen)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(endMenu, Vector2.Zero, Color.White);
                    spriteBatch.End();
                }

                // Stop drawing
                spriteBatch.End();

                base.Draw(gameTime);
            }
        }

        //===================================================================================================================================
       
        private Color healthColor(int health) {
            if (player.Health <= 50 && player.Health > 25)
            {
                return Color.Yellow;
            }
            else if (player.Health <= 25)
            {
                return Color.Red;
            }
            else return Color.Green;
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);

            // Get Thumbstick Controls
            player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

            // Use the Keyboard / Dpad
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                player.Position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                player.Position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                player.Position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                player.Position.Y += playerMoveSpeed;
            }

            // Make sure that the player does not go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, player.Width / 2, GraphicsDevice.Viewport.Width - (player.Width / 2));
            player.Position.Y = MathHelper.Clamp(player.Position.Y, player.Height / 2, GraphicsDevice.Viewport.Height - (player.Height / 2));

            //Last resort, double fire rate
            if (player.Health <= 10)
            {
                fireTime = fireTimex2;
            }

            // reset score if player health goes to zero
            if (player.Health <= 0 || transportShipHealth <= 0)
            {
                lastScore = score;

                //Play the crying man
                CryingSound(cryingSound);
                score = 0;
                player.Health = 100;
                transportShipHealth = 300;
                state = gameState.startScreen;
                Reset();
            }
        }

        //==============================================================================================================================

        private void AddEnemy()
        {
            // Create the animation object
            Animation enemyAnimation = new Animation();

            // Initialize the animation with the correct animation information
            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, enemyTexture.Width, enemyTexture.Height, 1, 30, Color.White, 1f, true);

            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height - 100));

            // Create an enemy
            Enemy enemy = new Enemy();

            // Initialize the enemy
            enemy.Initialize(enemyAnimation, position, difficultyFactor);

            // Add the enemy to the active enemies list
            enemies.Add(enemy);
        }

        //==============================================================================================================================

        private void AddHeavyEnemy()
        {
            // Create the animation object
            Animation heavyEnemyAnimation = new Animation();

            // Initialize the animation with the correct animation information
            heavyEnemyAnimation.Initialize(heavyEnemyTexture, Vector2.Zero, heavyEnemyTexture.Width, heavyEnemyTexture.Height, 1, 30, Color.White, 1f, true);

            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + heavyEnemyTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height - 100));

            // Create an enemy
            HeavyEnemy heavyEnemy = new HeavyEnemy();

            // Initialize the enemy
            heavyEnemy.Initialize(heavyEnemyAnimation, position, difficultyFactor);

            // Add the heavyEnemy to the active enemies list
            heavyEnemies.Add(heavyEnemy);
        }

        private void AddDiagonal()
        {
            // Create the animation object
            Animation DiagonalAnimation = new Animation();

            // Initialize the animation with the correct animation information
            DiagonalAnimation.Initialize(diagonalTexture, Vector2.Zero, diagonalTexture.Width, diagonalTexture.Height, 1, 30, Color.White, 1f, true);

            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + diagonalTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height - 100));

            // Create an enemy
            Diagonal diagonal = new Diagonal();

            // Initialize the enemy
            diagonal.Initialize(DiagonalAnimation, position, difficultyFactor);

            // Add the heavyEnemy to the active enemies list
            diagonals.Add(diagonal);
        }

        //==============================================================================================================================

        private void UpdateEnemies(GameTime gameTime)
        {
            // Spawn a new enemy enemy every 1.5 seconds
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;

                // Add an Enemy
                AddEnemy();
            }

            // Update the Enemies
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);

                if (enemies[i].Active == false)
                {

                    // If not active and health <= 0
                    if (enemies[i].Health <= 0)
                    {
                        // Add an explosion
                        AddExplosion(enemies[i].Position);

                        // Play the explosion sound
                        SoundEffectInstance explosionSoundInstance;
                        explosionSoundInstance = explosionSound.CreateInstance();
                        explosionSoundInstance.Volume = 0.5f;
                        explosionSoundInstance.Play();

                        score += ((int)enemies[i].Value);
                    }

                    else transportShipHealth -= ((int)enemies[i].Health)*3;

                    enemies.RemoveAt(i);
                }
            }
        }

        //==============================================================================================================================

        private void UpdateHeavyEnemies(GameTime gameTime)
        {
            // Spawn a new enemy enemy every 1.5 seconds
           /* if (gameTime.TotalGameTime - previousheavyEnemySpawnTime > heavyEnemySpawnTime)
            {
                previousheavyEnemySpawnTime = gameTime.TotalGameTime;

                // Add an Heavy Enemy
                AddHeavyEnemy();
            }
            */

            if (heavyEnemies.Count <= 0)
                AddHeavyEnemy();
            // Update the Heavy Enemies
            for (int i = heavyEnemies.Count - 1; i >= 0; i--)
            {
                heavyEnemies[i].Update(gameTime);

                if (heavyEnemies[i].Active == false)
                {

                    // If not active and health <= 0
                    if (heavyEnemies[i].Health <= 0)
                    {
                        // Add an explosion
                        AddExplosion(heavyEnemies[i].Position);

                        // Play the explosion sound
                        SoundEffectInstance explosionSoundInstance;
                        explosionSoundInstance = explosionSound2.CreateInstance();
                        explosionSoundInstance.Volume = 0.5f;
                        explosionSoundInstance.Play();

                        score += ((int)heavyEnemies[i].Value) ;

                    }
                    else transportShipHealth -= (int)heavyEnemies[i].Health;

                    heavyEnemies.RemoveAt(i);
                }
            }
        }

        private void UpdateDiagonals(GameTime gameTime)
        {
            {
                // Spawn a new enemy enemy every 1.5 seconds
                /* if (gameTime.TotalGameTime - previousheavyEnemySpawnTime > heavyEnemySpawnTime)
                 {
                     previousheavyEnemySpawnTime = gameTime.TotalGameTime;

                     // Add an Heavy Enemy
                     AddHeavyEnemy();
                 }
                 */

                if (diagonals.Count <= 0)
                    AddDiagonal();
                // Update the Heavy Enemies
                for (int i = diagonals.Count - 1; i >= 0; i--)
                {
                    diagonals[i].Update(gameTime);
                    diagonals[i].changeDirection(GraphicsDevice);

                    if (diagonals[i].Active == false)
                    {

                        // If not active and health <= 0
                        if (diagonals[i].Health <= 0)
                        {
                            // Add an explosion
                            AddExplosion(diagonals[i].Position);

                            // Play the explosion sound
                            explosionSound2.Play();

                            score += ((int)diagonals[i].Value);

                        }
                        else transportShipHealth -= (int)diagonals[i].Health;

                        diagonals.RemoveAt(i);
                    }
                }
            }
        }

        private void AddProjectile(Vector2 position)
        {
            Laser projectile = new Laser();
            projectile.Initialize(GraphicsDevice.Viewport, projectileTexture, position);
            projectiles.Add(projectile);
        }

        //==============================================================================================================================

        private void UpdateProjectiles(GameTime gameTime)
        {
            // Fire only every interval we set as the fireTime
            if (gameTime.TotalGameTime - previousFireTime > fireTime)
            {
                // Reset our current time
                previousFireTime = gameTime.TotalGameTime;

                // Add the projectile, but add it to the front and center of the player
                AddProjectile(player.Position + new Vector2(player.Width / 2, 0));

                // Play the laser sound
                laserSound.Play();

            }
            // Update the Projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();

                if (projectiles[i].active == false)
                {
                    projectiles.RemoveAt(i);
                }
            }
        }

        //==============================================================================================================================

        private void UpdateMissiles()
        {
            // Update the Missiles
            for (int i = missiles.Count - 1; i >= 0; i--)
            {
                missiles[i].Update();

                if (missiles[i].active == false)
                {
                    missiles.RemoveAt(i);
                }
            }
        }
        //==============================================================================================================================

        private void AddExplosion(Vector2 position)
        {
            Animation explosion = new Animation();
            explosion.Initialize(explosionTexture, position, 134, 134, 12, 45, Color.White, 1f, false);
            explosions.Add(explosion);
        }

        //==============================================================================================================================

        private void UpdateExplosions(GameTime gameTime)
        {
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                explosions[i].Update(gameTime);
                if (explosions[i].Active == false)
                {
                    explosions.RemoveAt(i);
                }
            }
        }

        //==============================================================================================================================

        private void PlayMusic(Song song)
        {
            // Due to the way the MediaPlayer plays music,
            // we have to catch the exception. Music will play when the game is not tethered
            try
            {
                // Play the music
                MediaPlayer.Play(song);

                // Loop the currently playing song
                MediaPlayer.IsRepeating = true;
            }
            catch { }
        }

        private void CryingSound(Song song)
        {
            // Due to the way the MediaPlayer plays music,
            // we have to catch the exception. Music will play when the game is not tethered
            try
            {
                // Play the music
                MediaPlayer.Play(song);

                // Don't loop the currently playing song
                MediaPlayer.IsRepeating = false;
            }
            catch { }
        }

        private void checkShip(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousTransportHealTime > transportHealTime)
            {
                // Reset our current time
                previousTransportHealTime = gameTime.TotalGameTime;
                if(transportShipHealth<300)
                transportShipHealth++;
            }
        }
        private void CheckforSecond()
        {

            if (secondTimer != 60)
                secondTimer++;
            if (secondTimer >= 60)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Space) ||
               GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
                {
                    if (missileCount > 0)
                    {
                        Missile missile = new Missile();
                        missile.Initialize(GraphicsDevice.Viewport, missileTexture, player.Position + new Vector2(player.Width / 2, 0));
                        missiles.Add(missile);
                        missileCount = missileCount - 1;
                        secondTimer = 0;
                    }
                }
            }
        }

        private void Reset()
        {

            explosions.Clear();

            diagonals.Clear();

            drawer = new Drawer();

            collision = new Collision();

            //Set player's score to zero
            missileCount = 3;
            secondTimer = 60;
            difficultyFactor = 1.0f;

            projectiles.Clear();

            missiles.Clear();

            // Initialize the enemies list
            enemies.Clear();

            // Initialize the heavyEnemies list
            heavyEnemies.Clear();

            //Reset fire time
            fireTime = TimeSpan.FromSeconds(.15f);
        }
    }
}