using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    abstract class MovingObject : GameObject // Alla objekt som ska röra på sig. 
    {
        protected Vector2 speed;

        public MovingObject(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y)
        {

            this.speed.X = speedX;
            this.speed.Y = speedY;



        }
               
    }
}
