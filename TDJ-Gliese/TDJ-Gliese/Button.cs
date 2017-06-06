using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TDJ_Gliese
{
    class Button
    {

        Texture2D Texture;
        Vector2 position;
        int color = 0;
        float size = 1.0f;

        public Vector2 screensize;
        public bool Chosen;
        public bool visible;


        public Button(Texture2D NewTexture, int Width, int Height)
        {
            Texture = NewTexture;
            screensize = new Vector2(Width, Height);
            Chosen = false;
            visible = true;
        }


        public void setPositionandsize(Vector2 NewPosition, float size)
        {
            position = NewPosition;
            this.size = size;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (visible)
            {
                if (Chosen)
                    spritebatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, (int)(size * screensize.X / 10), (int)(size * screensize.Y / 20)), Color.White);

                else if (!Chosen)
                    spritebatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, (int)(size * screensize.X / 10), (int)(size * screensize.Y / 20)), Color.Gray);
            }
        }

    }
}