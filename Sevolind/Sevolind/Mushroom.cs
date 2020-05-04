using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    class Mushroom : Enemys // är inte en svamp borde vara slime
    {

        private Rectangle rectangle;

        public Mushroom(Texture2D texture, float X, float Y) : base(texture, X, Y, 2.5f, 0.3f) { }

        public override void Update(GameWindow window)
        {

            rectangle = new Rectangle((int)vector.X, (int)vector.Y, 64, 64);

            //flytta på fienden
            vector.X += speed.X;
            //Kontroller så den inte åker utanför fönstret på sidorna 
            if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
                speed.X *= -1;

            vector.Y += speed.Y;

            if (vector.Y > window.ClientBounds.Height)
                IsAlive = false;



        }

        public override void Draw(SpriteBatch spriteBatch) // rita ut karaktären
        {
            spriteBatch.Draw(texture, rectangle, Color.White);


        }

       

    }
}
