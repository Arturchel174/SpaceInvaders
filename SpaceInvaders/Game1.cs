using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Object;
using System.Collections.Generic;

namespace SpaceInvaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static Texture2D playerSprite, bulletSprite, enemySprite;
        public static SpriteFont text;
        public static int width, height;
        private static Player player;
        private static Enemy[,] enemyArray;
        public static int n = 6, m = 4, menuState = 1;
        public static string mode = "Menu";
        public static int level = 1;
        public static float speed = 3.0F;
        public static int direction = 1;
        private void CheckEnemyArray()
        {
            bool flag = true;
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    if (enemyArray[i, j].active)
                        flag = false;
            if (flag == true)
            {
                speed += 1.0f;
                ++level;
                CreateEnemyArray();
            }       
        }
        private static bool CheckEnemyBoarder()
        {
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    if (enemyArray[i, j].active)
                    {
                        if (direction == 1)
                        {
                            if ((enemyArray[i, j].position.X + (enemyArray[i, j].sprite.Width / enemyArray[i, j].scale) + enemyArray[i, j].speed) >= width)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if ((enemyArray[i, j].position.X - enemyArray[i, j].speed) <= 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private static void AllMoveY()
        {
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    enemyArray[i, j].MoveY();
        }
        private static void AllMoveX()
        {
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    enemyArray[i, j].MoveX(direction);
        }
        private static void AllCheckGameOver()
        {
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    enemyArray[i, j].CheckGameOver(player);
        }
        public static void CreateEnemyArray()
        {
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    enemyArray[i, j] = new Enemy(enemySprite, new Vector2(i * 60, j * 50 + 20), speed);
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerSprite = Content.Load<Texture2D>("Player");
            bulletSprite = Content.Load<Texture2D>("Bullet");
            enemySprite = Content.Load<Texture2D>("Invader");
            text = Content.Load<SpriteFont>("Text");
            player = new Player(playerSprite);
            enemyArray = new Enemy[n, m];
            CreateEnemyArray();
        }
        protected override void UnloadContent()
        {
            
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (mode == "Menu")
                if (Menu.Controller() == 1)
                    Exit();
            if (mode == "Game")
            {
                player.Controll();
                if (CheckEnemyBoarder())
                {
                    direction *= (-1);
                    AllMoveY();
                }
                else
                {
                    AllMoveX();
                }
                foreach (var bullet in player.bulletList)
                {
                    bullet.Forward();
                    for (int i = 0; i < n; ++i)
                        for (int j = 0; j < m; ++j)
                            bullet.CheckEnemy(enemyArray[i, j]);
                }
                CheckEnemyArray();
                AllCheckGameOver();
            }
            if (mode == "Game Over")
            {
                KeyboardState keyState = Keyboard.GetState();
                if ((keyState.IsKeyDown(Keys.Enter)))
                    mode = "Menu";
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (mode == "Menu")
            {
                Menu.UI(spriteBatch);
            }
            if (mode == "Game")
            {
                player.UI(spriteBatch);
                for (int i = 0; i < n; ++i)
                    for (int j = 0; j < m; ++j)
                        enemyArray[i, j].UI(spriteBatch);
                spriteBatch.DrawString(Game1.text, "Уровень: " + level, new Vector2(20, Game1.height - 30), Color.Green);
            }
            if (mode == "Game Over")
            {
                spriteBatch.DrawString(Game1.text, "Вы проиграли", new Vector2(Game1.width / 2 - 75, Game1.height / 2 + 25), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
