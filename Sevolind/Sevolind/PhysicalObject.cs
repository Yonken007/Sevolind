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

        private bool isAlive = true;


        public PhysicalObject(Texture2D texture, float X, float Y, float speedX, float speedY ) : base(texture, X, Y, speedX, speedY)
        {
        }


        public bool IsAlive
        {

            get { return isAlive; }
            set { isAlive = value; }

        }
               
    }
}
