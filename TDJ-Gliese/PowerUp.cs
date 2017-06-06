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
    class PowerUp
    {

        public Texture2D texture;
        public bool Isdestroyed;
        public Rectangle ColisionBox;


        int ScreenWidth, ScreenHeigth;



        public PowerUp(Texture2D Enemy, int Width, int Height)
        {
            
            texture = Enemy;
            
            Isdestroyed = false;
  
            ScreenWidth = Width;
            ScreenHeigth = Height;

            ColisionBox = new Rectangle(ScreenWidth, ScreenHeigth / 3, ScreenWidth / 20, ScreenHeigth / 10);



        }

        public void Update(GameTime gametime)
        {

            ColisionBox.X-= 5;
            if (ColisionBox.X < 150) Isdestroyed = true;

        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (!Isdestroyed) spritebatch.Draw(texture, ColisionBox, Color.White * 0.5f);

        }

    }
}