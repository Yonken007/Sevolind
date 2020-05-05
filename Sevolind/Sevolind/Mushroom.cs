using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    class Mushroom : Enemys // Man kanske borde ändra namenet till slime, men tanken var att det skulle vara en annan texture. 
    {

        private Rectangle rectangle;// beskriver 

        public Mushroom(Texture2D texture, float X, float Y) : base(texture, X, Y, 2.5f, 0.3f) { }// om man vill kan man lägga till en förflyttning på motståndaren

        public override void Update(GameWindow window)
        {

            rectangle = new Rectangle((int)vector.X, (int)vector.Y, 64, 64);// ändrar storleken på hur texturen kommer ritas ut på skärmen, utan den här skulle bilden bli mycket större. 

        }

        public override void Draw(SpriteBatch spriteBatch) // rita ut karaktären med vår speciella rectangle.
        {
            spriteBatch.Draw(texture, rectangle, Color.White);


        }

       

    }
}
