#region all of the code on one line
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace DuckHunt
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keys, oldkeys;

        bool drawingDuck = true;
        bool drawingDuck2 = true;

        bool[] ducksKilled = new bool[10];

        int ducksNeeded;

        Texture2D backgroundBOI;
        Texture2D duckBoi;
        Texture2D mousePointer;
        
        Rectangle mouseRect;

        Rectangle duckBoiRect;
        Rectangle secondDuckRect;
        int ducksOnScreen = 2;

        MouseState clicker, oldClicker;
        Point cursorPoint;

        bool duckUP = true;
        bool duckRight = true;
        bool duckUP2 = true;
        bool duckRight2 = true;

        Random rand = new Random();

        int speed = 5;

        int bulletsLeft = 3;

        int drawingDogTime = 0;

        int duckEscapeTimer;
        int flyAwayText;

        int score = 0;

        bool perfectRun = true;

        int transitionTimer;

        bool duckFalling = false;
        bool duckFalling2 = false;

        enum GameState
        {
            start, PlayingOne, PlayingTwo, lose
        }

        GameState state = GameState.start;

        Color oneColor = Color.Red;
        Color twoColor = Color.White;

        int ducksLeft = 10;

        int round = 1;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1000;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundBOI = Content.Load<Texture2D>("duckHuntBackground");
            duckBoi = Content.Load<Texture2D>("duckOne");

            mousePointer = Content.Load<Texture2D>("MousePointer");
            mouseRect = new Rectangle(cursorPoint.X - 50, cursorPoint.Y - 50, 100, 100);

            duckBoiRect = new Rectangle(rand.Next(200, 700), 450, 100, 100);
            secondDuckRect = new Rectangle(rand.Next(200, 700), 450, 100, 100);

            for (int i = 0; i < 10; i++)
            {
                ducksKilled[i] = false;
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            clicker = Mouse.GetState();
            keys = Keyboard.GetState();

            cursorPoint = new Point(clicker.X, clicker.Y);

            mouseRect.X = cursorPoint.X - 50;
            mouseRect.Y = cursorPoint.Y - 50;

            switch (state)
            #region gameState
            {
                case GameState.start:
                    UpdateStart();
                    break;
                case GameState.PlayingOne:
                    UpdatePlayingOne();
                    break;
                case GameState.PlayingTwo:
                    UpdatePlayingTwo();
                    break;
                case GameState.lose:
                    UpdateLose();
                    break;
            }
                #endregion

            oldClicker = clicker;
            oldkeys = keys;
            base.Update(gameTime);
        }

        private void UpdateStart()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(Content.Load<Texture2D>("duckHuntStartBOI"), new Rectangle(0, 0, 1000, 700), Color.White);

            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "ONE DUCK", new Vector2(800, 50), oneColor);
            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "TWO DUCKS", new Vector2(800, 100), twoColor);

            spriteBatch.End();

            if (keys.IsKeyDown(Keys.Up) && oldkeys.IsKeyUp(Keys.Up))
            {
                oneColor = Color.Red;
                twoColor = Color.White;
            }
            if (keys.IsKeyDown(Keys.Down) && oldkeys.IsKeyUp(Keys.Down))
            {
                oneColor = Color.White;
                twoColor = Color.Red;
            }
            if (keys.IsKeyDown(Keys.Enter) && oldkeys.IsKeyUp(Keys.Enter))
            {
                if (oneColor == Color.Red)
                {
                    state = GameState.PlayingOne;
                    ducksNeeded = 1;
                    duckEscapeTimer = 390;
                }
                else
                {
                    state = GameState.PlayingTwo;
                    ducksNeeded = 1;
                    duckEscapeTimer = 390;
                    ducksOnScreen = 2;
                }
            }
        }

        private void UpdatePlayingOne()
        {
            #region up/down
            if (duckUP && !duckFalling && drawingDogTime == 0)
            {
                duckBoiRect.Y = duckBoiRect.Y - speed;
            }
            if (!duckUP && !duckFalling && drawingDogTime == 0)
            {
                duckBoiRect.Y = duckBoiRect.Y + speed;
            }

            if (duckBoiRect.Y <= 0)
            {
                duckUP = false;
            }
            if (duckBoiRect.Y >= 450)
            {
                duckUP = true;
            }
            #endregion
            #region left/right
            if (duckRight && !duckFalling && drawingDogTime == 0)
            {
                duckBoiRect.X = duckBoiRect.X + speed;
            }
            if (!duckRight && !duckFalling && drawingDogTime == 0)
            {
                duckBoiRect.X = duckBoiRect.X - speed;
            }
            if (duckBoiRect.X <= 0 && !duckFalling && drawingDogTime == 0)
            {
                duckRight = true;
            }
            if (duckBoiRect.X >= 900 && !duckFalling && drawingDogTime == 0)
            {
                duckRight = false;
            }
            #endregion
            #region spriteBatch
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundBOI, new Rectangle(0, 0, 1000, 700), Color.White);
            spriteBatch.Draw(mousePointer, mouseRect, Color.White);
            if (drawingDuck)
            {
                if (duckFalling)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("duckFalling"), duckBoiRect, Color.White);
                }
                else if (duckRight)
                {
                    spriteBatch.Draw(duckBoi, duckBoiRect, Color.White);
                }
                else if (!duckRight)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("duckOneLeft"), duckBoiRect, Color.White);
                }
            }
            if (drawingDogTime > 0)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("duckHuntDog"), new Vector2(duckBoiRect.X, 425), Color.White);
                drawingDogTime--;
                drawingDuck = false;
                duckEscapeTimer = 390;
                if (drawingDogTime == 0)
                {
                    duckBoiRect.Y = 450;
                    bulletsLeft = 3;
                    drawingDuck = true;
                    if (rand.Next(1, 3) == 1)
                    {
                        duckRight = true;
                        duckBoiRect.X = rand.Next(200, 400);
                    }
                    else
                    {
                        duckRight = false;
                        duckBoiRect.X = rand.Next(400, 700);
                    }
                    duckUP = true;
                }
            }
            spriteBatch.Draw(Content.Load<Texture2D>("duckHuntGrass"), new Rectangle(0, 507, 1000, 75), Color.White);
            spriteBatch.End();
            #endregion
            #region shooting

            if (oldClicker.LeftButton == ButtonState.Released && clicker.LeftButton == ButtonState.Pressed)
            {
                bulletsLeft--;
                if (duckBoiRect.Contains(cursorPoint))
                {
                    duckFalling = true;
                }
                else if (bulletsLeft == 2)
                {
                    perfectRun = false;
                }
                else if (bulletsLeft == 0)
                {
                    ducksKilled[ducksLeft - 1] = false;
                    ducksLeft--;
                    duckBoiRect.Y = 450;
                    bulletsLeft = 3;
                    drawingDuck = true;
                    perfectRun = false;
                    if (rand.Next(1, 3) == 1)
                    {
                        duckRight = true;
                        duckBoiRect.X = rand.Next(200, 500);
                    }
                    else
                    {
                        duckRight = false;
                        duckBoiRect.X = rand.Next(500, 800);
                    }
                    duckUP = true;
                    if (ducksLeft == 0)
                    {
                        Content.Load<SoundEffect>("stageClear").Play();
                        transitionTimer = 240;
                        if (perfectRun)
                        {
                            score = score + 5000;
                        }
                    }
                }
            }

            #endregion
            #region losing
            if (ducksNeeded > ducksLeft)
            {
                state = GameState.lose;
                Content.Load<SoundEffect>("gameOverSound").Play();
            }
            #endregion
            #region fly away
            if (duckEscapeTimer > 0)
            {
                duckEscapeTimer--;
                if (duckEscapeTimer == 0)
                {
                    flyAwayText = 120;
                    duckBoiRect.X = 50;
                    duckBoiRect.Y = 500;
                    perfectRun = false;
                }
            }
            spriteBatch.Begin();

            if (flyAwayText > 0)
            {
                flyAwayText--;
                duckBoiRect.Y = duckBoiRect.Y - 1;
                if (duckRight)
                {
                    duckBoiRect.X = duckBoiRect.X - (speed + 1);
                }
                if (!duckRight)
                {
                    duckBoiRect.X = duckBoiRect.X - (speed + 1);
                }
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "FLY AWAY", new Vector2(300, 200), Color.Black);
                if (flyAwayText == 0)
                {
                    ducksKilled[ducksLeft - 1] = false;
                    ducksLeft--;
                    duckBoiRect.Y = 450;
                    bulletsLeft = 3;
                    drawingDuck = true;
                    if (rand.Next(1, 3) == 1)
                    {
                        duckRight = true;
                        duckBoiRect.X = rand.Next(200, 500);
                    }
                    else
                    {
                        duckRight = false;
                        duckBoiRect.X = rand.Next(500, 800);
                    }
                    duckUP = true;
                    duckEscapeTimer = 390;
                }
            }

            spriteBatch.End();
            #endregion
            #region transition timer
            if (transitionTimer > 0)
            {
                transitionTimer--;
                drawingDuck = false;
                spriteBatch.Begin();
                if (perfectRun)
                {
                    spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "PERFECT RUN!", new Vector2(400, 300), Color.Black);
                }
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "LEVEL " + round.ToString(), new Vector2(400, 250), Color.Black);
                spriteBatch.End();
                if (transitionTimer == 0)
                {
                    #region ducks needed
                    if (round < 10)
                    {
                        ducksNeeded = 1;
                    }
                    if (round == 11)
                    {
                        ducksNeeded = 2;
                    }
                    if (round == 12)
                    {
                        ducksNeeded = 3;
                    }
                    if (round == 13)
                    {
                        ducksNeeded = 4;
                    }
                    if (round == 14)
                    {
                        ducksNeeded = 5;
                    }
                    if (round == 15)
                    {
                        ducksNeeded = 6;
                    }
                    if (round == 16)
                    {
                        ducksNeeded = 7;
                    }
                    if (round == 17)
                    {
                        ducksNeeded = 8;
                    }
                    if (round == 18)
                    {
                        ducksNeeded = 9;
                    }
                    if (round >= 19)
                    {
                        ducksNeeded = 10;
                    }
                    #endregion
                    #region speed
                    if (round == 3)
                    {
                        speed = 6;
                    }
                    if (round == 5)
                    {
                        speed = 7;
                    }
                    if (round == 7)
                    {
                        speed = 8;
                    }
                    if (round == 9)
                    {
                        speed = 9;
                    }
                    if (round == 11)
                    {
                        speed = 10;
                    }
                    if (round == 13)
                    {
                        speed = 11;
                    }
                    if (round == 15)
                    {
                        speed = 12;
                    }
                    #endregion
                    round++;
                    ducksLeft = 10;
                    duckBoiRect.Y = 450;
                    bulletsLeft = 3;
                    drawingDuck = true;
                    if (rand.Next(1, 3) == 1)
                    {
                        duckRight = true;
                        duckBoiRect.X = rand.Next(200, 400);
                    }
                    else
                    {
                        duckRight = false;
                        duckBoiRect.X = rand.Next(400, 700);
                    }
                    duckUP = true;
                    perfectRun = true;
                    for (int i = 0; i < 10; i++)
                    {
                        ducksKilled[i] = false;
                    }
                }
            }
            #endregion
            #region falling
            if (duckFalling)
            {
                duckBoiRect.Y += 15;
                duckBoiRect.Width = 70;
                if (duckBoiRect.Y >= 490)
                {
                    Content.Load<SoundEffect>("gotADuck").Play();
                    duckBoiRect.Width = 100;
                    duckFalling = false;
                    drawingDogTime = 60;
                    ducksKilled[ducksLeft - 1] = true;
                    ducksLeft--;
                    ducksNeeded--;
                    duckEscapeTimer = 390;
                    if (bulletsLeft == 2)
                    {
                        score = score + 1500;
                    }
                    else if (bulletsLeft == 1)
                    {
                        score = score + 1000;
                    }
                    else
                    {
                        score = score + 500;
                    }

                    if (ducksLeft == 0)
                    {
                        Content.Load<SoundEffect>("stageClear").Play();
                        transitionTimer = 240;
                        if (perfectRun)
                        {
                            score = score + 5000;
                        }
                    }
                }
            }
            #endregion
        }
        
        private void UpdatePlayingTwo()
        {
            #region up/down
            if (duckUP && !duckFalling && drawingDogTime == 0)
            {
                duckBoiRect.Y = duckBoiRect.Y - speed;
            }
            if (!duckUP && !duckFalling && drawingDogTime == 0)
            {
                duckBoiRect.Y = duckBoiRect.Y + speed;
            }

            if (duckBoiRect.Y <= 0)
            {
                duckUP = false;
            }
            if (duckBoiRect.Y >= 450)
            {
                duckUP = true;
            }

            if (duckUP2 && !duckFalling2 && drawingDogTime == 0)
            {
                secondDuckRect.Y = secondDuckRect.Y - speed;
            }
            if (!duckUP2 && !duckFalling2 && drawingDogTime == 0)
            {
                secondDuckRect.Y = secondDuckRect.Y + speed;
            }

            if (secondDuckRect.Y <= 0)
            {
                duckUP2 = false;
            }
            if (secondDuckRect.Y >= 450)
            {
                duckUP2 = true;
            }
            #endregion
            #region left/right
            if (duckRight && !duckFalling && drawingDogTime == 0)
            {
                duckBoiRect.X = duckBoiRect.X + speed;
            }
            if (!duckRight && !duckFalling && drawingDogTime == 0)
            {
                duckBoiRect.X = duckBoiRect.X - speed;
            }
            if (duckBoiRect.X <= 0 && !duckFalling && drawingDogTime == 0)
            {
                duckRight = true;
            }
            if (duckBoiRect.X >= 900 && !duckFalling && drawingDogTime == 0)
            {
                duckRight = false;
            }

            if (duckRight2 && !duckFalling2 && drawingDogTime == 0)
            {
                secondDuckRect.X = secondDuckRect.X + speed;
            }
            if (!duckRight2 && !duckFalling2 && drawingDogTime == 0)
            {
                secondDuckRect.X = secondDuckRect.X - speed;
            }
            if (secondDuckRect.X <= 0 && !duckFalling2 && drawingDogTime == 0)
            {
                duckRight2 = true;
            }
            if (secondDuckRect.X >= 900 && !duckFalling2 && drawingDogTime == 0)
            {
                duckRight2 = false;
            }
            #endregion
            #region spriteBatch
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundBOI, new Rectangle(0, 0, 1000, 700), Color.White);
            spriteBatch.Draw(mousePointer, mouseRect, Color.White);
            if (drawingDuck)
            {
                if (duckFalling)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("duckFalling"), duckBoiRect, Color.White);
                }
                else if (duckRight)
                {
                    spriteBatch.Draw(duckBoi, duckBoiRect, Color.White);
                }
                else if (!duckRight)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("duckOneLeft"), duckBoiRect, Color.White);
                }
            }
            if (drawingDuck2)
            {
                if (duckFalling2)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("duckFalling"), secondDuckRect, Color.White);
                }
                else if (duckRight2)
                {
                    spriteBatch.Draw(duckBoi, secondDuckRect, Color.White);
                }
                else if (!duckRight2)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("duckOneLeft"), secondDuckRect, Color.White);
                }
            }
            if (drawingDogTime > 0)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("duckHuntDock2"), new Rectangle(400, 425, 125, 100), Color.White);
                drawingDogTime--;
                ducksOnScreen = 2;
                drawingDuck = false;
                duckEscapeTimer = 390;
                if (drawingDogTime == 0)
                {
                    duckBoiRect.Y = 450;
                    bulletsLeft = 3;
                    drawingDuck = true;
                    if (rand.Next(1, 3) == 1)
                    {
                        duckRight = true;
                        duckBoiRect.X = rand.Next(200, 400);
                    }
                    else
                    {
                        duckRight = false;
                        duckBoiRect.X = rand.Next(400, 700);
                    }
                    duckUP = true;

                    secondDuckRect.Y = 450;
                    bulletsLeft = 3;
                    drawingDuck2 = true;
                    if (rand.Next(1, 3) == 1)
                    {
                        duckRight2 = true;
                        secondDuckRect.X = rand.Next(200, 400);
                    }
                    else
                    {
                        duckRight2 = false;
                        secondDuckRect.X = rand.Next(400, 700);
                    }
                    duckUP2 = true;
                }
            }
            spriteBatch.Draw(Content.Load<Texture2D>("duckHuntGrass"), new Rectangle(0, 507, 1000, 75), Color.White);
            spriteBatch.End();
            #endregion
            #region shooting

            if (oldClicker.LeftButton == ButtonState.Released && clicker.LeftButton == ButtonState.Pressed)
            {
                bulletsLeft--;
                if (duckBoiRect.Contains(cursorPoint))
                {
                    duckFalling = true;
                }
                if (secondDuckRect.Contains(cursorPoint))
                {
                    duckFalling2 = true;
                }
                else if (bulletsLeft == 1)
                {
                    perfectRun = false;
                }
                else if (bulletsLeft == 0)
                {
                    if (ducksOnScreen == 2)
                    {
                        ducksKilled[ducksLeft - 1] = false;
                        ducksKilled[ducksLeft - 2] = false;
                        ducksLeft =- 2;
                    }
                    else if (ducksOnScreen == 1)
                    {
                        ducksKilled[ducksLeft - 1] = false;
                        ducksLeft--;
                    }
                    duckBoiRect.Y = 450;
                    secondDuckRect.Y = 450;
                    bulletsLeft = 3;
                    drawingDuck = true;
                    drawingDuck2 = true;
                    perfectRun = false;
                    
                    if (rand.Next(1, 3) == 1)
                    {
                        duckRight = true;
                        duckBoiRect.X = rand.Next(200, 500);
                    }
                    else
                    {
                        duckRight = false;
                        duckBoiRect.X = rand.Next(500, 800);
                    }
                    duckUP = true;

                    if (rand.Next(1, 3) == 1)
                    {
                        duckRight2 = true;
                        secondDuckRect.X = rand.Next(200, 500);
                    }
                    else
                    {
                        duckRight2 = false;
                        secondDuckRect.X = rand.Next(500, 800);
                    }
                    duckUP2 = true;

                    if (ducksLeft == 0)
                    {
                        Content.Load<SoundEffect>("stageClear").Play();
                        transitionTimer = 240;
                        if (perfectRun)
                        {
                            score = score + 5000;
                        }
                    }
                }
            }

            #endregion
            #region losing
            if (ducksNeeded > ducksLeft)
            {
                state = GameState.lose;
                Content.Load<SoundEffect>("gameOverSound").Play();
            }
            #endregion
            #region transition timer
            if (transitionTimer > 0)
            {
                transitionTimer--;
                drawingDuck2 = false;
                drawingDuck = false;
                spriteBatch.Begin();
                if (perfectRun)
                {
                    spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "PERFECT RUN!", new Vector2(400, 300), Color.Black);
                }
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "LEVEL " + round.ToString(), new Vector2(400, 250), Color.Black);
                spriteBatch.End();
                if (transitionTimer == 0)
                {
                    #region ducks needed
                    if (round < 10)
                    {
                        ducksNeeded = 1;
                    }
                    if (round == 11)
                    {
                        ducksNeeded = 2;
                    }
                    if (round == 12)
                    {
                        ducksNeeded = 3;
                    }
                    if (round == 13)
                    {
                        ducksNeeded = 4;
                    }
                    if (round == 14)
                    {
                        ducksNeeded = 5;
                    }
                    if (round == 15)
                    {
                        ducksNeeded = 6;
                    }
                    if (round == 16)
                    {
                        ducksNeeded = 7;
                    }
                    if (round == 17)
                    {
                        ducksNeeded = 8;
                    }
                    if (round == 18)
                    {
                        ducksNeeded = 9;
                    }
                    if (round >= 19)
                    {
                        ducksNeeded = 10;
                    }
                    #endregion
                    #region speed
                    if (round == 3)
                    {
                        speed = 6;
                    }
                    if (round == 5)
                    {
                        speed = 7;
                    }
                    if (round == 7)
                    {
                        speed = 8;
                    }
                    if (round == 9)
                    {
                        speed = 9;
                    }
                    if (round == 11)
                    {
                        speed = 10;
                    }
                    if (round == 13)
                    {
                        speed = 11;
                    }
                    if (round == 15)
                    {
                        speed = 12;
                    }
                    #endregion
                    round++;
                    ducksLeft = 10;
                    duckBoiRect.Y = 450;
                    secondDuckRect.Y = 450;
                    bulletsLeft = 3;
                    drawingDuck = true;
                    drawingDuck2 = true;
                    
                    if (rand.Next(1, 3) == 1)
                    {
                        duckRight = true;
                        duckBoiRect.X = rand.Next(200, 400);
                    }
                    else
                    {
                        duckRight = false;
                        duckBoiRect.X = rand.Next(400, 700);
                    }

                    if (rand.Next(1, 3) == 1)
                    {
                        duckRight2 = true;
                        secondDuckRect.X = rand.Next(200, 400);
                    }
                    else
                    {
                        duckRight2 = false;
                        secondDuckRect.X = rand.Next(400, 700);
                    }

                    duckUP = true;
                    duckUP2 = true;
                    perfectRun = true;
                    for (int i = 0; i < 10; i++)
                    {
                        ducksKilled[i] = false;
                    }
                }
            }
            #endregion
            #region falling
            if (duckFalling)
            {
                duckBoiRect.Y += 15;
                duckBoiRect.Width = 70;
                if (duckBoiRect.Y >= 490)
                {
                    Content.Load<SoundEffect>("gotADuck").Play();
                    duckBoiRect.Width = 100;
                    duckFalling = false;
                    drawingDuck = false;
                    ducksKilled[ducksLeft - 1] = true;
                    ducksLeft--;
                    ducksNeeded--;
                    ducksOnScreen--;
                    duckEscapeTimer = 390;
                    if (bulletsLeft == 2)
                    {
                        score = score + 1500;
                    }
                    else if (bulletsLeft == 1)
                    {
                        score = score + 1000;
                    }
                    else
                    {
                        score = score + 500;
                    }

                    if (ducksLeft == 0)
                    {
                        Content.Load<SoundEffect>("stageClear").Play();
                        transitionTimer = 240;
                        if (perfectRun)
                        {
                            score = score + 5000;
                        }
                    }
                }
                if (ducksOnScreen == 0)
                {
                    drawingDogTime = 60;
                }
            }

            if (duckFalling2)
            {
                secondDuckRect.Y += 15;
                secondDuckRect.Width = 70;
                if (secondDuckRect.Y >= 490)
                {
                    Content.Load<SoundEffect>("gotADuck").Play();
                    secondDuckRect.Width = 100;
                    duckFalling2 = false;
                    drawingDuck2 = false;
                    ducksKilled[ducksLeft - 1] = true;
                    ducksLeft--;
                    ducksOnScreen--;
                    ducksNeeded--;
                    duckEscapeTimer = 390;
                    if (bulletsLeft == 2)
                    {
                        score = score + 1500;
                    }
                    else if (bulletsLeft == 1)
                    {
                        score = score + 1000;
                    }
                    else
                    {
                        score = score + 500;
                    }

                    if (ducksLeft == 0)
                    {
                        Content.Load<SoundEffect>("stageClear").Play();
                        transitionTimer = 240;
                        if (perfectRun)
                        {
                            score = score + 5000;
                        }
                    }
                }
                if (ducksOnScreen == 0)
                {
                    drawingDogTime = 60;
                }
            }
            #endregion
        }

        private void UpdateLose()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(Content.Load<Texture2D>("duckHuntLose"), new Rectangle(0, 0, 1000, 700), Color.White);

            spriteBatch.End();
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            #region bullets
            if (state != GameState.lose && state != GameState.start)
            {
                if (bulletsLeft == 3)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("threeBullets"), new Rectangle(150, 610, 100, 65), Color.White);
                }
                else if (bulletsLeft == 2)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("twoBullets"), new Rectangle(150, 610, 100, 65), Color.White);
                }
                else
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("oneBullet"), new Rectangle(150, 610, 100, 65), Color.White);
                }
            }
            #endregion
            #region ducks killed
            if (state != GameState.start && state != GameState.lose)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!ducksKilled[i])
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("duckIcon"), new Rectangle(650 - (40 * i), 610, 40, 40), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("duckIcon"), new Rectangle(650 - (40 * i), 610, 40, 40), Color.Red);
                    }
                }
            }
            #endregion
            #region score
            if (state != GameState.start && state != GameState.lose)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("scoreDisplayer"), new Rectangle(700, 600, 200, 80), Color.White);
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), score.ToString(), new Vector2(720, 605), Color.White);
            }
            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
#endregion