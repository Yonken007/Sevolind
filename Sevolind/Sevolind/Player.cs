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
   class Player : PhysicalObject
    {
        // medlems variabler
        private Rectangle rectangle;    
        private bool hasJumped = false;
        private bool wongame = false;
        double time = 0;
        double deltaTime = 0;
        double lastTime = 0;
        double gamestarttime = 0;
      

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY): base(texture, X, Y, speedX, speedY){}

        public void Reset (float X, float Y, float speedX, float speedY) // ändrar tillbaka alla värden när spelet resetas
        {

            //återställ spelaren position och hastighet:
            vector.X = X;
            vector.Y = Y; 
            speed.X = speedX;
            speed.Y = speedY;         
            //gör så att spelaren lever igen:
            IsAlive = true;
            time = 0;
            GameElements.Havecalled = false; 
            lastTime = 0;
            wongame = false;

        }

        public void GameStart(double time)
        {
            gamestarttime = time;

        }     

        public void Update(GameTime gameTime)// uppdaten redigerar rectanglen och upptaterar vilket input som kommer från användaren, Därefter beräknas också tiden här. 
        {
            vector += speed;
            rectangle = new Rectangle((int)vector.X, (int)vector.Y, 64,64 ); // spelarens rektangel

            Input(gameTime); // keyboard input från användaren

            if (speed.Y < 10) {speed.Y += 0.4f; } // lägger till gravitation i spelet
                

            deltaTime = gameTime.TotalGameTime.TotalSeconds - lastTime - gamestarttime; 
            lastTime = gameTime.TotalGameTime.TotalSeconds - gamestarttime;// beräknar tiden från förra tiden man spelade

               time += deltaTime; // tiden för hur lång tid det tog att klara spelet

        }


        private void Input(GameTime gameTime) // Här läser spelet in hur karaktären ska röra sig efter tangentbordet. 
        {

            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.D))
                speed.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3; // rör sig till göer 
            else if (keyboard.IsKeyDown(Keys.A))
                speed.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3; // rör sig till vänster
            else speed.X = 0f;

            if(keyboard.IsKeyDown(Keys.Space) && !hasJumped) // lägger till att hoppa i spelet
            {

                vector.Y -= 7f;
                speed.Y = -9f;
                hasJumped = true;
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset) // kollar när player har kommit tillbaka till marken
        {
            if (rectangle.TouchTopOf(newRectangle)) // kollar när spelaren har hoppat och sätter hatigheten upp till noll så gravitationen drar ner spelaren. 
            {                                       // Du kan hoppa igen när du kommer ner till marken
                rectangle.Y = newRectangle.Y - rectangle.Height;
                speed.Y = 0f;
                hasJumped = false;
            }

            // Dessa if statements sätter ett värde på spelarens rektangel beroende på om spelaren nuddar någon av sidorna. Så spelaren inte åker utanför spelskärmen
            if (rectangle.TouchLeftOf(newRectangle))
            {

                vector.X = newRectangle.X - rectangle.Width -2;

            }
            if (rectangle.TouchRightOf(newRectangle))
            {

                vector.X = newRectangle.X + newRectangle.Width + 2;
            }

            if (rectangle.TouchBottomOf(newRectangle))
            {                               
               vector.Y = 1f;
            }

            if (vector.X < 0) vector.X = 0;//om karaktären nuddar till vänster om skärmen
            if (vector.X > xOffset - rectangle.Width)
            {
                vector.X = xOffset - rectangle.Width; // om karaktären nuddar skrämen till höger
               wongame = true;// När man har vunnit spelet

            }
                
            if (vector.Y < 0) speed.Y = 1f; // om karaktären nuddar toppen av skärmen
            if (vector.Y > yOffset - rectangle.Height) // om karaktären nudar botten av skärmen
            {
                IsAlive = false;// ramlar ner i ett hål och dör
                vector.Y = yOffset - rectangle.Height;
            }                    



        }

        public override void Draw(SpriteBatch spriteBatch) // värderna för att rita ut karaktären
        {
            spriteBatch.Draw(texture, rectangle, Color.White);

        }
        
        public double LastTime      // egenskaper
        {
            get { return lastTime; }
        }
        public double Time 
        {
            get { return time; }
        }

        public bool Wongame
        {
            get { return wongame; }
        }

    }

}
