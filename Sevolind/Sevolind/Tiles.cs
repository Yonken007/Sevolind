using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    class Tiles 
    {
        protected Texture2D texture;

        private Rectangle rectangle; // reltangeln för de olika blocken
        
        private static ContentManager content;
        

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);// här är draw medtoden för 

        }

        public static ContentManager Content // Egenskaper
        {
            protected get { return content; }
            set { content = value; }
        }

        public Rectangle Rectangle // egenskaper
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

    }

    class CollisionTiles : Tiles
    {

        public CollisionTiles(int i, Rectangle newRectangle)// här laddas texturen in i programmet. Fördelen är att den här kan ladda in flera olika textures i samma klass. 
        {                                                   // Detta gör att alla mina tiles laddas in här och får samma egenskaper.
            texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;

        }

    }


}
