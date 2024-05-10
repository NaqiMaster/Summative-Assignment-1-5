using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Summative_Assignment_1_5
{
    enum Screen
    {
        Intro,
        MainAnimation,
        End,
    }

    enum MarioPhase
    {
        InitialRun,
        Jumping1, 
        RunningRight1,
        HitCoin,
        bowserJump,
        bowserStop,
        marioThrow,
    }
   /*  enum BowserPhase
    {
        bowserJump,
        bowserStop
    }*/




    public class Game1 : Game
    {
        Screen screen;
        MarioPhase marioPhase;
       // BowserPhase bowserPhase;

        Rectangle window, introExit, introContinue, mario, goldCoinRect,bowserRect,redShellRect,
            endExit,endRestart;
        Texture2D introScreen, textureExit, contentScreen, marioLeftTexture, marioRightTexture,
            goldCoinTexture,marioCurrent, marioStraight,bowser,marioThrow,redShell,bowserKnockedTexture;
        MouseState mouseState, mouseState1;
        SpriteFont introTitleFont,introDescription,exitText;
        Vector2 marioSpeed, bowserSpeed,redShellSpeed;
        List <Texture2D> marioTextures = new List<Texture2D>();
        float seconds;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        bool drawBowser;

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
            marioPhase = MarioPhase.InitialRun;
            seconds = 0f;

            marioTextures.Add(marioLeftTexture);
            marioTextures.Add(marioRightTexture);

            
            goldCoinRect = new Rectangle(480, 250, 50, 50);
            introContinue = new Rectangle(50, 350, 210, 60);
            introExit = new Rectangle(50,420,210,60);
            endExit = new Rectangle(50,200,210,60);
            endRestart = new Rectangle(50,130, 210, 60);
            window = new Rectangle(0, 0, 900, 500);
            mario = new Rectangle(0, 380, 70, 70);
            marioSpeed = new Vector2 (2,0);
            redShellRect = new Rectangle(450, 270, 30, 30);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;

            drawBowser = false;

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
            marioLeftTexture = Content.Load<Texture2D>("marioLeft");
            marioRightTexture = Content.Load<Texture2D>("marioRight");
            goldCoinTexture = Content.Load<Texture2D>("goldCoin");
            marioStraight = Content.Load<Texture2D>("MarioStraight");
            marioCurrent = Content.Load<Texture2D>("marioRight");
            bowser = Content.Load<Texture2D>("Bowser");
            marioThrow = Content.Load < Texture2D>("marioThrowing");
            redShell = Content.Load<Texture2D>("redShell");
            bowserKnockedTexture = Content.Load<Texture2D>("bowserKnocked");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            mouseState = Mouse.GetState();

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (introExit.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.End;
                    }
                    if (introContinue.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.MainAnimation;
                    }
                }
            }
            else if (screen == Screen.MainAnimation)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

                mario.X += (int)marioSpeed.X;
                mario.Y += (int)marioSpeed.Y;

                bowserRect.X += (int)bowserSpeed.X;
                bowserRect.Y += (int)bowserSpeed.Y;


                if (marioPhase == MarioPhase.InitialRun)
                {
                    marioSpeed.X = 2;
                    marioSpeed.Y = 0;
                    marioPhase = MarioPhase.Jumping1;
                }
                else if (marioPhase == MarioPhase.Jumping1)
                {
                    if (seconds >= 3)
                    {
                        marioCurrent = marioStraight;
                        marioSpeed.X = 0;
                        marioSpeed.Y = -3;
                        marioPhase = MarioPhase.RunningRight1;
                    }
                }
                else if (marioPhase == MarioPhase.RunningRight1)
                {
                    if (seconds >= 3.8 && !mario.Intersects(goldCoinRect))
                    {
                        marioCurrent = marioRightTexture;
                        marioSpeed.X = 2;
                        marioSpeed.Y = 0;
                        marioPhase = MarioPhase.HitCoin;
                    }
                }
                else if (marioPhase == MarioPhase.HitCoin)
                {
                    if (mario.Intersects(goldCoinRect))
                    {
                        marioSpeed.X = 0;
                        marioSpeed.Y = 0;
                        goldCoinRect = new Rectangle(0, 0, 0, 0);
                        marioCurrent = marioStraight;
                        bowserRect = new Rectangle(730, 250, 150, 150);
                        marioPhase = MarioPhase.bowserJump;
                        drawBowser = true;

                    }
                }
                else if (marioPhase == MarioPhase.bowserJump)
                {

                    bowserSpeed.X = -2;
                    bowserSpeed.Y = -3;

                    if (bowserRect.Y <= 150)
                    {
                        bowserSpeed.X = -2;
                        bowserSpeed.Y = 0;
                    }
                    if (bowserRect.X == 550)
                    {
                        marioPhase = MarioPhase.bowserStop;
                    }
                }
                else if (marioPhase == MarioPhase.bowserStop)
                {
                    bowserSpeed.X = 0;
                    bowserSpeed.Y = 0;
                    marioPhase = MarioPhase.marioThrow;
                    marioCurrent = marioThrow;
                    redShellRect = new Rectangle(450, 270, 30, 30);
                    redShellSpeed.X = 10;
                    redShellSpeed.Y = 0;

                }
                else if (marioPhase == MarioPhase.marioThrow)
                {
                    redShellRect.Offset(redShellSpeed);
                    bowserRect.X += 5;


                    if (bowserRect.Contains(redShellRect.X, redShellRect.Y))
                    {
                        redShellRect = new Rectangle(0, 0, 0, 0);
                        marioCurrent = marioStraight;

                    }

                }
            }
            else if (screen == Screen.End)
            {
                mouseState1 = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (endExit.Contains(mouseState1.X, mouseState1.Y))
                    {
                        Exit();
                    }
                    if (endRestart.Contains(mouseState1.X, mouseState1.Y))
                    {
                        screen = Screen.MainAnimation;
                    }
                }
            }


                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                // TODO: Add your update logic here

                base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introScreen, window, Color.White);
                _spriteBatch.DrawString(introTitleFont, ("Super Mario"), new Vector2(5, 10), Color.Black);
                _spriteBatch.DrawString(introDescription, ("Fight for the Throne"), new Vector2(80, 70), Color.White);
                _spriteBatch.Draw(textureExit, introExit, Color.White);
                _spriteBatch.Draw(textureExit, introContinue, Color.White);
                _spriteBatch.DrawString(exitText,"Exit", new Vector2(110,440),Color.Black);
                _spriteBatch.DrawString(exitText, "Continue", new Vector2(60, 370), Color.Black);
            }
            else if (screen == Screen.MainAnimation)
            {
                _spriteBatch.Draw(contentScreen, window, Color.White);
                _spriteBatch.Draw(marioCurrent, mario, Color.White);
                _spriteBatch.Draw(goldCoinTexture, goldCoinRect,Color.White);

               if (drawBowser)
                {
                    _spriteBatch.Draw(bowser,bowserRect,Color.White);

                }
                if (marioPhase == MarioPhase.marioThrow)
                {
                    _spriteBatch.Draw(redShell, redShellRect, Color.White);

                }
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(introScreen, window, Color.White);
                _spriteBatch.Draw(textureExit, endExit, Color.White);
                _spriteBatch.Draw(textureExit, endRestart, Color.White);
                _spriteBatch.DrawString(exitText, "Exit", new Vector2(110, 220), Color.Black);
                _spriteBatch.DrawString(exitText, "Restart", new Vector2(70, 150), Color.Black);
            }

            _spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}