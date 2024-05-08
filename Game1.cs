using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        Rectangle window, Exit, Continue, mario, goldCoinRect;
        Texture2D introScreen, textureExit, contentScreen, marioLeft, marioRight, goldCoin,marioCurrent, marioStraight;
        MouseState mouseState;
        SpriteFont introTitleFont,introDescription,exitText;
        Vector2 marioSpeed;
        List <Texture2D> marioTextures = new List<Texture2D>();
        float seconds;

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
            seconds = 0f;

            marioTextures.Add(marioLeft);
            marioTextures.Add(marioRight);

            goldCoinRect = new Rectangle(480, 250, 50, 50);
            Continue = new Rectangle(50, 350, 210, 60);
            Exit = new Rectangle(50,420,210,60);
            window = new Rectangle(0, 0, 900, 500);
            mario = new Rectangle(0, 380, 70, 70);
            marioSpeed = new Vector2 (2,0);
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
            contentScreen = Content.Load<Texture2D>("SuperMarioBackground");
            marioLeft = Content.Load<Texture2D>("MarioLeft");
            marioRight = Content.Load<Texture2D>("MarioRight");
            goldCoin = Content.Load<Texture2D>("GoldCoin");
            marioStraight = Content.Load<Texture2D>("MarioStraight");
            marioCurrent = Content.Load<Texture2D>("MarioRight");


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
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

                mario.X += (int)marioSpeed.X;
                mario.Y += (int)marioSpeed.Y;

                if (seconds >= 3)
                {
                    marioCurrent = marioStraight;
                    marioSpeed.X = 0;
                    marioSpeed.Y = -3;
                }

                if (seconds >= 3.8)
                {
                    marioCurrent = marioRight;
                    marioSpeed.X = 2;                  
                    marioSpeed.Y = 0;
                  
                }
                if (mario.Intersects(goldCoinRect))
                {
                    marioSpeed.X = 0;
                    marioSpeed.Y = 0;
                    goldCoinRect.Location = new Point(50, 50);

                }



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
                _spriteBatch.Begin();
                _spriteBatch.Draw(contentScreen, window, Color.White);
                _spriteBatch.Draw(marioCurrent, mario, Color.White);
                _spriteBatch.Draw(goldCoin, goldCoinRect,Color.White);

                _spriteBatch.End();
            }
            else if (screen == Screen.End)
            {

            }


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}