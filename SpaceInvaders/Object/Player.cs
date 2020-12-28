using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Object
{
    class Player : Object
    {
        //public Bullet bullet;
        public List<Bullet> bulletList;
        private bool isShoot = false;
        public Player(Texture2D _sprite)
        {
            scale = 4;
            speed = 4.0F;
            sprite = _sprite;
            position.X = Game1.width / 2 - sprite.Width / scale;
            position.Y = Game1.height - sprite.Height / scale * 2;
            bulletList = new List<Bullet>();
        }
        public void Controll()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
                if (position.X > 0)
                    position.X -= speed;
            if (keyboardState.IsKeyDown(Keys.Right))
                if (position.X < (Game1.width - (sprite.Width / scale)))
                    position.X += speed;
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (!(isShoot))
                {
                    Shoot();
                    isShoot = true;
                }
            }
            else
            {
                isShoot = false;
            }
        }
        public override void UI(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GetRectangle(), Color.White);
            foreach (var bullet in bulletList)
            {
                bullet.UI(spriteBatch);
            }
        }
        public void Shoot()
        {
            Bullet bullet = new Bullet(this);
            bulletList.Add(bullet);
        }
    }
}
