using Microsoft.Xna.Framework;
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
      
        
    
               
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

     
        protected override void Initialize()
        {
          
            GameElements.currentState = GameElements.State.Menu;         
            GameElements.Initialize();           
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
           
            spriteBatch = new SpriteBatch(GraphicsDevice);        
            GameElements.LoadContent(Content, Window,GraphicsDevice); // kallar på loadcontent i GameElements
            


            // TODO: use this.Content to load your game content here
        }

 
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            //Stänger av spelet om man trycker på back-knappen på gamepaden:
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            switch (GameElements.currentState) // kollar vilket state spelet är i 
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

            
           
                

            base.Update(gameTime);

        } 
       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);                   
                
            
            
            
            switch (GameElements.currentState)
            {
                
                case GameElements.State.Run: //kör själva spelet                                                                 
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
            spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
    
}
