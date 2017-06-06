using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace TDJ_Gliese.Content
{
    class Boom
    {

        Texture2D ExplosionImg;
        public Rectangle Explosion_ColisionBox;
        public int x;
        bool critical;


        public Boom(Texture2D texture)
        {
            ExplosionImg = texture;
            x = 0;


        }
        public void Draw_Explosion(SpriteBatch spritebatch)
        {

            if (x < 5 && x >= 0)
            {
                spritebatch.Draw(ExplosionImg, Explosion_ColisionBox, new Rectangle(0, 0, 32, 32), Color.White);
            }
            if (x <10  && x >=5)
            {
                spritebatch.Draw(ExplosionImg, Explosion_ColisionBox, new Rectangle(31, 0, 32, 32), Color.White);
            }
            if (x < 15 && x >= 10)
            {
                spritebatch.Draw(ExplosionImg, Explosion_ColisionBox, new Rectangle(62, 0, 32, 32), Color.White);
            }
            if (x < 20 && x >= 15)
            {
                spritebatch.Draw(ExplosionImg, Explosion_ColisionBox, new Rectangle(94, 0, 32, 32), Color.White);
            }
            if (x < 25 && x >= 20)
            {
                spritebatch.Draw(ExplosionImg, Explosion_ColisionBox, new Rectangle(126, 0, 32, 32), Color.White);
            }
            if (x < 30 && x >= 25)
            {
                spritebatch.Draw(ExplosionImg, Explosion_ColisionBox, new Rectangle(158, 0, 32, 32), Color.White);
            }
            if (x < 40 && x >= 30)
            {
                spritebatch.Draw(ExplosionImg, Explosion_ColisionBox, new Rectangle(190, 0, 32, 32), Color.White);
            }
            x++;

        }
    }
}
