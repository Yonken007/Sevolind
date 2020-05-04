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
        private Rectangle rectangle;       

        private bool hasJumped = false;
        private bool wongame = false;
        double time = 0;




        public Player(Texture2D texture, float X, float Y, float speedX, float speedY): base(texture, X, Y, speedX, speedY)
        {
           

        }

       

        public void Reset (float X, float Y, float speedX, float speedY)
        {

            //återställ spelaren position och hastighet:
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;         
            //gör så att spelaren lever igen:
            IsAlive = true;


        }

                     

             

        public void Update(GameTime gameTime)
        {
            vector += speed;
            rectangle = new Rectangle((int)vector.X, (int)vector.Y, 64,64 );

            Input(gameTime);

            if (speed.Y < 10)
                speed.Y += 0.4f;

            
            if(IsAlive == false)
            {

                gameTime.TotalGameTime.TotalSeconds 0;
            }

            else { time = - time; }
            

        }

        private void Input(GameTime gameTime) // Här läser spelet in hur karaktären ska röra sig efter tangentbordet. 
        {

            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.D))
                speed.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else if (keyboard.IsKeyDown(Keys.A))
                speed.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else speed.X = 0f;

            if(keyboard.IsKeyDown(Keys.Space) && !hasJumped)
            {

                vector.Y -= 7f;
                speed.Y = -9f;
                hasJumped = true;
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset) // kollar när player har kommit tillbaka till marken
        {
            if (rectangle.TouchTopOf(newRectangle))
            {

                rectangle.Y = newRectangle.Y - rectangle.Height;
                speed.Y = 0f;
                hasJumped = false;
                

            }

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
                IsAlive = false;// När man har vunnit spelet

            }
                
            if (vector.Y < 0) speed.Y = 1f; // om karaktären nuddar toppen av skärmen
            if (vector.Y > yOffset - rectangle.Height) // om karaktären nudar botten av skärmen
            {
                IsAlive = false;// ramlar ner i ett hål och dör
                vector.Y = yOffset - rectangle.Height;
            }                    



        }


        public double Time
        {

            get { return time; }
        }

        public bool Wongame
        {
            get { return wongame; }

        }

        public override void Draw(SpriteBatch spriteBatch) // rita ut karaktären
        {
            spriteBatch.Draw(texture, rectangle, Color.White);


        }

    }

}
