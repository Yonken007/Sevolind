using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    class PrintText

    {
        SpriteFont font;

        public PrintText(SpriteFont font) // konstruktor
        {

            this.font = font;

        }


        public void Print(string text, SpriteBatch spriteBatch, int X, int Y)// draw metoden för hur texten ska skrivas ut på skärmen
        {

            spriteBatch.DrawString(font, text, new Vector2(X, Y), Color.White);
            

        }

       

    }
}
