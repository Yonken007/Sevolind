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
        static Map map;
        static CollisionTiles hap;

        // olika gamesates
        public enum State { Menu, Run, HighScore, Quit };
        public static State currentState;


        public static void Initialize()
        {
           

        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {

            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("menu/start"), (int)State.Run);
            menu.AddItem(content.Load<Texture2D>("menu/highscore"), (int)State.HighScore);
            menu.AddItem(content.Load<Texture2D>("menu/exit"), (int)State.Quit);

            player = new Player(content.Load<Texture2D>("player"), 64, 150, 4.5f, 4.5f);
            map = new Map();
            

            Tiles.Content = content;
            map.Generate(new int[,] {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}

            }, 32);


        }

        public static State MenuUpdate(GameTime gameTime)
        {
            return (State)menu.Update(gameTime);
        }

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }

        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {

            player.Update(window, gameTime);


            if (!player.IsAlive) //Spelaren död
            {


                Reset(window, content);
                return State.Menu;//Återgå till menyn

            } 

            return State.Run;
        }

        public static void RunDraw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);

        }


        private static void Reset(GameWindow window, ContentManager content)
        {


            player.Reset(64, 150, 4.5f, 4.5f);


        }


        public static State HighScoreUpdate()
        {

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                return State.Menu;

            return State.HighScore; // Stanna kvar i HighScore
        }

        public static void HighScoreDraw(SpriteBatch spriteBatch)
        {


            // Rite ut highscore listan:  




        }


    }
}
