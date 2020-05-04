using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    
    public class Camera
    {
       
        public Matrix Transform { get; private set; }

        public void Follow(Player target)
        {

            var offset = Matrix.CreateTranslation(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2, 0);
            var position = Matrix.CreateTranslation(
                -target.Vector.X - (target.rectangle.Width / 2),
                -target.Vector.Y - (target.rectangle.Height / 2),
                0);

            Transform = position * offset;

        }






















        /*
        private Matrix transform;
        public Matrix Transform
        {

            get { return transform; }
        }

        private Vector2 centre;
        private Viewport viewport;

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;

        }

        public void Update (Vector2 position, int xOffset, int yOffset)
        {
            if (position.X < viewport.Width / 2)
                centre.X = viewport.Width / 2;
            else if (position.X > xOffset - (viewport.Width / 2))
                centre.X = xOffset - (viewport.Width / 2);
            else centre.X = position.X;

            if (position.Y < viewport.Height / 2)
                centre.Y = viewport.Height / 2;
            else if (position.Y > yOffset - (viewport.Height / 2))
                centre.Y = yOffset - (viewport.Height / 2);
            else centre.Y = position.Y;
            
           transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2),
                                                            -centre.Y + (viewport.Height / 2), 0));
        }

    */
    }
}
