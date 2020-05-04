using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    class GameElements
    {

        static Player player;
        static List<Enemys> enemies;
        static Menu menu;
        static Camera camera;
        static Map map;
        static Texture2D bakground;
        static Texture2D goal;      
        static PrintText printtext;
      
        // olika gamesates
        public enum State { Menu, Run, HighScore, Quit };
        public static State currentState;


        public static void Initialize()
        {
            map = new Map();
            

        }

        public static void LoadContent(ContentManager Content, GameWindow window, GraphicsDevice graphics)
        {
            printtext = new PrintText(Content.Load<SpriteFont>("PrintScore"));
            menu = new Menu((int)State.Menu);
            menu.AddItem(Content.Load<Texture2D>("menu/start"), (int)State.Run);
            menu.AddItem(Content.Load<Texture2D>("menu/highscore"), (int)State.HighScore);
            menu.AddItem(Content.Load<Texture2D>("menu/exit"), (int)State.Quit);
            bakground = (Content.Load<Texture2D>("menu/bakground"));

            player = new Player(Content.Load<Texture2D>("Player"), 30, 700, 4.5f, 4.5f);
            map = new Map();
            camera = new Camera(graphics.Viewport);


            goal = Content.Load<Texture2D>("goal");

            enemies = new List<Enemys>();           
            Texture2D tmpSprite = Content.Load<Texture2D>("mushroom");
            Mushroom temp = new Mushroom(tmpSprite, 1050, 650);


            Mushroom temp2 = new Mushroom(tmpSprite, 760, 580);
            enemies.Add(temp);
            enemies.Add(temp2);



            Tiles.Content = Content;
            map.Generate(new int[,] {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,6,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,2,2},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,1,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,4,5,6,0,0,0,0,0,0,2,1,1,1,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,2,0,0,2,2,0,0,0,0,0,2,2,2,2,2,1,1,1,1,0,0,0,0,0,0,0,0},
                { 2,2,2,2,2,1,0,0,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0},
                { 1,1,1,1,1,1,0,0,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0}

            }, 64);

                       
        }

        public static State MenuUpdate(GameTime gameTime)
        {
            return (State)menu.Update(gameTime);
        }

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bakground,new Rectangle(0,0,870,480), Color.White);
            menu.Draw(spriteBatch);
        }

        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {

            player.Update(gameTime);

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);
                camera.Update(player.Vector, map.Width, map.Height);

            }
              if (!player.IsAlive) //Spelaren död
              {
                  Reset(window, content);
                  return State.Menu;//Återgå till menyn
              }

            if (player.Wongame) // om man har vunnit spelet
            {
               

            }
              

            foreach (Enemys e in enemies.ToList())
            {
                if (true)//e.IsAlive) // kontrollera om fienden lever
                {
                    


                    if (e.CheckCollision(player)) // dödar spelaren om man kolliderar med en motståndare
                    player.IsAlive = false;
                    e.Update(window);
                }
            }

                    
            return State.Run;
        }
        
        public static void RunDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                                  BlendState.AlphaBlend,
                                  null, null, null, null,
                                  camera.Transform);         
            player.Draw(spriteBatch);
            map.Draw(spriteBatch);
            spriteBatch.Draw(goal, new Rectangle(1990, 400, 64, 64), Color.White); // ritar ut en flaga vid slutet av kartan
            foreach (Enemys e in enemies)
                e.Draw(spriteBatch);


            
            printtext.Print("Your time " + (float)player.Time, spriteBatch, (int)player.X+20, (int)player.Y-50);

        }


        private static void Reset(GameWindow window, ContentManager content)
        {


            player.Reset(30, 700, 4.5f, 4.5f);


        }


        public static State HighScoreUpdate()
        {

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                return State.Menu;

            return State.HighScore; // Stanna kvar i HighScore
        }

        public static void HighScoreDraw(SpriteBatch spriteBatch) //Här ritas highscorelistan ut
        {

            spriteBatch.Begin();


            printtext.Print("Time " + (float)player.Time, spriteBatch, 20, 50);

            // spara alla tider som en lista och skriv sedan ut listan





        }


    }
}
