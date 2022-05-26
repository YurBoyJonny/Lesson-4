using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Lesson_4
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bombTexture;
        Rectangle bombRect;

        Texture2D explosionTexture;
        Rectangle explosionRect;

        SpriteFont timeFont;

        SoundEffect explode;
        SoundEffectInstance explodeInstance;

        float seconds;
        float startTime;

        MouseState mouseState;

        bool exploded;

        Texture2D defuseTexture;
        Rectangle defuse;

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

            exploded = false;
            bombRect = new Rectangle(50, 50, 700, 400);
            explosionRect = new Rectangle(0, 0, 800, 500);

            defuse = new Rectangle(175, 60, 100, 50); ///////////////////////

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            bombRect = new Rectangle(50,50,700,400);
            timeFont = Content.Load<SpriteFont>("Time");

            explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();

            explosionTexture = Content.Load<Texture2D>("bombExploded");
            explosionRect = new Rectangle(0, 0, 800, 500);

            defuseTexture = Content.Load<Texture2D>("rectangle");
            defuse = new Rectangle(175, 60, 60, 50);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (seconds > 15) // Takes a timestamp every 10 seconds.
            {
                explodeInstance.Play();
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                exploded = true;
                explosionRect = new Rectangle(0, 0, 800, 500);
            }
            if (exploded && explodeInstance.State == SoundState.Stopped)
                Exit();
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
                if (defuse.Contains(mouseState.X, mouseState.Y))
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.Draw(defuseTexture, defuse, Color.Green);
            _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("0:00"), new Vector2(270, 200), Color.Black);
            if (exploded)
            {
                _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
