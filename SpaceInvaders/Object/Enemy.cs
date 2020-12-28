using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Object
{
    class Enemy : Object
    {
        public int step;
        private int stepNum = 50;
        public Enemy(Texture2D _sprite, Vector2 _position, float _speed)
        {
            step = 0;
            speed = _speed;
            scale = 50;
            sprite = _sprite;
            position = _position;
            stepNum = 50 - (int)(speed * 2);
            active = true;
        }
        public void MoveX(int direction)
        {
            position.X += (direction) * speed;
        }
        public void MoveY()
        {
            position.Y += speed;
        }
        public void CheckGameOver(Player player)
        {
            if (active)
            {
                if (position.Y >= player.position.Y - 20)
                {
                    Game1.level = 1;
                    Game1.speed = 3.0f;
                    Game1.mode = "Game Over";
                    Game1.CreateEnemyArray();
                }
            }
        }
    }
}
