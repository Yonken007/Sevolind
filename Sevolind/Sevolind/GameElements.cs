using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
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
        static StreamreaderAndWriter streamreaderwriter;
        static bool havecalled = false;
       


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
            menu.AddItem(Content.Load<Texture2D>("menu/exit"), (int)State.Quit); ;
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

            streamreaderwriter = new StreamreaderAndWriter();
            map.Generate(streamreaderwriter.StreamreaderMap(), 64); // här genereraskartan utifrån en textfil som är skriven i debugen. DU ändrar kartan genom att ändra väderna i textfilen


            streamreaderwriter.LoadHighscore10();

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

            if (!havecalled)
            {

                player.GameStart(gameTime.TotalGameTime.TotalSeconds);
                havecalled = true;
            }

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



                streamreaderwriter.StreamWriterhighscore10(player.LastTime);

                Reset(window, content);
                return State.Menu;
            }
              

            foreach (Enemys e in enemies.ToList())
            {
                    if (e.CheckCollision(player)) // dödar spelaren om man kolliderar med en motståndare
                    player.IsAlive = false;
                    e.Update(window);
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
            if (keyboardState.IsKeyDown(Keys.B))
                return State.Menu;

            



            return State.HighScore; // Stanna kvar i HighScore
        }

        public static void HighScoreDraw(SpriteBatch spriteBatch) //Här ritas highscorelistan ut
        {

            spriteBatch.Begin();
            
            printtext.Print("....To go back to menu press b ", spriteBatch, 10, 10);
            printtext.Print("Your best highscores: ", spriteBatch, 50, 80);
            printtext.Print("Your latest 10 scores: ", spriteBatch, 400, 80);

            int spacebetweenHighscore = 30;
            

            foreach (string n in streamreaderwriter.Highscore10)
            {
                printtext.Print(n, spriteBatch, 400,80+spacebetweenHighscore );
                spacebetweenHighscore += 30;

            }



            // spara alla tider som en lista och skriv sedan ut listan

           



        }

        public static bool Havecalled
        {
            set { havecalled = value; }

        }


    }
}
