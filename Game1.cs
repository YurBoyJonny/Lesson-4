using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson_4
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bombTexture;
        Rectangle bombRect;

        SpriteFont titleFont;

        float seconds;
        float startTime;

        MouseState mouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 500; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions
            // TODO: Add your initialization logic here

            bombRect = new Rectangle(50, 50, 700, 400);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bombTexture = Content.Load<Texture2D>("bomb");
            bombRect = new Rectangle(50,50,700,400);
            
            titleFont = Content.Load<SpriteFont>("Time");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (seconds > 10) // Takes a timestamp every 10 seconds.
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;

            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;

            seconds.ToString("00.0");

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(titleFont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
