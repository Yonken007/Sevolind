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


        public PhysicalObject(Texture2D texture, float X, float Y, float speedX, float speedY ) : base(texture, X, Y, speedX, speedY) {} // konstruktor

        public bool CheckCollision(PhysicalObject other) // kontrollerar när motståndaren och spelarn krockar
        {

            Rectangle myrect = new Rectangle(Convert.ToInt32(X), // här jämförs två rektanglar för att hitta var de korsar de varandra. 
                Convert.ToInt32(Y), 32, 32);                    // det uppstod ett problem med det då det förut stod texture.width och texture.height
                                                                // problemet var att bilderna var för stora. En ful lösningar var att manuellt ställa in kolliders för objekten. 
            Rectangle otherRect =                               // men bör ändras om olika fiender lägs till i spelet. 
                 new Rectangle(Convert.ToInt32(other.X),
                 Convert.ToInt32(other.Y), 32,32);
            return myrect.Intersects(otherRect);

        }


        public bool IsAlive // Egenskaper för om ett objekt är levande
        {

            get { return isAlive; }
            set { isAlive = value; }

        }
               
    }
}
