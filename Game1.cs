using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Summative_Assignment_1_5
{
    enum Screen
    {
        Intro,
        Content,
        End
    }
    public class Game1 : Game
    {
        Screen screen;
        Rectangle window, Exit, Continue;
        Texture2D introScreen, textureExit;
        MouseState mouseState;
        SpriteFont introTitleFont,introDescription,exitText;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            screen = Screen.Intro;

            Continue = new Rectangle(50, 350, 210, 60);
            Exit = new Rectangle(50,420,210,60);
            window = new Rectangle(0, 0, 900, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            introScreen = Content.Load<Texture2D>("SuperMario");
            introTitleFont = Content.Load<SpriteFont>("introTitle");
            introDescription = Content.Load<SpriteFont>("introDescription");
            textureExit = Content.Load<Texture2D>("gold");
            exitText = Content.Load<SpriteFont>("exitText");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (Exit.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.End;
                    }
                    if (Continue.Contains(mouseState.X,mouseState.Y))
                    {
                        screen = Screen.Content;
                    }
                }
            }
            else if (screen == Screen.Content)
            {
                //Logic for animations
            }
            else if (screen == Screen.End)
            {
                //End Page, 2 buttons (One to restart, one to exit)
            }

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                // TODO: Add your update logic here

                base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (screen == Screen.Intro)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(introScreen, window, Color.White);
                _spriteBatch.DrawString(introTitleFont, ("Super Mario"), new Vector2(5, 10), Color.Black);
                _spriteBatch.DrawString(introDescription, ("Fight for the Throne"), new Vector2(80, 70), Color.White);
                _spriteBatch.Draw(textureExit, Exit, Color.White);
                _spriteBatch.Draw(textureExit, Continue, Color.White);
                _spriteBatch.DrawString(exitText,"Exit", new Vector2(110,440),Color.Black);
                _spriteBatch.DrawString(exitText, "Continue", new Vector2(60, 370), Color.Black);


                _spriteBatch.End();
            }
            else if (screen == Screen.Content)
            {

            }
            else if (screen == Screen.End)
            {

            }


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}