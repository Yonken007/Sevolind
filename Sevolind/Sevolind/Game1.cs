﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sevolind
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;

        Map map;        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameElements.currentState = GameElements.State.Menu;
            GameElements.Initialize();           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice); 
            GameElements.LoadContent(Content, Window);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Stänger av spelet om man trycker på back-knappen på gamepaden:
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            switch (GameElements.currentState)
            {

                case GameElements.State.Run: // kör själva spelet
                    GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                    break;

                case GameElements.State.HighScore: // Highscorelistan
                    GameElements.currentState = GameElements.HighScoreUpdate();
                    break;

                case GameElements.State.Quit: // avsluta spelet
                    this.Exit();
                    break;

                default: // Menu
                    GameElements.currentState = GameElements.MenuUpdate(gameTime);
                    break;

            }

            /*
            foreach (CollisionTiles tile in map.CollisionTiles)
                player.Collision(tile.Rectangle, map.Width, map.Height);
                */

            base.Update(gameTime);

        }                   

          
 

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (GameElements.currentState)
            {

                case GameElements.State.Run://kör själva spelet
                    GameElements.RunDraw(spriteBatch);
                    break;

                case GameElements.State.HighScore:// highscore listan
                    GameElements.HighScoreDraw(spriteBatch);
                    break;

                case GameElements.State.Quit: // avsluta spelet
                    this.Exit();
                    break;

                default: // Menyn
                    GameElements.MenuDraw(spriteBatch);
                    break;

            }
                       
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
    
}
