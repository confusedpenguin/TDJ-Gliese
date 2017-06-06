using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using TDJ_Gliese.Content;

namespace TDJ_Gliese
{
    class Ship
    {

        //Ship section:Ship State:
        Texture2D Player_Ship_image1, Player_Ship_image2, Player_Ship_image3, Health_texture;
        Texture2D Explosion;
        public Rectangle Ship_ColisionBox, HealthBox;

        public bool Ship_Is_Destructed;
        public bool isbeinghit;

        public int Kind_Of_Power_Up;

        
        //Points Avaliable to spend and points already spent in each stat:

        public int AvaliablePoints;

        public int Ship_Speed_PTS;
        public int Ship_Endurance_PTS;
        public int Ship_Total_HP_PTS;
        public int Ship_Shooting_Speed_PTS;
        public int Projectile_Damage_PTS;


        //Ship Attributes:
        public int Ship_Tier;
        public int Ship_Speed, Ship_Endurance, Ship_Total_HP, Ship_Shooting_Speed, Projectile_Damage;
        public int Projectile_Delay;

        //Projectile Section:
        public Texture2D Projectile_Image;
        public List<Projectile> Projectiles_List;
        /// 
        int Width;
        int Height;

        Random Random;

        SoundEffect Shot1;
        SoundEffect Shot2;
        SoundEffect Shot3;
        SoundEffect Explosion1;

        public int score;

        Boom EnemyShipDies;
        public List<Boom> ExplosionsList;

 

        public Ship(int Tier, int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;

            Ship_Tier = Tier;

            #region Ship Stats Initializer
            switch (Tier)
            {
                case 1:
                    {
                        Ship_Total_HP = 1000;
                        Ship_Endurance = 2;
                        Ship_Shooting_Speed = 25;
                        Projectile_Damage = 100;
                        Ship_Speed = 8;
                        break;
                    }
                case 2:
                    {
                        Ship_Total_HP = 1500;
                        Ship_Endurance = 3;
                        Ship_Shooting_Speed = 19;
                        Projectile_Damage = 120;
                        Ship_Speed = 10;
                        break;
                    }
                case 3:
                    {
                        Ship_Total_HP = 1700;
                        Ship_Endurance = 4;
                        Ship_Shooting_Speed = 17;
                        Projectile_Damage = 100;
                        Ship_Speed = 12;
                        break;
                    }


            }
            #endregion

            Ship_Speed_PTS = 0; Ship_Endurance_PTS = 0; Ship_Total_HP_PTS = 0; Ship_Shooting_Speed_PTS = 0; Projectile_Damage_PTS = 0;
            Projectile_Delay = Ship_Shooting_Speed;

            Projectiles_List = new List<Projectile>();
            ExplosionsList = new List<Boom>();
            Projectile_Image = null;
            Health_texture = null;
            Player_Ship_image1 = null;
            Player_Ship_image2 = null;
            Player_Ship_image3 = null;

            Ship_ColisionBox = new Rectangle(0, Height/2, Width/12, Height/10);
            Ship_Is_Destructed = false;
            Projectile_Image = null;
            HealthBox = new Rectangle(0, 0, 0, 0);
            Kind_Of_Power_Up = 1;
            AvaliablePoints = 10;
            score = 0;
            isbeinghit = false;
        }

        public void Load_content_Ship(ContentManager Content)
        {
            Health_texture = Content.Load<Texture2D>("Health_Bar.png");

            Player_Ship_image1 = Content.Load<Texture2D>("Player_Ship_1.png");
            Player_Ship_image2 = Content.Load<Texture2D>("Player_Ship_2.png");
            Player_Ship_image3 = Content.Load<Texture2D>("Player_Ship_3.png");

            Shot1 = Content.Load<SoundEffect>("Tier1_SpaceShip_Shot");
            Shot2 = Content.Load<SoundEffect>("Tier2_SpaceShip_Shot");
            Shot3 = Content.Load<SoundEffect>("Tier3_SpaceShip_Shot");
            Explosion1 = Content.Load<SoundEffect>("Explosion1");

            Projectile_Image = Content.Load<Texture2D>("Projectile.png");

            Explosion = Content.Load<Texture2D>("ExplosionRed");
        }

        //Used only for Upgrades Menu
        public void Update_Tier()
        {

            switch (Ship_Tier)
            {
                case 1:
                    {
                        Ship_Total_HP = 1000 + Ship_Total_HP_PTS * 100;
                        Ship_Endurance = 2 + Ship_Endurance_PTS;
                        Ship_Shooting_Speed = 25 - Ship_Shooting_Speed_PTS;
                        Projectile_Damage = 100 + Projectile_Damage_PTS * 20;
                        Ship_Speed = 8 + Ship_Speed_PTS;
                        Ship_ColisionBox.Width = Width / 11;
                        Ship_ColisionBox.Height = (int)(Height);
                        break;
                    }
                case 2:
                    {
                        Ship_Total_HP = 1500 + Ship_Total_HP_PTS * 100;
                        Ship_Endurance = 3 + Ship_Endurance_PTS;
                        Ship_Shooting_Speed = 19 - Ship_Shooting_Speed_PTS;
                        Projectile_Damage = 120 + Projectile_Damage_PTS * 20;
                        Ship_Speed = 10 + Ship_Speed_PTS;
                        break;
                    }
                case 3:
                    {
                        Ship_Total_HP = 1700 + Ship_Total_HP_PTS * 100;
                        Ship_Endurance = 4 + Ship_Endurance_PTS;
                        Ship_Shooting_Speed = 17 - Ship_Shooting_Speed_PTS;
                        Projectile_Damage = 100 + Projectile_Damage_PTS * 20;
                        Ship_Speed = 12 + Ship_Speed_PTS;
                        break;
                    }

            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        public void Update_Ship(GameTime Gametime)
        {

            #region Keyboard Input
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))

            if (Ship_ColisionBox.X > 15)
            if (Keyboard.GetState().IsKeyDown(Keys.A)) Ship_ColisionBox.X -= Ship_Speed;

            if (Ship_ColisionBox.X + Ship_ColisionBox.Width < Width)
                if (Keyboard.GetState().IsKeyDown(Keys.D)) Ship_ColisionBox.X += Ship_Speed;
            if (Ship_ColisionBox.Y > Width/20)
                if (Keyboard.GetState().IsKeyDown(Keys.W)) Ship_ColisionBox.Y -= Ship_Speed;
            if (Ship_ColisionBox.Y > 0 && Ship_ColisionBox.Y < Height * 0.83)
                if (Keyboard.GetState().IsKeyDown(Keys.S)) Ship_ColisionBox.Y += Ship_Speed;



            if (Ship_Tier == 1)
            {
                Ship_ColisionBox.Width = Width / 9;
                Ship_ColisionBox.Height = (int)((float)Height / (float)15);
            }
            if (Ship_Tier == 2)
                Ship_ColisionBox.Width = Width / 9;
                Ship_ColisionBox.Height = (int)((float)Height / (float)10);
            if (Ship_Tier == 3)
                Ship_ColisionBox.Width = Width / 9;
                Ship_ColisionBox.Height = (int)((float)Height / (float)8);

            #endregion

            HealthBox = new Rectangle((int)(Width*0.01), (int)(Height*0.01), (int)Ship_Total_HP/5, Height / 20);

            if (Ship_ColisionBox.X > 0)
            {
                Ship_ColisionBox.X -= 3;
            }

            #region Projectiles
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && Projectile_Delay <= 0)
            {
                switch(Ship_Tier)
                {
                    case 1:
                        Projectile newprojectile = new Projectile(Projectile_Image);
                        newprojectile.Projectile_ColisionBox = new Rectangle(Ship_ColisionBox.X + Ship_ColisionBox.Width / 2, Ship_ColisionBox.Y + Ship_ColisionBox.Height / 2, Projectile_Image.Width, Projectile_Image.Height);
                        newprojectile.ShootSpeed = Ship_Shooting_Speed;
                        newprojectile.ProjectileDamage = Projectile_Damage;
                        newprojectile.isvisible = true;

                        if (Projectiles_List.Count < 20)
                        {
                            Projectiles_List.Add(newprojectile);
                        }
                        Shot1.Play((float)0.1, -1, 0);


                        Projectile_Delay = Ship_Shooting_Speed;
                        break;

                    case 2:
                        Projectile newprojectile2 = new Projectile(Projectile_Image);
                        newprojectile2.Projectile_ColisionBox = new Rectangle(Ship_ColisionBox.X + Ship_ColisionBox.Width / 2, Ship_ColisionBox.Y + Ship_ColisionBox.Height / 2, Projectile_Image.Width, Projectile_Image.Height);
                        newprojectile2.ShootSpeed = Ship_Shooting_Speed;
                        newprojectile2.ProjectileDamage = Projectile_Damage;
                        newprojectile2.isvisible = true;

                        if (Projectiles_List.Count < 20)
                        {
                            Projectiles_List.Add(newprojectile2);
                        }
                        Shot2.Play((float)0.1, -1,0);
                        Projectile_Delay = Ship_Shooting_Speed;
                        break;
                    case 3:
                        Projectile newprojectile3 = new Projectile(Projectile_Image);
                        Projectile newprojectile4 = new Projectile(Projectile_Image);
                        newprojectile3.Projectile_ColisionBox = new Rectangle(Ship_ColisionBox.X + Ship_ColisionBox.Width / 2, Ship_ColisionBox.Y + (int)(Ship_ColisionBox.Height * 0.2), Projectile_Image.Width, Projectile_Image.Height);
                        newprojectile4.Projectile_ColisionBox = new Rectangle(Ship_ColisionBox.X + Ship_ColisionBox.Width / 2, Ship_ColisionBox.Y + (int)(Ship_ColisionBox.Height * 0.8), Projectile_Image.Width, Projectile_Image.Height);
                        newprojectile3.ShootSpeed = Ship_Shooting_Speed;
                        newprojectile4.ShootSpeed = Ship_Shooting_Speed;
                        newprojectile3.ProjectileDamage = Projectile_Damage;
                        newprojectile4.ProjectileDamage = Projectile_Damage;
                        newprojectile3.isvisible = true;
                        newprojectile4.isvisible = true;

                        if (Projectiles_List.Count < 40)
                        {
                            Projectiles_List.Add(newprojectile3);
                            Projectiles_List.Add(newprojectile4);
                        }
                        Shot3.Play((float)0.05, -1, 1);
                        Projectile_Delay = Ship_Shooting_Speed;
                        break;

                }


            }


            foreach (Projectile P in Projectiles_List)
            {
                P.Projectile_ColisionBox.X += 10;

                if (P.Projectile_ColisionBox.X > Width) P.isvisible = false;

            }
            for (int i = 0; i < Projectiles_List.Count; i++)
            {
                if (!Projectiles_List[i].isvisible)
                {
                    Projectiles_List.RemoveAt(i);
                    i--;
                }
            }
            Projectile_Delay--;

        }
        #endregion

    


        public void Is_ship_Coliding(List<EnemyShip> Enemylist)
        {
            #region EnemyShipColision
            foreach (EnemyShip E in Enemylist)
            {
                if (Ship_Is_Destructed == false)
                {
                    if (Ship_ColisionBox.Intersects(E.ColisionBox) == true)
                    {
                        Boom Exp1 = new Boom(Explosion);
                        Exp1.Explosion_ColisionBox = E.ColisionBox;
                        ExplosionsList.Add(Exp1);
                        Explosion1.Play(0.2f, 0f, 0f);
                        E.isdestroyed = true;
                        isbeinghit = true;
                        score += E.colisiondamage;
                        Ship_Total_HP = Ship_Total_HP - E.colisiondamage * Ship_Endurance / 10;
                    }
                    foreach (Projectile P in Projectiles_List)
                    {
                        if(P.Projectile_ColisionBox.Intersects(E.ColisionBox))
                        {
                            E.Health -= P.ProjectileDamage;


                            if (E.Health < 1)
                            {
                                Boom Exp1 = new Boom(Explosion);
                                Exp1.Explosion_ColisionBox = E.ColisionBox;
                                ExplosionsList.Add(Exp1);
                                Explosion1.Play(0.2f, 0f, 0f);
                                score += E.colisiondamage;
                                P.isvisible = false;
                                E.isdestroyed = true;

                            }

                            else if (E.Health > 1)
                            {
                                E.isbeingdamaged = true;
                                P.isvisible = false;
                            }
                        }

                    }

                }
            }
            #endregion

            if (Ship_Total_HP < 1 && Ship_Is_Destructed == false)
            {
                Ship_Is_Destructed = true;

                Boom Exp1 = new Boom(Explosion);
                Exp1.Explosion_ColisionBox = Ship_ColisionBox;
                ExplosionsList.Add(Exp1);

            }
        }

        public void Draw_Ship(SpriteBatch spritebatch, SpriteFont Font)
        {
            
            foreach (Projectile P in Projectiles_List)
            {
                P.Draw_Projectile(spritebatch);
            }

            for (int i = 0; i < ExplosionsList.Count; i++)
            {
                {
                    ExplosionsList[i].Draw_Explosion(spritebatch);
                    if (ExplosionsList[i].x >= 40)
                    {
                        ExplosionsList.RemoveAt(i);
                        i--;
                    }
                }

            }
            if(!isbeinghit)
            {
            if (Ship_Tier == 1 && Ship_Is_Destructed == false) spritebatch.Draw(Player_Ship_image1, Ship_ColisionBox, Color.White);
            if (Ship_Tier == 2 && Ship_Is_Destructed == false) spritebatch.Draw(Player_Ship_image2, Ship_ColisionBox, Color.White);
            if (Ship_Tier == 3 && Ship_Is_Destructed == false) spritebatch.Draw(Player_Ship_image3, Ship_ColisionBox, Color.White);

            }
            if (isbeinghit)
            {
                if (Ship_Tier == 1 && Ship_Is_Destructed == false) spritebatch.Draw(Player_Ship_image1, Ship_ColisionBox, Color.DarkRed);
                if (Ship_Tier == 2 && Ship_Is_Destructed == false) spritebatch.Draw(Player_Ship_image2, Ship_ColisionBox, Color.IndianRed);
                if (Ship_Tier == 3 && Ship_Is_Destructed == false) spritebatch.Draw(Player_Ship_image3, Ship_ColisionBox, Color.OrangeRed);
                isbeinghit = false;
            }



            spritebatch.Draw(Health_texture, HealthBox, Color.DarkRed);
            spritebatch.DrawString(Font, "Score  " + score.ToString(), new Vector2(Width * (float)0.8, Height * (float)0.01), Color.DimGray);
        }



    }
}