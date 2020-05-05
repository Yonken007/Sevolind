using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    class Map
    {
        private int width, height;
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        public Map() { }

        public void Generate(int[,] map, int size)// Här genereras kartan genom att ta in ett värde av en two dimitionell array och en nästlad for loop. 
        {
            for(int x = 0; x < map.GetLength(1); x++)
                for(int y = 0; y < map.GetLength(0); y++)
                {

                    int number = map[y, x];
                    if (number > 0)
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));// här hämtar den tilsen från den tidigare klassen och ställer in storleken på rectangeln. 

                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
      
        }
      
        public void Draw (SpriteBatch spriteBatch)// ritar ut kartan från en Lista
        {
            foreach (CollisionTiles tile in collisionTiles)
                tile.Draw(spriteBatch);

        }

        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }


    }
}
