using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;


namespace TDJ_Gliese
{

    public class Game1 : Game
    {

      
        #region Initialize
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Game States
        enum State
        {
            Main_Menu,
            UpgradesMenu,
            Playing,
            Gameover,
            Beggining,
        }
        State currentstate;

        int BegginingText;
        //
        //Audio
        int Ketsap;
        int Blockbuster;
        public Song Ketsa;
        public Song Space_BackGround_Music;
        SoundEffect ButtonSwitch;
        SoundEffect ButtonChoose;
        SoundEffect Engine1;
        SoundEffect Engine2;
        SoundEffect Engine3;
        SoundEffect Wrong;


        #region Images
        Texture2D MainMenuBackground;
        Texture2D UpgradesBackground;
        Texture2D UpgradesBackgroundSpinner;
        Texture2D Gliese;
        Texture2D PlayButton;
        Texture2D UpgradesButton;
        Texture2D Stats;
        Texture2D ArrowLeft;
        Texture2D ArrowRight;
        Texture2D StatBar;
        Texture2D Ship1;
        Texture2D Ship2;
        Texture2D Ship3;
        Texture2D MusicPlate;
        Texture2D GameOver;

        Texture2D Saboteur;
        Texture2D Chaser;
        Texture2D Goliath;
        Texture2D Lock;
        #endregion

        //Fonts
        public SpriteFont font;
        //
        //Buttons////////////////////////////////////////////////////////////////////////////////////////
        Button PLAY;
        Button Upgrades;

        Button Leftt1;
        Button Leftt2;
        Button Leftt3;
        Button Leftt4;
        Button Leftt5;
        Button Right1;
        Button Right2;
        Button Right3;
        Button Right4;
        Button Right5;

        Button SpaceshipLeft;
        Button SpaceshipRight;

        enum UpgradeMenustates
        {
            Right1,
            Right2,
            Right3,
            Right4,
            Right5,
            Left1,
            Left2,
            Left3,
            Left4,
            Left5,
            Spaceleft,
            SpaceRight,
        }
        UpgradeMenustates menustate;


        //Screen Size
        int Width;
        int Height;

        //PLayerShip
        new Ship PlayerShip;
        new Space_BackGround Space;
        new LVL playLVL;

        Color colour = new Color(255, 255, 255, 200);
        Color Fadecolor = new Color(255, 255, 255, 255);


        //key pressure timer
        int x;

        float SpinnerRotation;
        float scaleForProblems;

        int FiveFramesAnimation;

        public bool GoliathLocked;
        public bool ChaserLocked;

        public int TotalScore;


        struct DisplayMessage
        {
            public string Message;
            public TimeSpan DisplayTime;
            public int CurrentIndex;
            public Vector2 Position;
            public string DrawMessage;
            public Color DrawColor;
            public DisplayMessage(string message, TimeSpan displayTime, Vector2 position, Color color)
            {
                Message = message;
                DisplayTime = displayTime;
                CurrentIndex = 0;
                Position = position;
                DrawMessage = string.Empty;
                DrawColor = color;
            }

        }

        List<DisplayMessage> messages = new List<DisplayMessage>();
  

        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

          if (graphics.IsFullScreen == false) graphics.IsFullScreen = true;
          graphics.ApplyChanges();
            //States
            currentstate = State.Beggining;
            menustate = UpgradeMenustates.Left1;

            //
            //Audio
            Ketsap = 0;
            Blockbuster = 0;
            ButtonChoose = null;
            ButtonSwitch = null;

            //
            //Images
            MainMenuBackground = null;
            Gliese = null;
            PlayButton = null;
            UpgradesButton = null;
            Stats = null;


            //Screen size
            Width = GraphicsDevice.Viewport.Width;
            Height = GraphicsDevice.Viewport.Height;
            BegginingText = 0;

            //Ship
            PlayerShip = new Ship(1, Width, Height);
            Space = new Space_BackGround(1, Width, Height);

            x = 10;
            SpinnerRotation = 0f;
            scaleForProblems = (float)Width/1920;

            FiveFramesAnimation = 0;
            TotalScore = 0;

            playLVL = new LVL(Width, Height);

            GoliathLocked = true;
            ChaserLocked = true;


            base.Initialize();
      
        }
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Space.LoadContent_Space_Background(Content);
            PlayerShip.Load_content_Ship(Content);
            playLVL.LoadContent_LVL(Content);

            
            
            //Pescadero Font
            font = Content.Load<SpriteFont>("Game");
            //
            //Audio
            Ketsa = Content.Load<Song>("Ketsa_falling_sky");
            Space_BackGround_Music = Content.Load<Song>("Soulbringer_-_Space_Blockbuster");
            ButtonSwitch = Content.Load<SoundEffect>("ButtonSwitch");
            ButtonChoose = Content.Load<SoundEffect>("ButtonChoose");
            GameOver = Content.Load<Texture2D>("GameOver");
            Engine1 = Content.Load<SoundEffect>("Tier1_SpaceShip_Startup");
            Engine2 = Content.Load<SoundEffect>("Tier2_SpaceShip_Startup");
            Engine3 = Content.Load<SoundEffect>("Tier3_SpaceShip_Startup");
            Wrong = Content.Load<SoundEffect>("Wrong");

            //
            //Images
            MainMenuBackground = Content.Load<Texture2D>("MainMenuBackground");
            UpgradesBackground = Content.Load<Texture2D>("UpgradesMenu");
            UpgradesBackgroundSpinner = Content.Load<Texture2D>("UpgradesMenuSpinner");
            Gliese = Content.Load<Texture2D>("Gliese");
            PlayButton = Content.Load<Texture2D>("PLAY");
            UpgradesButton = Content.Load<Texture2D>("Upgrades");
            Stats = Content.Load<Texture2D>("Stats");
            ArrowLeft = Content.Load<Texture2D>("ArrowLeft");
            ArrowRight = Content.Load<Texture2D>("ArrowRight");
            StatBar = Content.Load<Texture2D>("StatBar");
            MusicPlate = Content.Load<Texture2D>("MusicPlate");
            Saboteur = Content.Load<Texture2D>("Saboteur");
            Chaser = Content.Load<Texture2D>("Chaser");
            Goliath = Content.Load<Texture2D>("Goliath");
            Lock = Content.Load<Texture2D>("Lock");

            //Buttons with images from above loaded, weight and height are the screen curren resolution
            PLAY = new Button(PlayButton, Width, Height); PLAY.setPositionandsize(new Vector2(Width / (float)2.25, Height /2), 1.0f); PLAY.Chosen = true;
            Upgrades = new Button(UpgradesButton, Width, Height); Upgrades.setPositionandsize(new Vector2(Width /(float)2.32, Height /(float)1.7), 1.3f); Upgrades.Chosen = false;

            //Buttons from the upgrade menu, Arrows Left:
            Leftt1 = new Button(ArrowLeft, Width, Height); Leftt1.setPositionandsize(new Vector2(Width / (float)2.8, Height / (float)4.1), 2f); Leftt1.Chosen = true;
            Leftt2 = new Button(ArrowLeft, Width, Height); Leftt2.setPositionandsize(new Vector2(Width / (float)2.8, Height / (float)3), 2f); Leftt2.Chosen = false;
            Leftt3 = new Button(ArrowLeft, Width, Height); Leftt3.setPositionandsize(new Vector2(Width / (float)2.8, Height / (float)2.36), 2f); Leftt3.Chosen = false;
            Leftt4 = new Button(ArrowLeft, Width, Height); Leftt4.setPositionandsize(new Vector2(Width / (float)2.8, Height / (float)1.97), 2f); Leftt4.Chosen = false;
            Leftt5 = new Button(ArrowLeft, Width, Height); Leftt5.setPositionandsize(new Vector2(Width / (float)2.8, Height / (float)1.65), 2f); Leftt5.Chosen = false;
            Right1 = new Button(ArrowRight, Width, Height); Right1.setPositionandsize(new Vector2(Width / (float)2.4, Height / (float)4.1), 2f); Right1.Chosen = false;
            Right2 = new Button(ArrowRight, Width, Height); Right2.setPositionandsize(new Vector2(Width / (float)2.4, Height / (float)3), 2f); Right2.Chosen = false;
            Right3 = new Button(ArrowRight, Width, Height); Right3.setPositionandsize(new Vector2(Width / (float)2.4, Height / (float)2.36), 2f); Right3.Chosen = false;
            Right4 = new Button(ArrowRight, Width, Height); Right4.setPositionandsize(new Vector2(Width / (float)2.4, Height / (float)1.97), 2f); Right4.Chosen = false;
            Right5 = new Button(ArrowRight, Width, Height); Right5.setPositionandsize(new Vector2(Width / (float)2.4, Height / (float)1.65), 2f); Right5.Chosen = false;

            SpaceshipLeft = new Button(ArrowLeft, Width, Height); SpaceshipLeft.setPositionandsize(new Vector2(Width / (float)1000, Height / (float)2.5), 2f); SpaceshipLeft.Chosen = false;
            SpaceshipLeft.visible = false;
            SpaceshipRight = new Button(ArrowRight, Width, Height); SpaceshipRight.setPositionandsize(new Vector2(Width / (float)4, Height / (float)2.5), 2f); SpaceshipRight.Chosen = false;

            //SpaceShips
            Ship1 = Content.Load<Texture2D>("Player_Ship_1");
            Ship2 = Content.Load<Texture2D>("Player_Ship_2");
            Ship3 = Content.Load<Texture2D>("Player_Ship_3");


        }



        protected override void UnloadContent()
        {

        }

        void UpdateMessages(GameTime gameTime)
        {
            if(messages.Count > 0)
            {
                for(int i = 0; i < messages.Count; i++)
                {
                    DisplayMessage dm = messages[i];
                    dm.DisplayTime -= gameTime.ElapsedGameTime;
                    if(dm.DisplayTime <= TimeSpan.Zero)
                    {
                        messages.RemoveAt(i);
                    }
                    else
                    {
                        messages[i] = dm;
                    }

                }
            }


        }

        protected override void Update(GameTime gameTime)
        {

                   
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) && currentstate == State.Main_Menu && x <= 0)
                Exit();

            PlayerShip.Is_ship_Coliding(playLVL.Wave1);
            PlayerShip.Is_ship_Coliding(playLVL.Wave3);
            PlayerShip.Is_ship_Coliding(playLVL.Wave4);


            foreach (Projectile P in playLVL.EnemyProjectilesList)
            {


            }
        

            switch (currentstate)
            {
                #region MainMenu
                
                case State.Main_Menu:
                    
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        ButtonSwitch.Play();
                        PLAY.Chosen = false;
                        Upgrades.Chosen = true;

                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        ButtonSwitch.Play();
                        PLAY.Chosen = true;
                        Upgrades.Chosen = false;
                    }


                    if (PLAY.Chosen && Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        messages.Add(new DisplayMessage("Use W, S, D Keys to control your ship \n\n Use Space Bar to shoot", TimeSpan.FromSeconds(5), new Vector2(Width * (float)0.25, Height * (float)0.45), Color.White*0.5f));
                        if (PlayerShip.Ship_Tier == 1) Engine1.Play();
                        if (PlayerShip.Ship_Tier == 2) Engine2.Play();
                        if (PlayerShip.Ship_Tier == 3) Engine3.Play();
                        Ketsap = 0;
                        ButtonChoose.Play();
                        MediaPlayer.Stop();
                        currentstate = State.Playing;
 
                        
                    }
                    if (Upgrades.Chosen && Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        ButtonChoose.Play();
                        currentstate = State.UpgradesMenu;
                        x = 15;
                    }

                    //Music/////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (Ketsap == 0)
                    {
                        MediaPlayer.Play(Ketsa);
                        Ketsap = 1;
                    }
                    //

                    break;
                    #endregion

                #region Upgrades Menu
                    

                case State.UpgradesMenu:
                    if (TotalScore > 200000)
                    {
                        ChaserLocked = false;
                    }
                    if (TotalScore > 550000)
                    {
                        GoliathLocked = false;
                    }
                    PlayerShip.Update_Tier();
                    if (x <= 0)
                    {
                        if (Ketsap == 0)
                        {
                            MediaPlayer.Stop();
                            Ketsap = (int)MediaState.Playing;
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            if (PlayerShip.Ship_Tier == 2 && ChaserLocked == true)
                            {
                                Wrong.Play();
                            }

                            else if (PlayerShip.Ship_Tier == 3 && GoliathLocked == true)
                            {
                                Wrong.Play();
                            }
                            else
                            {
                                currentstate = State.Main_Menu;
                                x = 20;
                            }
                        }
                        #region Player Presses UP arrow
                        if (Keyboard.GetState().IsKeyDown(Keys.Up) && x <= 0)
                        {
                            x = 15;
                            switch (menustate)
                            {
                                case UpgradeMenustates.Right1:
                                    break;

                                case UpgradeMenustates.Right2:
                                    menustate = UpgradeMenustates.Right1;
                                    Right2.Chosen = false;
                                    Right1.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Right3:
                                    menustate = UpgradeMenustates.Right2;
                                    Right3.Chosen = false;
                                    Right2.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Right4:
                                    menustate = UpgradeMenustates.Right3;
                                    Right4.Chosen = false;
                                    Right3.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Right5:
                                    menustate = UpgradeMenustates.Right4;
                                    Right5.Chosen = false;
                                    Right4.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Left1:
                                    break;
                                case UpgradeMenustates.Left2:
                                    menustate = UpgradeMenustates.Left1;
                                    Leftt2.Chosen = false;
                                    Leftt1.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left3:
                                    menustate = UpgradeMenustates.Left2;
                                    Leftt3.Chosen = false;
                                    Leftt2.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left4:
                                    menustate = UpgradeMenustates.Left3;
                                    Leftt4.Chosen = false;
                                    Leftt3.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left5:
                                    menustate = UpgradeMenustates.Left4;
                                    Leftt5.Chosen = false;
                                    Leftt4.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Spaceleft:
                                    break;

                                case UpgradeMenustates.SpaceRight:
                                    break;

                            }
                        }
                        #endregion

                        #region Player Presses Down Key
                        if (Keyboard.GetState().IsKeyDown(Keys.Down) && x <= 0)
                        {
                            x = 15;
                            switch (menustate)
                            {
                                case UpgradeMenustates.Right1:
                                    menustate = UpgradeMenustates.Right2;
                                    Right1.Chosen = false;
                                    Right2.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Right2:
                                    menustate = UpgradeMenustates.Right3;
                                    Right2.Chosen = false;
                                    Right3.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Right3:
                                    menustate = UpgradeMenustates.Right4;
                                    Right3.Chosen = false;
                                    Right4.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Right4:
                                    menustate = UpgradeMenustates.Right5;
                                    Right4.Chosen = false;
                                    Right5.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Right5:
                                    break;

                                case UpgradeMenustates.Left1:
                                    menustate = UpgradeMenustates.Left2;
                                    Leftt1.Chosen = false;
                                    Leftt2.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left2:
                                    menustate = UpgradeMenustates.Left3;
                                    Leftt2.Chosen = false;
                                    Leftt3.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left3:
                                    menustate = UpgradeMenustates.Left4;
                                    Leftt3.Chosen = false;
                                    Leftt4.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left4:
                                    menustate = UpgradeMenustates.Left5;
                                    Leftt4.Chosen = false;
                                    Leftt5.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left5:
                                    break;

                                case UpgradeMenustates.Spaceleft:
                                    break;

                                case UpgradeMenustates.SpaceRight:
                                    break;

                            }

                        }
                        #endregion

                        #region Player Presses Left Key
                        if (Keyboard.GetState().IsKeyDown(Keys.Left) && x <= 0)
                        {
                            x = 15;
                            switch (menustate)
                            {
                                case UpgradeMenustates.Right1:
                                    menustate = UpgradeMenustates.Left1;
                                    Right1.Chosen = false;
                                    Leftt1.Chosen = true;
                                    ButtonSwitch.Play();

                                    break;

                                case UpgradeMenustates.Right2:
                                    menustate = UpgradeMenustates.Left2;
                                    Right2.Chosen = false;
                                    Leftt2.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Right3:
                                    menustate = UpgradeMenustates.Left3;
                                    Right3.Chosen = false;
                                    Leftt3.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Right4:
                                    menustate = UpgradeMenustates.Left4;
                                    Right4.Chosen = false;
                                    Leftt4.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Right5:
                                    menustate = UpgradeMenustates.Left4;
                                    Right4.Chosen = false;
                                    Leftt4.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Left1:
                                    if (PlayerShip.Ship_Tier == 3)
                                    {
                                        menustate = UpgradeMenustates.Spaceleft;
                                        SpaceshipLeft.Chosen = true;
                                        Leftt1.Chosen = false;
                                        ButtonSwitch.Play();
                                        break;
                                    }
                                    else
                                    {
                                        menustate = UpgradeMenustates.SpaceRight;
                                        SpaceshipRight.Chosen = true;
                                        Leftt1.Chosen = false;
                                        ButtonSwitch.Play();
                                        break;
                                    }
                                case UpgradeMenustates.Left2:
                                    menustate = UpgradeMenustates.SpaceRight;
                                    Leftt2.Chosen = false;
                                    SpaceshipRight.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left3:
                                    menustate = UpgradeMenustates.SpaceRight;
                                    Leftt3.Chosen = false;
                                    SpaceshipRight.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left4:
                                    menustate = UpgradeMenustates.SpaceRight;
                                    Leftt4.Chosen = false;
                                    SpaceshipRight.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left5:
                                    menustate = UpgradeMenustates.SpaceRight;
                                    Leftt5.Chosen = false;
                                    SpaceshipRight.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Spaceleft:
                                    break;

                                case UpgradeMenustates.SpaceRight:
                                    
                                    if (PlayerShip.Ship_Tier != 1)
                                    {
                                        menustate = UpgradeMenustates.Spaceleft;
                                        SpaceshipRight.Chosen = false;
                                        SpaceshipLeft.Chosen = true;
                                        ButtonSwitch.Play();
                                    }
                                    break;
                                    
                                    

                            }
                        }
                        #endregion

                        #region Player Presses Right Key
                        if (Keyboard.GetState().IsKeyDown(Keys.Right) && x <= 0)
                        {
                            x = 15;
                            switch (menustate)
                            {
                                case UpgradeMenustates.Right1:
                                    break;

                                case UpgradeMenustates.Right2:
                                    break;

                                case UpgradeMenustates.Right3:
                                    break;

                                case UpgradeMenustates.Right4:
                                    break;

                                case UpgradeMenustates.Right5:
                                    break;

                                case UpgradeMenustates.Left1:
                                    menustate = UpgradeMenustates.Right1;
                                    Leftt1.Chosen = false;
                                    Right1.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left2:
                                    menustate = UpgradeMenustates.Right2;
                                    Leftt2.Chosen = false;
                                    Right2.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left3:
                                    menustate = UpgradeMenustates.Right3;
                                    Leftt3.Chosen = false;
                                    Right3.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left4:
                                    menustate = UpgradeMenustates.Right4;
                                    Leftt4.Chosen = false;
                                    Right4.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;
                                case UpgradeMenustates.Left5:
                                    menustate = UpgradeMenustates.Right5;
                                    Leftt5.Chosen = false;
                                    Right5.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                                case UpgradeMenustates.Spaceleft:
                                    if (PlayerShip.Ship_Tier == 3)
                                    {
                                        menustate = UpgradeMenustates.Left1;
                                        SpaceshipLeft.Chosen = false;
                                        Leftt1.Chosen = true;
                                        ButtonSwitch.Play();
                                        break;
                                    }
                                    else
                                    {
                                        menustate = UpgradeMenustates.SpaceRight;
                                        SpaceshipLeft.Chosen = false;
                                        SpaceshipRight.Chosen = true;
                                        ButtonSwitch.Play();
                                        break;
                                    }

                                case UpgradeMenustates.SpaceRight:
                                    menustate = UpgradeMenustates.Left1;
                                    SpaceshipRight.Chosen = false;
                                    Leftt1.Chosen = true;
                                    ButtonSwitch.Play();
                                    break;

                            }
                        }
                        #endregion

                        #region Player Presses Enter
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter) && x <= 0)
                        {
                            x = 15;
                            switch (menustate)
                            {
                                case UpgradeMenustates.Right1:
                                    if (PlayerShip.AvaliablePoints > 0 && PlayerShip.Ship_Total_HP_PTS < 5)
                                    {
                                        PlayerShip.Ship_Total_HP += 100;
                                        PlayerShip.Ship_Total_HP_PTS += 1;
                                        PlayerShip.AvaliablePoints -= 1;
                                    }
                                    break;

                                case UpgradeMenustates.Right2:
                                    if (PlayerShip.AvaliablePoints > 0 && PlayerShip.Ship_Endurance_PTS < 5)
                                    {
                                        PlayerShip.Ship_Endurance += 1;
                                        PlayerShip.Ship_Endurance_PTS += 1;
                                        PlayerShip.AvaliablePoints -= 1;
                                    }
                                    break;

                                case UpgradeMenustates.Right3:
                                    if (PlayerShip.AvaliablePoints > 0 && PlayerShip.Ship_Shooting_Speed_PTS < 5)
                                    {
                                        PlayerShip.Ship_Shooting_Speed -= 1;
                                        PlayerShip.Ship_Shooting_Speed_PTS += 1;
                                        PlayerShip.AvaliablePoints -= 1;
                                    }
                                    break;

                                case UpgradeMenustates.Right4:
                                    if (PlayerShip.AvaliablePoints > 0 && PlayerShip.Projectile_Damage_PTS < 5)
                                    {
                                        PlayerShip.Projectile_Damage += 20;
                                        PlayerShip.Projectile_Damage_PTS += 1;
                                        PlayerShip.AvaliablePoints -= 1;
                                    }
                                    break;

                                case UpgradeMenustates.Right5:
                                    if (PlayerShip.AvaliablePoints > 0 && PlayerShip.Ship_Speed_PTS < 5)
                                    {
                                        PlayerShip.Ship_Speed += 1;
                                        PlayerShip.Ship_Speed_PTS += 1;
                                        PlayerShip.AvaliablePoints -= 1;
                                    }
                                    break;

                                case UpgradeMenustates.Left1:
                                    if (PlayerShip.AvaliablePoints < 10 && PlayerShip.Ship_Total_HP_PTS > 0)
                                    {
                                        PlayerShip.Ship_Total_HP -= 100;
                                        PlayerShip.Ship_Total_HP_PTS -= 1;
                                        PlayerShip.AvaliablePoints += 1;
                                    }
                                    break;
                                case UpgradeMenustates.Left2:
                                    if (PlayerShip.AvaliablePoints < 10 && PlayerShip.Ship_Endurance_PTS > 0)
                                    {
                                        PlayerShip.Ship_Endurance -= 1;
                                        PlayerShip.Ship_Endurance_PTS -= 1;
                                        PlayerShip.AvaliablePoints += 1;
                                    }
                                    break;
                                case UpgradeMenustates.Left3:
                                    if (PlayerShip.AvaliablePoints < 10 && PlayerShip.Ship_Shooting_Speed_PTS > 0)
                                    {
                                        PlayerShip.Ship_Shooting_Speed += 1;
                                        PlayerShip.Ship_Shooting_Speed_PTS -= 1;
                                        PlayerShip.AvaliablePoints += 1;
                                    }
                                    break;
                                case UpgradeMenustates.Left4:
                                    if (PlayerShip.AvaliablePoints < 10 && PlayerShip.Projectile_Damage_PTS > 0)
                                    {
                                        PlayerShip.Projectile_Damage -= 20;
                                        PlayerShip.Projectile_Damage_PTS -= 1;
                                        PlayerShip.AvaliablePoints += 1;
                                    }
                                    break;
                                case UpgradeMenustates.Left5:
                                    if (PlayerShip.AvaliablePoints < 10 && PlayerShip.Ship_Speed_PTS > 0)
                                    {
                                        PlayerShip.Ship_Speed -= 1;
                                        PlayerShip.Ship_Speed_PTS -= 1;
                                        PlayerShip.AvaliablePoints += 1;
                                    }
                                    break;

                                case UpgradeMenustates.Spaceleft:
                                    if (PlayerShip.Ship_Tier == 2)
                                    {
                                        PlayerShip.Ship_Tier = 1;
                                        SpaceshipLeft.visible = false;
                                        SpaceshipRight.Chosen = true;
                                        menustate = UpgradeMenustates.SpaceRight;
                                    }
                                    if (PlayerShip.Ship_Tier == 3)
                                    {
                                        PlayerShip.Ship_Tier = 2;
                                        SpaceshipRight.visible = true;
                                        SpaceshipRight.Chosen = false;
                                    }
                                    PlayerShip.AvaliablePoints = 10;
                                    PlayerShip.Projectile_Damage_PTS = 0;
                                    PlayerShip.Ship_Endurance_PTS = 0;
                                    PlayerShip.Ship_Shooting_Speed_PTS = 0;
                                    PlayerShip.Ship_Speed_PTS = 0;
                                    PlayerShip.Ship_Total_HP_PTS = 0;

                                    break;
                                case UpgradeMenustates.SpaceRight:
                                    if (PlayerShip.Ship_Tier == 2)
                                    {
                                        PlayerShip.Ship_Tier = 3;
                                        SpaceshipRight.visible = false;
                                        SpaceshipLeft.Chosen = true;
                                        menustate = UpgradeMenustates.Spaceleft;
                                    }
                                    if (PlayerShip.Ship_Tier == 1)
                                    {
                                        PlayerShip.Ship_Tier = 2;
                                        SpaceshipLeft.visible = true;
                                        SpaceshipLeft.Chosen = false;
                                    }
                                    PlayerShip.AvaliablePoints = 10;
                                    PlayerShip.Projectile_Damage_PTS = 0;
                                    PlayerShip.Ship_Endurance_PTS = 0;
                                    PlayerShip.Ship_Shooting_Speed_PTS = 0;
                                    PlayerShip.Ship_Speed_PTS = 0;
                                    PlayerShip.Ship_Total_HP_PTS = 0;
                                    break;

                            }
                        }
                        #endregion

                    }
                    break;
                #endregion

                #region PLaying
             

                case State.Playing:
                    {
                        if (Blockbuster == 0)
                        {
                            MediaPlayer.Play(Space_BackGround_Music);
                            Blockbuster = 1;
                        }
                        //
                if(PlayerShip.score > 200000 && PlayerShip.score < 250000 && TotalScore < 200000)
                        {
                            messages.Add(new DisplayMessage("Chaser Unlocked", TimeSpan.FromSeconds(1.0), new Vector2(Width * (float)0.7, Height * (float)0.9), Color.White));
                        }
                if (PlayerShip.score > 400000 && PlayerShip.score < 450000 && TotalScore < 400000)
                        {
                            messages.Add(new DisplayMessage("Goliath Unlocked", TimeSpan.FromSeconds(1.0), new Vector2(Width * (float)0.7, Height * (float)0.9), Color.White));
                        }


                        if (Keyboard.GetState().IsKeyDown(Keys.Escape) && x <= 0 || PlayerShip.Ship_Is_Destructed == true || playLVL.MB.isdestroyed)
                    {
                            MediaPlayer.Stop();
                            Blockbuster = 0;
                        #region game end

                            if (PlayerShip.score > TotalScore) TotalScore = PlayerShip.score;

                            
                            PlayerShip.Update_Tier();
                            Space.Space_BackGround_DrawPosition = new Rectangle(0, 0, Width, Height);
                            Space.Space_BackGround_DrawPosition2 = new Rectangle(Width, 0, Width, Height);
                            playLVL.B1.isdestroyed = false;
                            playLVL.B2.isdestroyed = false;
                            playLVL.B3.isdestroyed = false;

                            for (int i = 0; i < 6; i++)
                            {
                                playLVL.Spawntimes[i] = 0;
                            }

                            PlayerShip.Ship_Is_Destructed = false;

                            playLVL.Wave1.Clear();
                            playLVL.Wave3.Clear();
                            playLVL.Wave4.Clear();

                            PlayerShip.ExplosionsList.Clear();
                            PlayerShip.Projectiles_List.Clear();
                            playLVL.EnemyProjectilesList.Clear();

                            playLVL.wave1Time = 0;
                            playLVL.wave2Time = 0;
                            playLVL.wave3Time = 0;

                            x = 15;
                            Space.Space_BackGround_DrawPosition.X = 0;
                            currentstate = State.Gameover;
                           
                  }

                        Space.UpdateContent_Space_Background(gameTime);
                        PlayerShip.Update_Ship(gameTime);
                        playLVL.UpdateContent_LVL(gameTime);

                        if(PlayerShip.Ship_ColisionBox.Intersects(playLVL.LVLpowerup.ColisionBox))
                        {
                            PlayerShip.Ship_Total_HP += 5;
                            if (PlayerShip.Ship_Speed < 17 && PlayerShip.Ship_Speed > 2)
                                PlayerShip.Ship_Speed += 1;
                            if (PlayerShip.Ship_Shooting_Speed > 11) PlayerShip.Ship_Shooting_Speed -= 1;
                            if (PlayerShip.Projectile_Damage < 300) PlayerShip.Projectile_Damage += 2;
                            playLVL.LVLpowerup.Isdestroyed = true;
                        }
                        foreach (Projectile P in playLVL.EnemyProjectilesList)
                        {
                            if (P.Projectile_ColisionBox.Intersects(PlayerShip.Ship_ColisionBox))
                            {
                                P.isvisible = false;
                                if (PlayerShip.Ship_Shooting_Speed < 20) PlayerShip.Ship_Shooting_Speed++;
                                if (PlayerShip.Projectile_Damage > 100) PlayerShip.Projectile_Damage--;
                                if (PlayerShip.Ship_Speed > 5) PlayerShip.Ship_Speed--;
                                PlayerShip.Ship_Total_HP -= P.ProjectileDamage;
                                PlayerShip.isbeinghit = true;

                            }
                        }
                        #region game end

                        break;
                    }
                #endregion
                #endregion
                #endregion

                #region Gameover

                case State.Gameover:

                    

                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && x <= 0)
                    {
                        PlayerShip.score = 0;
                        Ketsap = 0;
                        x = 15;
                        currentstate = State.Main_Menu;
                    }
                        break;
                #endregion

                case State.Beggining:
                    {


                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            x = 15;
                            messages.Clear();
                            currentstate = State.Main_Menu;
                        }

                        if (BegginingText == 100)
                        {
                            messages.Add(new DisplayMessage("\n\n\nWinter 2240, the tension between Mars colony and Earth rises. \n\n \n By one side Mars is avidly against human and animal dna \n\n\nfusion as it's still a very premature science and offten human \n\n\nbeings are born attrocitties that grow with countless dissieses \n\n\n and defects, on the other Earth has majorly voted for a evolution \n\n\n to the next Super Human, no matter what it takes.", TimeSpan.FromSeconds(30), new Vector2(Width*(float)0.02, Height* (float)0.1), Color.White));
                        }

                        if (BegginingText == 2100)
                        {
                            messages.Add(new DisplayMessage("\n\n\nSummer 2250 Mars claims it's independency from earth. \n\n\nThe great colonial war beggins. Billions die. \n\n\n\n\n\n 1 January 2301, a cease fire is called, both our plannet and earth \n\n\n are completly destroyed and no longer can support life \n\n\n We need to find a new place, but so do they.", TimeSpan.FromSeconds(25), new Vector2(Width * (float)0.02, Height * (float)0.1), Color.White));
                        }

                        if (BegginingText == 3800)
                        {
                            messages.Add(new DisplayMessage("\n\n\n\n\n\n\n\n\n\n Our best chances are Gliese 581 c and Gliese 581 g \n\n\n Both you and me captain know the cease fire wont last long \n\n\n our future is in your hands,", TimeSpan.FromSeconds(10), new Vector2(Width * (float)0.02, Height * (float)0.1), Color.White));
                        }

                        if (BegginingText == 4600) currentstate = State.Main_Menu;
                        break;
                    }
            }
          



            x--;
            BegginingText++;
            if (FiveFramesAnimation < 24)
            {
                FiveFramesAnimation += 1;
            }
            else FiveFramesAnimation = 0;

            if (Fadecolor.B > 0)
            {
                Fadecolor.A--;
                Fadecolor.R--;
                Fadecolor.G--;
                Fadecolor.B--;
            }
            if (SpinnerRotation < 360) SpinnerRotation += (float)0.05; else if (SpinnerRotation >= 360) SpinnerRotation = 0;
            UpdateMessages(gameTime);
 
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (currentstate)
            {
                #region MainMenu Draw
                case State.Main_Menu:
                    
                    spriteBatch.Draw(MainMenuBackground, new Rectangle(0, 0, Width, Height), Color.White);
                    spriteBatch.Draw(MusicPlate, new Rectangle((int)(Width * 0.84), (int)(Height * 0.83), Width / 7, Height / 6), Fadecolor);
                    spriteBatch.DrawString(font, "Falling Sky \n     Ketsa", new Vector2(Width * (float)0.83, Height * (float)0.88), Fadecolor);
                    spriteBatch.Draw(Gliese, new Rectangle((int)Width / 3, (int)Height / 8, (int)Width / 3, (int)Height / 3), Color.White);
                    Upgrades.Draw(spriteBatch);
                    PLAY.Draw(spriteBatch);

                    break;
                #endregion

                #region UpgradesMenuDraw
                case State.UpgradesMenu:
                    
                    spriteBatch.Draw(UpgradesBackground, new Rectangle(0, 0, Width, Height), Color.White);

                    spriteBatch.Draw(UpgradesBackgroundSpinner, new Vector2(Width/(float)4.2, Height/(float)2.3), null, Color.White, SpinnerRotation, new Vector2(Width / (float)4.2 + UpgradesBackgroundSpinner.Width/2*scaleForProblems, Height / (float)2.3+ UpgradesBackgroundSpinner.Height/2*scaleForProblems), (float)scaleForProblems, SpriteEffects.None, 0f);

                    

                    //StatBar

                        spriteBatch.Draw(StatBar, new Rectangle((int)(Width / 1.8), (int)(Height / 3.9), (int)(PlayerShip.Ship_Total_HP / 7.333), Height / 14), colour);
                        spriteBatch.Draw(StatBar, new Rectangle((int)(Width / 1.8), (int)(Height / 2.9), (int)(PlayerShip.Ship_Endurance * 33.333), Height / 14), colour);
                        spriteBatch.Draw(StatBar, new Rectangle((int)(Width / 1.8), (int)(Height / 2.27),(int)((28 - PlayerShip.Ship_Shooting_Speed) * 14.28), Height / 14), colour);
                        spriteBatch.Draw(StatBar, new Rectangle((int)(Width / 1.8), (int)(Height / 1.87),(int)(PlayerShip.Projectile_Damage * 1.363), Height / 14), colour);
                        spriteBatch.Draw(StatBar, new Rectangle((int)(Width / 1.8), (int)(Height / 1.6), (int)(PlayerShip.Ship_Speed * 17.64), Height / 14), colour);

                    //Ship
                    #region ship tier 1 draw
                    if (PlayerShip.Ship_Tier == 1)
                    {
                        spriteBatch.Draw(Ship1, new Rectangle(Width / 7, (int)(Height / 2.7), Width / 6, Height / 7), Color.White);
                        spriteBatch.Draw(Saboteur, new Rectangle(Width / 6, (int)(Height / 1.9), 100, 50), Color.White);
                    }
                    #endregion

                    #region Ship tier 2 draw
                    if (PlayerShip.Ship_Tier == 2)
                    {
                        spriteBatch.Draw(Chaser, new Rectangle(Width / 6, (int)(Height / 1.9), 100, 50), Color.White);
                        if (ChaserLocked == true)
                        {
                            spriteBatch.Draw(Ship2, new Rectangle(Width / 7, (int)(Height / 2.7), Width / 6, Height / 6), Color.Gray);
                            spriteBatch.Draw(Lock, new Rectangle((int)(Width / 6.1), (int)(Height / 2.9), (int)(Width / 7.6), (int)(Height / 3.5)), Color.Gray * 0.5f);
                        }
                        else spriteBatch.Draw(Ship2, new Rectangle(Width / 7, (int)(Height / 2.7), Width / 6, Height / 6), Color.White);
                    }
                    #endregion

                    #region Ship Tier 3 Draw

                    if (PlayerShip.Ship_Tier == 3)
                    {
                      

                        if (FiveFramesAnimation <= 4)
                        {
                            spriteBatch.Draw(Goliath, new Rectangle(Width / 6, (int)(Height / 1.9), 100, 50), new Rectangle(0, 0, 436, 200), Color.White);
                        }
                        if (FiveFramesAnimation <= 9 && FiveFramesAnimation > 4)
                        {
                            spriteBatch.Draw(Goliath, new Rectangle(Width / 6, (int)(Height / 1.9), 100, 50), new Rectangle(436, 0, 436, 200), Color.White);
                        }
                        if (FiveFramesAnimation <= 15 && FiveFramesAnimation > 9)
                        {
                            spriteBatch.Draw(Goliath, new Rectangle(Width / 6, (int)(Height / 1.9), 100, 50), new Rectangle(872, 0, 436, 200), Color.White);
                        }
                        if (FiveFramesAnimation <= 20 && FiveFramesAnimation > 15)
                        {
                            spriteBatch.Draw(Goliath, new Rectangle(Width / 6, (int)(Height / 1.9), 100, 50), new Rectangle(1308, 0, 436, 200), Color.White);
                        }
                        if (FiveFramesAnimation < 26 && FiveFramesAnimation > 20)
                        {
                            spriteBatch.Draw(Goliath, new Rectangle(Width / 6, (int)(Height / 1.9), 100, 50), new Rectangle(1744, 0, 436, 200), Color.White);
                        }

                        if (GoliathLocked == true)
                        {
                            spriteBatch.Draw(Ship3, new Rectangle(Width / 7, (int)(Height / 2.7), Width / 6, Height / 6), Color.Gray);
                            spriteBatch.Draw(Lock, new Rectangle((int)(Width / 6.1), (int)(Height / 2.9), (int)(Width / 7.6), (int)(Height / 3.5)), Color.Gray * 0.5f);
                        }
                        else spriteBatch.Draw(Ship3, new Rectangle(Width / 7, (int)(Height / 2.7), Width / 6, Height / 6), Color.White);
                    }
                    #endregion

                    spriteBatch.Draw(Stats, new Rectangle((int)(Width/ 2.6), (int)(Height / 4.5), Width / 2, Height / 2), colour);
                  

                    //Buttons
                    Leftt1.Draw(spriteBatch);
                    Leftt2.Draw(spriteBatch);
                    Leftt3.Draw(spriteBatch);
                    Leftt4.Draw(spriteBatch);
                    Leftt5.Draw(spriteBatch);
                    Right1.Draw(spriteBatch);
                    Right2.Draw(spriteBatch);
                    Right3.Draw(spriteBatch);
                    Right4.Draw(spriteBatch);
                    Right5.Draw(spriteBatch);
                    SpaceshipLeft.Draw(spriteBatch);
                    SpaceshipRight.Draw(spriteBatch);
                    //

                    spriteBatch.DrawString(font, "Avaliable Points  " + PlayerShip.AvaliablePoints.ToString(), new Vector2(Width * (float)0.6, Height *(float)0.75), Color.DimGray);
                    spriteBatch.DrawString(font, "Press ESC to Save and Leave", new Vector2(Width * (float)0.47, Height * (float)0.80), Color.DimGray);
                    
                    break;
                #endregion

                case State.Playing:
                    Space.DrawContent_Space_BackGround(spriteBatch,playLVL.wave1Time ,playLVL.wave2Time, playLVL.wave3Time);
                    PlayerShip.Draw_Ship(spriteBatch, font);
                    playLVL.DrawContent_LVL(spriteBatch, font);

                    spriteBatch.DrawString(font, PlayerShip.Ship_Total_HP.ToString(), new Vector2(Width*(float)0.02, Height*(float)0.01), Color.White * 0.5f);



                    break;
                case State.Gameover:
                    spriteBatch.Draw(GameOver, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.DrawString(font, "Your Score was " + PlayerShip.score.ToString(), new Vector2(Width/(float)2.9, Height/2), Color.White);
                    spriteBatch.DrawString(font, "Your Score Maximum Score is " + TotalScore.ToString(), new Vector2(Width / (float)4, Height *(float)0.6), Color.White);


                    break;
            }

            DrawMessages();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawMessages()
        {

            if(messages.Count >0)
                for(int i = 0; i < messages.Count; i++)
                {
                    DisplayMessage dm = messages[i];
                    dm.DrawMessage += dm.Message[dm.CurrentIndex].ToString();
                    spriteBatch.DrawString(font, dm.DrawMessage, dm.Position, dm.DrawColor);
                    if(dm.CurrentIndex != dm.Message.Length -1)
                    {
                        dm.CurrentIndex++;
                        messages[i] = dm;
                    }
                }
        }
    }
}

