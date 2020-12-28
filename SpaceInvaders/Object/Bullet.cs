using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Object
{
    class Bullet : Object
    {
        public Bullet(Player player)
        {
            scale = player.scale;
            speed = 7.0F;
            sprite = Game1.bulletSprite; ;
            position.X = player.position.X + (player.sprite.Width / player.scale) / 2;
            position.Y = player.position.Y;
        }
        public void Forward()
        {
            position.Y -= speed;
        }
        public void CheckEnemy(Enemy enemy)
        {
            if (active)
            {
                if (enemy.active)
                {
                    if (GetRectangle().Intersects(enemy.GetRectangle()))
                    {
                        enemy.active = false;
                        active = false;
                    }
                }
            }
        }
    }
}
