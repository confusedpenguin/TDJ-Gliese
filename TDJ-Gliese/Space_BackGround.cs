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
    class Space_BackGround
    {
        #region Assets
        Texture2D Space_BackGround_Image1;
 
        #endregion


        public Rectangle Space_BackGround_DrawPosition;
        public Rectangle Space_BackGround_DrawPosition2;


        string Music_Name;
        int LVL;
        Vector2 ScreenSize;

        public int x;

        public Space_BackGround(int lvl, int Width, int Height)
        {
            LVL = lvl;
            Space_BackGround_Image1 = null;

            Music_Name = null;

            ScreenSize = new Vector2(Width, Height);
            Space_BackGround_DrawPosition = new Rectangle(0, 0, Width, Height);
            Space_BackGround_DrawPosition2 = new Rectangle(Width, 0, Width, Height);



        }
        Color Spacecolor = new Color(255, 255, 255, 255);

        public void LoadContent_Space_Background(ContentManager Content)
        {


            switch (LVL)
            {

                case 1:
                    Space_BackGround_Image1 = Content.Load<Texture2D>("Space_BackGround_Image_1.jpg");


                    
                    Music_Name = "Music:Space Blockbuster\nArtist: Soulbringer";

                    break;
            }
        }

        public void UpdateContent_Space_Background(GameTime GameTime)
        {

            Space_BackGround_DrawPosition.X -= 9;
            Space_BackGround_DrawPosition2.X -= 9;

            if (Space_BackGround_DrawPosition.X == 0)
            {
                if (Spacecolor.R > 1)
                {
                    Spacecolor.R -= 1;
                    Spacecolor.G -= 1;
                }
            }

        }

        public void DrawContent_Space_BackGround(SpriteBatch SpriteBatch, int wave1time, int wave2time, int wave3time)
        {
            if (wave1time > 7000 && wave2time < 1)
            {
                Space_BackGround_DrawPosition.X += 9;
                Space_BackGround_DrawPosition2.X += 9;
            }

            if (wave2time > 7000 && wave3time < 1)
            {
                Space_BackGround_DrawPosition.X += 9;
                Space_BackGround_DrawPosition2.X += 9;
            }


            SpriteBatch.Draw(Space_BackGround_Image1, Space_BackGround_DrawPosition, Spacecolor);
            SpriteBatch.Draw(Space_BackGround_Image1, Space_BackGround_DrawPosition2, new Rectangle(1920, 0, -1920, 1080), Spacecolor);

            if (Space_BackGround_DrawPosition.X < -ScreenSize.X) Space_BackGround_DrawPosition.X = (int)ScreenSize.X - 1;
            if (Space_BackGround_DrawPosition2.X < -ScreenSize.X) Space_BackGround_DrawPosition2.X = (int)ScreenSize.X - 1;


        }

    }

}

