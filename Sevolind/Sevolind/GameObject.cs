using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    class GameObject
    {

        protected Texture2D texture;
        protected Vector2 vector;

        public GameObject(Texture2D texture, float X, float Y)
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;

        }


        public Vector2 Vector { get { return vector;} } 
        public float X { get { return vector.X; } }
        public float Y { get { return vector.Y; } }


    }
}
