﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    abstract class Enemys : PhysicalObject
    {
       

        public Enemys(Texture2D texture, float X, float Y, float speedX, float speedY) : base (texture, X, Y, 0f, 0f) // konstuktor
        {
            

        }
                                   


        public abstract void Update(GameWindow window); // update metoden som kommer overridas. Betydligt enklare om man vill lägga till fler fiender


    }
}
