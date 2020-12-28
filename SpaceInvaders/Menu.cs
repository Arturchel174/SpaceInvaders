using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    static class Menu
    {
        private static bool menuChecker = true;
        public static void UI(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.text, "Начать игру", new Vector2(Game1.width / 2 - 75, Game1.height / 2 - 25), Color.White);
            spriteBatch.DrawString(Game1.text, "Выход", new Vector2(Game1.width / 2 - 75, Game1.height / 2 + 25), Color.White);
            if (Game1.menuState == 1)
                spriteBatch.DrawString(Game1.text, ">", new Vector2(Game1.width / 2 - 100, Game1.height / 2 - 25), Color.White);
            if (Game1.menuState == 2)
                spriteBatch.DrawString(Game1.text, ">", new Vector2(Game1.width / 2 - 100, Game1.height / 2 + 25), Color.White);
        }
        public static int Controller()
        {
            KeyboardState keyState = Keyboard.GetState();
            if ((keyState.IsKeyDown(Keys.Down)))
            {
                if (menuChecker)
                {
                    menuChecker = false;
                    Game1.menuState += 1;
                    if (Game1.menuState == 3)
                        Game1.menuState = 1;
                }
            }
            else
            {
                if ((keyState.IsKeyDown(Keys.Up)))
                {
                    if (menuChecker)
                    {
                        menuChecker = false;
                        Game1.menuState -= 1;
                        if (Game1.menuState == 0)
                            Game1.menuState = 2;
                    }
                }
                else
                    menuChecker = true;
            }
            if (keyState.IsKeyDown(Keys.Enter))
            {
                if (Game1.menuState == 1)
                    Game1.mode = "Game";
                if (Game1.menuState == 2)
                    return 1;
                menuChecker = true;
            }
            return 0;
        }

    }
}
