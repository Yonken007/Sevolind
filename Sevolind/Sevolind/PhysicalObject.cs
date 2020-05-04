using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
   abstract class PhysicalObject : MovingObject 
    {

        private bool isAlive = true; // Lägger till variabel när spelare eller motståndare dör


        public PhysicalObject(Texture2D texture, float X, float Y, float speedX, float speedY ) : base(texture, X, Y, speedX, speedY)
        {
        }

        public bool CheckCollision(PhysicalObject other) // kontrollerar när motståndaren och spelarn krockar
        {

            Rectangle myrect = new Rectangle(Convert.ToInt32(X),
                Convert.ToInt32(Y), 32, 32);

            Rectangle otherRect =
                 new Rectangle(Convert.ToInt32(other.X),
                 Convert.ToInt32(other.Y), 32,32);
            return myrect.Intersects(otherRect);

        }


        public bool IsAlive // Egenskaper
        {

            get { return isAlive; }
            set { isAlive = value; }

        }
               
    }
}
