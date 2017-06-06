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
    class Projectile
    { 

        public Texture2D Projectile_Image;
        public Rectangle Projectile_ColisionBox;
        public bool isvisible;
        public int ShootSpeed;
        public int ProjectileDamage;
  

    public Projectile(Texture2D texture)
    {

            isvisible = true;
            Projectile_Image = texture;
            Projectile_ColisionBox = new Rectangle(10, 10, 10, 5);
            ShootSpeed = 10;
            ProjectileDamage = 100;
        }




    public void Draw_Projectile(SpriteBatch spritebatch)
        {
            if (isvisible)
            {
                if(ShootSpeed > 10 &&  ShootSpeed < 14)
                spritebatch.Draw(Projectile_Image, Projectile_ColisionBox, Color.Blue);
                if (ShootSpeed > 13 && ShootSpeed < 18)
                spritebatch.Draw(Projectile_Image, Projectile_ColisionBox, Color.Red);
                if (ShootSpeed > 17 &&  ShootSpeed< 30)
                spritebatch.Draw(Projectile_Image, Projectile_ColisionBox, Color.White);
            }
        }
    }
}
