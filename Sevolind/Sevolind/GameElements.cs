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
        // statiska medlemsvariabler
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

        // Det som sker i klassen att nästan alla textures och värden laddas in för de olika objekten
        public static void LoadContent(ContentManager Content, GameWindow window, GraphicsDevice graphics)
        {
            printtext = new PrintText(Content.Load<SpriteFont>("PrintScore"));              // laddar in spritefonten
            menu = new Menu((int)State.Menu);                                               // skapar ett obejekt av menu klassen som skickar in vilket state som spelet är i
            menu.AddItem(Content.Load<Texture2D>("menu/start"), (int)State.Run);            // menu knappen
            menu.AddItem(Content.Load<Texture2D>("menu/highscore"), (int)State.HighScore);  // highscore kanppen
            menu.AddItem(Content.Load<Texture2D>("menu/exit"), (int)State.Quit); ;          // exit knappen
            bakground = (Content.Load<Texture2D>("menu/bakground"));                        // bakgrunden
            goal = Content.Load<Texture2D>("goal");                                         // laddar in flaggan i slutet av spelet


            player = new Player(Content.Load<Texture2D>("Player"), 30, 700, 4.5f, 4.5f);    // laddar in spelarn 
            map = new Map();                                                                // skapar ett obejekt av kartan
            camera = new Camera(graphics.Viewport);                                         // skapar ett obejekt av kameran


            

            enemies = new List<Enemys>();                                                   // skapar en lista för de olika fienderna
            Texture2D tmpSprite = Content.Load<Texture2D>("mushroom");                      //laddar in fienden
            Mushroom temp = new Mushroom(tmpSprite, 1050, 650);                             // skapar två nya fieneder vid speciella koordinater
            Mushroom temp2 = new Mushroom(tmpSprite, 760, 580);
            enemies.Add(temp);                                                              // lägger till dem i listan
            enemies.Add(temp2);



            Tiles.Content = Content;                                                        // hämtar rätt content från tileklassen

            streamreaderwriter = new StreamreaderAndWriter(); // obejektet av streamreaderandwriter klassen 
            map.Generate(streamreaderwriter.StreamreaderMap(), 64); // här genereraskartan utifrån en textfil som är skriven i debugen. DU ändrar kartan genom att ändra väderna i textfilen

            // kallar på metoder som läser in highscore listan i spelet från textfiler
            streamreaderwriter.LoadHighscore10();
            streamreaderwriter.LoadBestHighscore();

        }

        public static State MenuUpdate(GameTime gameTime) // upptaterar menyn, men för tillfälet returnerar den endast samma värde
        {
            return (State)menu.Update(gameTime);
        }

        public static void MenuDraw(SpriteBatch spriteBatch)// ritar vad som ska finnas i menyn
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bakground,new Rectangle(0,0,870,480), Color.White);            // ritar bakgrunden för menyn
            menu.Draw(spriteBatch);
                      


        }

        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime) // den metoden tar hand om all logic i spelet. 
        {

            if (!havecalled)                                      // det här if statement kontrollerar när användaren trycker på start så timern inte blir fel. 
            {                                                     // Eftersom updaten går när du är i highscore också. Därför behövs den här. 

                player.GameStart(gameTime.TotalGameTime.TotalSeconds);
                havecalled = true;
            }

            player.Update(gameTime);                              // upptaterar spelaren efter speltiden

            foreach (CollisionTiles tile in map.CollisionTiles)   // här kallas metoderna som kontrollerar karmeran och kolliders mellan spelaren och tilesen
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);
                camera.Update(player.Vector, map.Width, map.Height);

            }
              if (!player.IsAlive)                               //Spelaren död
              {
                  Reset(window, content);                       // spelet resetas om man dör
                  return State.Menu;                            //Återgå till menyn
              }

            if (player.Wongame)                                 // om man har vunnit spelet
            {

                streamreaderwriter.StreamWriterhighscore10(player.LastTime); // tiden som spelaren har spelat skickas både till besthighscore och vanliga highscore listan för att checkas. 
                streamreaderwriter.StremWriterbesthighscore(player.LastTime);
                Reset(window, content);                                     // spelet resetas och går tillbaka till menyn
                return State.Menu;
            }
              

            foreach (Enemys e in enemies.ToList()) // här upptaterar spelet för att kolla om spelaren har kolliderat med en fiende
            {
                    if (e.CheckCollision(player)) // dödar spelaren om man kolliderar med en motståndare
                    player.IsAlive = false;     // om den har kolliderat ska spelaren dö
                    e.Update(window);           // därefter upptateras fienderna i fönstret
            }

                    
            return State.Run;
        }
        
        public static void RunDraw(SpriteBatch spriteBatch) // här ritas allting i nästan allting i spelet ut
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, // kameran läggs till i spritebatch.begin för att upptatera hur skärmen ska se ut när allt ritas ut
                                  BlendState.AlphaBlend,
                                  null, null, null, null,
                                  camera.Transform);         
            player.Draw(spriteBatch); // spelaren ritas ut
            map.Draw(spriteBatch);    // kartan ritas ut
            spriteBatch.Draw(goal, new Rectangle(1990, 400, 64, 64), Color.White); // ritar ut en flaga vid slutet av kartan
            foreach (Enemys e in enemies)       // fienderna ritas ut
                e.Draw(spriteBatch);

            printtext.Print("Your time " + (float)player.Time, spriteBatch, (int)player.X+20, (int)player.Y-50); // här skriv tiden ut under spelets gång. tiden följer med spelarens position

        }


        private static void Reset(GameWindow window, ContentManager content)
        {
            player.Reset(30, 700, 4.5f, 4.5f); // spelaren resetas

        }


        public static State HighScoreUpdate() // inget i highscore behöver upptateras du logiken sker i streamreader och writer.
        {

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.B))
                return State.Menu;

            return State.HighScore; // Stanna kvar i HighScore
        }

        public static void HighScoreDraw(SpriteBatch spriteBatch) //Här ritas highscorelistan ut
        {

            spriteBatch.Begin();
            
            printtext.Print("....To go back to menu press b ", spriteBatch, 10, 10); // Här skrivs vanlig text ut
            printtext.Print("Your best highscores: ", spriteBatch, 50, 80);
            printtext.Print("Your latest 10 scores: ", spriteBatch, 400, 80);

            int spacebetweenHighscoreallscores = 30; // dessa två variabler ser till att det är mellanrum när tiderna skrivs ut
            int spaceBetweenHighscoresBestScores = 30;

            foreach (string n in streamreaderwriter.Highscore10)
            {
                printtext.Print(n, spriteBatch, 400,80+spacebetweenHighscoreallscores ); // här skrivs alla highscores ut
                spacebetweenHighscoreallscores += 30;

            }

            foreach(double n in streamreaderwriter.BestHighscore)
            {
                printtext.Print(n.ToString(), spriteBatch, 50, 80 + spaceBetweenHighscoresBestScores); // här skrivs de bästa highscoresen ut
                spaceBetweenHighscoresBestScores += 30;

            }

        }

        public static bool Havecalled // egenskaper
        {
            set { havecalled = value; }

        }


    }
}
