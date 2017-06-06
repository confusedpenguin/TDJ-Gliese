using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace TDJ_Gliese
{
    class EnemyShip
    {

        public Texture2D texture;
        public bool isdestroyed;
        public bool isboss;
        public bool isbeingdamaged;

        public Rectangle ColisionBox;
        public int colisiondamage;

        public int Health;
        bool wavingup;

        int ScreenWidth, ScreenHeigth;

        public int ShootTimer;


        public EnemyShip(Texture2D Enemy, int Width, int Height)
        {
            ShootTimer = 10;

            texture = Enemy;
            wavingup = true;

            isbeingdamaged = false;
            isdestroyed = false;
            isboss = false;

            ScreenWidth = Width;
            ScreenHeigth = Height;

            ColisionBox = new Rectangle(ScreenWidth, ScreenHeigth/3, ScreenWidth/6, ScreenHeigth/3);
            colisiondamage = 100;
            Health = 1000;

         
        }

        public void Update(GameTime gametime)
        {

            if(isboss)
            {
                if(ColisionBox.X > ScreenWidth * 0.8)
                ColisionBox.X--;
                if (wavingup && ColisionBox.Y > ScreenHeigth/20 - 1)
                    ColisionBox.Y--;
                if (ColisionBox.Y == ScreenHeigth / 20) wavingup = false;

                if (!wavingup && ColisionBox.Y < ScreenHeigth -ColisionBox.Height +1)
                    ColisionBox.Y++;
                if (ColisionBox.Y == ScreenHeigth - ColisionBox.Height) wavingup = true;

            }
            else if (!isboss) ColisionBox.X -= 5;
            if (ColisionBox.X < 0 - ColisionBox.Width) isdestroyed = true;

            ShootTimer--;
            if (ShootTimer <= -1) ShootTimer = 10;

        }
        public void Draw(SpriteBatch spritebatch)
        {
            if(isbeingdamaged == false)
            if (isdestroyed == false) spritebatch.Draw(texture, ColisionBox, Color.White);

            if(isbeingdamaged == true)
            if (isdestroyed == false) spritebatch.Draw(texture, ColisionBox, Color.Red);



        }

    }
}