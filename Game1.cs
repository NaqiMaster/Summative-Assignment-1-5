using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        Victory
    }





    public class Game1 : Game
    {
        Screen screen;
        MarioPhase marioPhase;
        SoundEffect marioIntroSong;
        SoundEffectInstance marioIntroSongInstance;
        Rectangle window, introExit, introContinue, mario, goldCoinRect,bowserRect,redShellRect,
            endExit,textBubbleRect;
        Texture2D introScreen, textureExit, contentScreen,marioTextureRight, marioTextureExcited,textBubble,
            goldCoinTexture,marioTextureCurrent, marioTextureStraight,bowserTexture,marioTextureThrow,redShellTexture,marioTextureUp
            ,endScreen;
        MouseState mouseState, mouseState1;
        SpriteFont introTitleFont,introDescription,exitText,victorySpeech;
        Vector2 marioSpeed, bowserSpeed,redShellSpeed;
        float seconds, seconds1;

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
            seconds1 = 0f;


            textBubbleRect = new Rectangle(270,180,200,50);
            goldCoinRect = new Rectangle(480, 250, 50, 50);
            introContinue = new Rectangle(50, 350, 210, 60);
            introExit = new Rectangle(50,420,210,60);
            endExit = new Rectangle(670,50,210,60);
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
            //intro screen
            introScreen = Content.Load<Texture2D>("SuperMario");
            introTitleFont = Content.Load<SpriteFont>("introTitle");
            introDescription = Content.Load<SpriteFont>("introDescription");
            textureExit = Content.Load<Texture2D>("gold");
            exitText = Content.Load<SpriteFont>("exitText");
            marioIntroSong = Content.Load<SoundEffect>("Mario Intro Song");
            marioIntroSongInstance = marioIntroSong.CreateInstance();

            //Animation
            victorySpeech = Content.Load<SpriteFont>("victorySpeech");

            contentScreen = Content.Load<Texture2D>("SuperMarioBackground");
            marioTextureRight = Content.Load<Texture2D>("marioTextureRun");
            goldCoinTexture = Content.Load<Texture2D>("goldCoin");
            marioTextureStraight = Content.Load<Texture2D>("mariotextureStand");
            marioTextureUp = Content.Load<Texture2D>("marioTextureUp");
            marioTextureCurrent = Content.Load<Texture2D>("marioTextureRun");
            bowserTexture = Content.Load<Texture2D>("Bowser");
            marioTextureThrow = Content.Load < Texture2D>("marioTextureThrow");
            redShellTexture = Content.Load<Texture2D>("redShell");
            marioTextureExcited = Content.Load<Texture2D>("marioTextureExcited");
            textBubble = Content.Load<Texture2D>("textBubble");

            endScreen = Content.Load<Texture2D>("marioEndScreen");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            mouseState = Mouse.GetState();

            if (screen == Screen.Intro)
            {
                marioIntroSongInstance.Play();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (introExit.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.End;
                        marioIntroSongInstance.Stop();
                    }
                    if (introContinue.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.MainAnimation;
                        marioIntroSongInstance.Stop();
                    }
                }
                if (marioIntroSongInstance.State == SoundState.Stopped)
                    {
                        screen = Screen.End;
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
                        marioTextureCurrent = marioTextureUp;
                        marioSpeed.X = 0;
                        marioSpeed.Y = -3;
                        marioPhase = MarioPhase.RunningRight1;
                    }
                }
                else if (marioPhase == MarioPhase.RunningRight1)
                {
                    if (seconds >= 3.8 && !mario.Intersects(goldCoinRect))
                    {
                        marioTextureCurrent = marioTextureRight;
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
                        marioTextureCurrent = marioTextureStraight;
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
                    marioTextureCurrent = marioTextureThrow;
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
                    }

                    if (bowserRect.X >= 950)
                    {
                        marioPhase = MarioPhase.Victory;
                    }

                }
                else if (marioPhase == MarioPhase.Victory)
                {
                    marioTextureCurrent = marioTextureExcited;
                    seconds1 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (seconds1 >=3)
                    {
                        screen = Screen.End;
                        marioTextureCurrent = marioTextureExcited;
                        mario.X = 50;
                        mario.Y = 380;
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
                _spriteBatch.Draw(marioTextureCurrent, mario, Color.White);
                _spriteBatch.Draw(goldCoinTexture, goldCoinRect,Color.White);

               if (drawBowser)
               {
                    _spriteBatch.Draw(bowserTexture,bowserRect,Color.White);

               }
               if (marioPhase == MarioPhase.marioThrow)
               {
                    _spriteBatch.Draw(redShellTexture, redShellRect, Color.White);

               }

               if (marioPhase == MarioPhase.Victory)
                {
                    _spriteBatch.Draw(textBubble, textBubbleRect, Color.White);
                    _spriteBatch.DrawString(victorySpeech, ("WE GOT HIM!"), new Vector2(280, 190), Color.Black);
                }
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(endScreen, window, Color.White);
                _spriteBatch.Draw(textureExit, endExit, Color.White);
                _spriteBatch.DrawString(exitText, "Exit", new Vector2(730, 70), Color.Black);
                _spriteBatch.Draw(marioTextureExcited, mario, Color.White);
            }

            _spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}