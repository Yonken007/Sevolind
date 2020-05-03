using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    class Menu
    {

        List<MenuItem> menu; // Lista på menuItems
        int selected = 0;

        //currenHeight anväbds för att rita ut menyItems på olika höjd
        float currentHeight = 0;
        //lastCahnge används för att "pausa" tangentrycktningar så att det inte ska gå för fort att bläddra bland menyvalen:
        double lastChange = 0;
        // det state som representerar själva menyn
        int defaultMenuState;


        public Menu(int defaultMenuState)
        {
            menu = new List<MenuItem>();
            this.defaultMenuState = defaultMenuState;
        }

        public void AddItem(Texture2D itemTexture, int state)
        {

            //sätt höjd på item:
            float X = 0;
            float Y = 0 + currentHeight;

            //ändra currentHeight efter detta items höjd på + 20 pixlar för lite extra mellanrum:
            currentHeight += itemTexture.Height + 20;

            // skapa ett temporärt objekt och lägg det i listan:
            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add(temp);

        }

        public int Update(GameTime gameTime)
        {
            //läs in tangentryckningar 
            KeyboardState keyboardState = Keyboard.GetState();

            //byte mellan olika menyval. Först måste vi dock kontrollera så att användaren inte precis nyligen bytte menyval. Vi vill ju inte att det ska ändras 30 eller 60 gåbger per sekund
            //därför pausar vi i 130 milisekunder:

            if (lastChange + 130 < gameTime.TotalGameTime.TotalMilliseconds)
            {
                //gå ett steg ned i menyn
                if (keyboardState.IsKeyDown(Keys.Down))
                {

                    selected++;
                    //Om vi har gått utanför de möjliga valen så vill vi att det första menyvalet ska väljar.
                    if (selected > menu.Count - 1)
                        selected = 0; // det sista menyvalet

                }

                // ställ lastChange till exakt detta ögonblick:
                lastChange = gameTime.TotalGameTime.TotalMilliseconds;

            }

            //välj ett menyval med ENTER
            if (keyboardState.IsKeyDown(Keys.Enter))
                return menu[selected].State; // retunera menyvalets state

            // om inget menyval har valts, så stannar vci kvar i menyn

            return defaultMenuState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < menu.Count; i++)
            {

                // om vi har ett aktivit menyval ritar vi ut det med en speciell färgtoning:
                if (i == selected)
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.RosyBrown);
                else // annars ingen färgtoning alls:
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.White);

            }

        }

    }

    class MenuItem
    {

        Texture2D texture; // bilden för menyvalet
        Vector2 position; // positionen för menyvalet
        int currentState; // mentvalets state


        public MenuItem(Texture2D texture, Vector2 position, int currentState)
        {

            this.texture = texture;
            this.position = position;
            this.currentState = currentState;


        }


        public Texture2D Texture { get { return texture; } }
        public Vector2 Position { get { return position; } }
        public int State { get { return currentState; } }





    }

}

