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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        enum gameState {startScreen, playing, endScreen};
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
        Texture2D mainBackground;
        Texture2D mainMenu;
        Texture2D endMenu;
        Texture2D projectileTexture;
        Texture2D missileTexture;
        Texture2D enemyTexture, heavyEnemyTexture;
        Texture2D explosionTexture;

        // Parallaxing Layers
        ParallaxingBackground bgLayer1;
        ParallaxingBackground bgLayer2;

        // Enemies
        List<Enemy> enemies;
        List<HeavyEnemy> heavyEnemies;
        List<Animation> explosions;
        List<Projectile> projectiles;
        List<Missile> missiles;

        // The rate at which the enemies appear
        TimeSpan enemySpawnTime;
        TimeSpan previousSpawnTime;
        TimeSpan heavyEnemySpawnTime;
        TimeSpan previousheavyEnemySpawnTime;
        TimeSpan fireTime;
        TimeSpan previousFireTime;
        TimeSpan fireTimex2;
        TimeSpan fireTimex5;
        
        // The sound that is played when a laser is fired
        SoundEffect laserSound;
        SoundEffect explosionSound;
        Song gameplayMusic;
        Song cryingSound;
        SoundEffect explosionSound2;

        //Number that holds the player score
        int score, lastScore, missileCount, missileTimer, difficultyTimer;

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
            state = gameState.startScreen;
            drawer = new Drawer();
            collision = new Collision();

            //Set player's score to zero
            score = 0;
            missileCount = 3;
            missileTimer = 60;
            difficultyTimer = 0;
            difficultyFactor = 1.0f;

            projectiles = new List<Projectile>();

            missiles = new List<Missile>();

            // Set the laser to fire every quarter second
            fireTime = TimeSpan.FromSeconds(.15f);

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
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y
            + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, playerPosition);

            // Load the parallaxing background
            bgLayer1.Initialize(Content, "bglayer1", GraphicsDevice.Viewport.Width, -1);
            bgLayer2.Initialize(Content, "bglayer2", GraphicsDevice.Viewport.Width, -2);

            enemyTexture = Content.Load<Texture2D>("smallShip");
            heavyEnemyTexture = Content.Load<Texture2D>("bigShip");

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

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

                    state=gameState.playing;
                }
            }
            else
            {
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();
                difficultyTimer++;
                if (difficultyTimer / 1000 == 1)
                {
                    difficultyFactor += 0.1f;
                    difficultyTimer = 0;
                }
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

                // Update the collision
                collision.UpdateVariables(enemies, heavyEnemies, projectiles, player, missiles);
                collision.collision();

                // Update the projectiles
                UpdateProjectiles(gameTime);

                // Update the missiles
                UpdateMissiles();

                //Check for missiles
                CheckforMissile();

                // Update the explosions
                UpdateExplosions(gameTime);
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
                spriteBatch.DrawString(font, "Press enter or A on gamepad to start", new Vector2((GraphicsDevice.Viewport.Width-310)/3, GraphicsDevice.Viewport.Height-280), Color.White);
                spriteBatch.End();
            }
            else if (state==gameState.playing)
            {
                Color color;

                GraphicsDevice.Clear(Color.CornflowerBlue);
                drawer.UpdateVariables(enemies, heavyEnemies, projectiles, explosions,
                    spriteBatch, player, mainBackground, bgLayer1, bgLayer2, missiles);
                // Start drawing
                drawer.DrawAll();
                // Draw the score
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
                spriteBatch.DrawString(font, "Missiles: " + missileCount, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X+300, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);


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
            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
            currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                player.Position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
            currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                player.Position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) ||
            currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                player.Position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) ||
            currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                player.Position.Y += playerMoveSpeed;
            }

            // Make sure that the player does not go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);



            //Last resort, double fiire rate
            if (player.Health <= 20)
            {
                fireTime = fireTimex2;
            }

            //Last resort, stupid fire rate
            if (player.Health <= 5)
            {
                fireTime = fireTimex5;
            }

            // reset score if player health goes to zero
            if (player.Health <= 0)
            {
                lastScore = score;

                //Play the crying man
                CryingSound(cryingSound);
                score = 0;
                player.Health = 100;
                state = gameState.startScreen;
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
                        explosionSound.Play();

                        score += ((int)enemies[i].Value);

                    }

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
                        explosionSound2.Play();

                        score += ((int)heavyEnemies[i].Value) ;

                    }

                    heavyEnemies.RemoveAt(i);
                }
            }
        }

        //==============================================================================================================================

        private void AddProjectile(Vector2 position)
        {
            Projectile projectile = new Projectile();
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


        private void CheckforMissile()
        {

            if (missileTimer != 60)
                missileTimer++;
            if (missileTimer >= 60)
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
                        missileTimer = 0;
                    }
                }
            }
        }

    }
}
