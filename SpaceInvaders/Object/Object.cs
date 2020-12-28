using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Object
{
    abstract class Object
    {
        public Vector2 position;
        public Texture2D sprite;
        public int scale;
        public float speed;
        public bool active = true;
        public Rectangle GetRectangle()
        {
            Rectangle Rec = new Rectangle((int)position.X, (int)position.Y, sprite.Width / scale, sprite.Height / scale);
            return Rec;
        }
        public virtual void UI(SpriteBatch spriteBatch)
        {
            if (active)
                spriteBatch.Draw(sprite, GetRectangle(), Color.White);
        }
    }
}