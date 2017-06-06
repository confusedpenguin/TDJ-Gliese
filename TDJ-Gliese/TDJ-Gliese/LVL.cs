using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using TDJ_Gliese;

namespace TDJ_Gliese
{
    class LVL
    {
        Texture2D Enemy1;
        Texture2D Enemy2;
        Texture2D Enemy3;
        Texture2D Enemy4;
        Texture2D Enemy5;
        Texture2D Enemy6;

        Texture2D Boss1;
        Texture2D Boss2;
        Texture2D Boss3;
        Texture2D MegaBoss;
        Texture2D Projectileimg;
        Texture2D Powerupimg;

        public List<EnemyShip> Wave1;
        public List<EnemyShip> Wave3;
        public List<EnemyShip> Wave4;

        public List<Projectile> EnemyProjectilesList;


        public EnemyShip B1;
        public EnemyShip B2;
        public EnemyShip B3;
        public EnemyShip MB;

        Random EnemySpawn;

        int Screenwidth;
        int ScreenHeight;

        public int wave1Time;
        public int wave2Time;
        public int wave3Time;

        public PowerUp LVLpowerup;
        public int[] Spawntimes;



        

        public LVL(int Width, int Height)
        {
            

            EnemyProjectilesList = new List<Projectile>();

            Screenwidth = Width;
            ScreenHeight = Height;

            EnemySpawn = new Random();

            Wave1 = new List<EnemyShip>();
            Wave3 = new List<EnemyShip>();
            Wave4 = new List<EnemyShip>();

            Spawntimes = new int[7] { 0, 15, 30, 26, 21, 5, 0 };

            wave1Time = 0;
        }

        public void LoadContent_LVL(ContentManager Content)
        {
            Enemy1 = Content.Load<Texture2D>("Enemy1");
            Enemy2 = Content.Load<Texture2D>("Enemy2");
            Enemy3 = Content.Load<Texture2D>("Enemy3");
            Enemy4 = Content.Load<Texture2D>("Enemy4");
            Enemy5 = Content.Load<Texture2D>("Enemy5");
            Enemy6 = Content.Load<Texture2D>("Enemy6");
            Projectileimg = Content.Load<Texture2D>("Projectile");

            Boss1 = Content.Load<Texture2D>("Boss1");
            Boss2 = Content.Load<Texture2D>("Boss2");

            B1 = new EnemyShip(Boss1, Screenwidth, ScreenHeight);



            Boss3 = Content.Load<Texture2D>("Boss3");
            B2 = new EnemyShip(Boss3, Screenwidth, ScreenHeight);
            B3 = new EnemyShip(Boss1, Screenwidth, ScreenHeight);
  

            MegaBoss = Content.Load<Texture2D>("Megaboss");

            Powerupimg = Content.Load<Texture2D>("PowerUp");
            LVLpowerup = new PowerUp(Powerupimg, Screenwidth, ScreenHeight);
            MB = new EnemyShip(MegaBoss, Screenwidth, ScreenHeight);
        }

        public void UpdateContent_LVL(GameTime GameTime)
        {
            #region enemy projectile update

            for (int i = 0; i < EnemyProjectilesList.Count; i++)
            {
                if (EnemyProjectilesList[i].isvisible == false)
                {
                    EnemyProjectilesList.RemoveAt(i);
                    i--;
                }
            }

            foreach (Projectile P in EnemyProjectilesList)
            {
                P.Projectile_ColisionBox.X -= 10;
                if (P.Projectile_ColisionBox.X < -100) P.isvisible = false;

            }

            #endregion

            #region ShipSpawn wave 1
            if (B1.isdestroyed == false && Wave1.Count < 10 && Spawntimes[0] == 60)
            {
                EnemyShip newEnemyShip = new EnemyShip(Enemy1, Screenwidth, ScreenHeight);
                newEnemyShip.ColisionBox = new Rectangle(Screenwidth, (EnemySpawn.Next(ScreenHeight / 20, ScreenHeight - ScreenHeight / 5)), Screenwidth / 12, ScreenHeight / 7);
                newEnemyShip.isdestroyed = false;
                newEnemyShip.colisiondamage = 1000;
                newEnemyShip.Health = 100;
                newEnemyShip.ShootTimer = 40;
                Wave1.Add(newEnemyShip);
                Spawntimes[0] = 0;
            }

            if (B1.isdestroyed == false && wave1Time > 3500 && Wave1.Count < 10 && Spawntimes[1] >= 100)
            {
                EnemyShip newEnemyShip = new EnemyShip(Enemy2, Screenwidth, ScreenHeight);
                newEnemyShip.ColisionBox = new Rectangle(Screenwidth, (EnemySpawn.Next(ScreenHeight / 20, ScreenHeight - ScreenHeight / 5)), Screenwidth / 12, ScreenHeight / 7);
                newEnemyShip.isdestroyed = false;
                newEnemyShip.colisiondamage = 2000;
                newEnemyShip.Health = 130;
                newEnemyShip.ShootTimer = 36;
                Wave1.Add(newEnemyShip);
                Spawntimes[1] = 0;
            }

            if (B1.isdestroyed == false && wave1Time == 7000)
            {
                B1 = new EnemyShip(Boss1, Screenwidth, ScreenHeight);
                B1.ColisionBox = new Rectangle(Screenwidth, ScreenHeight / 3, Screenwidth / 5, ScreenHeight / 3);
                B1.isdestroyed = false;
                B1.isboss = true;
                B1.ShootTimer = 24;
                B1.colisiondamage = 50000;
                B1.Health = 10000;
                Wave1.Add(B1);
            }
            #endregion

            #region ShipSpawn wave 2
            if (B1.isdestroyed == true && B2.isdestroyed == false && Wave3.Count < 10 && Spawntimes[2] >= 60)
            {
                EnemyShip newEnemyShip = new EnemyShip(Enemy3, Screenwidth, ScreenHeight);
                newEnemyShip.ColisionBox = new Rectangle(Screenwidth, (EnemySpawn.Next(ScreenHeight / 20, ScreenHeight - ScreenHeight / 5)), Screenwidth / 12, ScreenHeight / 7);
                newEnemyShip.isdestroyed = false;
                newEnemyShip.colisiondamage = 2000;
                newEnemyShip.Health = 150;
                Wave3.Add(newEnemyShip);
                newEnemyShip.ShootTimer = 36;
                Spawntimes[2] = 0;
            }

            if (B1.isdestroyed == true && B2.isdestroyed == false && wave2Time > 3500 && Wave3.Count < 10 && Spawntimes[3] >= 100)
            {
                EnemyShip newEnemyShip = new EnemyShip(Enemy4, Screenwidth, ScreenHeight);
                newEnemyShip.ColisionBox = new Rectangle(Screenwidth, (EnemySpawn.Next(ScreenHeight / 20, ScreenHeight - ScreenHeight / 5)), Screenwidth / 12, ScreenHeight / 7);
                newEnemyShip.isdestroyed = false;
                newEnemyShip.colisiondamage = 4000;
                newEnemyShip.Health = 200;
                newEnemyShip.ShootTimer = 32;
                Wave3.Add(newEnemyShip);
                Spawntimes[3] = 0;
            }

            if (B1.isdestroyed == true && B2.isdestroyed == false && wave2Time == 7000)
            {
                B2 = new EnemyShip(Boss2, Screenwidth, ScreenHeight);
                B2.ColisionBox = new Rectangle(Screenwidth, ScreenHeight / 3, Screenwidth / 5, ScreenHeight / 3);
                B2.isdestroyed = false;
                B2.isboss = true;
                B2.ShootTimer = 28;
                B2.colisiondamage = 80000;
                B2.Health = 15000;
                Wave3.Add(B2);
            }
            #endregion

            #region ShipSpawn wave 3
            if (B2.isdestroyed == true && MB.isdestroyed == false && Wave4.Count < 10 && Spawntimes[4] >= 60)
            {
                EnemyShip newEnemyShip = new EnemyShip(Enemy5, Screenwidth, ScreenHeight);
                newEnemyShip.ColisionBox = new Rectangle(Screenwidth, (EnemySpawn.Next(ScreenHeight / 20, ScreenHeight - ScreenHeight / 5)), Screenwidth / 12, ScreenHeight / 7);
                newEnemyShip.isdestroyed = false;
                newEnemyShip.colisiondamage = 4000;
                newEnemyShip.Health = 600;
                newEnemyShip.ShootTimer = 32;
                Wave4.Add(newEnemyShip);
                Spawntimes[4] = 0;
            }

            if (B2.isdestroyed == true && MB.isdestroyed == false && wave3Time > 3500 && Wave4.Count < 10 && Spawntimes[5] >= 100)
            {
                EnemyShip newEnemyShip = new EnemyShip(Enemy6, Screenwidth, ScreenHeight);
                newEnemyShip.ColisionBox = new Rectangle(Screenwidth, (EnemySpawn.Next(ScreenHeight / 20, ScreenHeight - ScreenHeight / 5)), Screenwidth / 12, ScreenHeight / 7);
                newEnemyShip.isdestroyed = false;
                newEnemyShip.colisiondamage = 6000;
                newEnemyShip.Health = 800;
                newEnemyShip.ShootTimer = 30;
                Wave4.Add(newEnemyShip);
                Spawntimes[5] = 0;
            }

            if (B2.isdestroyed == true && B3.isdestroyed == false && wave3Time == 7000)
            {
                B3 = new EnemyShip(Boss3, Screenwidth, ScreenHeight);
                B3.ColisionBox = new Rectangle(Screenwidth, ScreenHeight / 3, Screenwidth / 5, ScreenHeight / 3);
                B3.isdestroyed = false;
                B3.isboss = true;
                B3.colisiondamage = 100000;
                B3.ShootTimer = 22;
                B3.Health = 25000;
                Wave4.Add(B3);
            }
            if (B3.isdestroyed && wave1Time > 14000)
            {
                MB = new EnemyShip(MegaBoss, Screenwidth, ScreenHeight);
                MB.ColisionBox = new Rectangle(Screenwidth, ScreenHeight / 3, Screenwidth / 5, ScreenHeight / 3);
                MB.isdestroyed = false;
                MB.isboss = true;
                MB.colisiondamage = 200000;
                MB.Health = 100000;
                MB.ShootTimer = 10;
                wave1Time = 0;
                Wave4.Add(MB);


            }

            #endregion

            #region powerup
            if (wave1Time > 200)
            LVLpowerup.Update(GameTime);

            if (Spawntimes[6] == 400)
            {
                LVLpowerup.ColisionBox.X = Screenwidth +200;
                Spawntimes[6] = 0;
                LVLpowerup.Isdestroyed = false;
                
            }
            #endregion

            #region EnemyProjectilespanw
            foreach (EnemyShip E in Wave1)
            {
                if (E.ShootTimer == 0)
                {
                    if (E.isboss == false)
                    {
                        Projectile EnemyProjectile = new Projectile(Projectileimg);
                        EnemyProjectile.ProjectileDamage = 12;
                        EnemyProjectile.Projectile_ColisionBox.X = E.ColisionBox.X + (E.ColisionBox.Width / 2);
                        EnemyProjectile.Projectile_ColisionBox.Y = E.ColisionBox.Y + (E.ColisionBox.Height / 2);
                        EnemyProjectile.ShootSpeed = 18;
                        EnemyProjectilesList.Add(EnemyProjectile);
                    }
                    else if (E.isboss == true)
                    {
                        Projectile EnemyProjectile = new Projectile(Projectileimg);
                        Projectile EnemyProjectile2 = new Projectile(Projectileimg);


                        EnemyProjectile.ProjectileDamage = 30;
                        EnemyProjectile2.ProjectileDamage = 30;

                        EnemyProjectile.Projectile_ColisionBox.X = E.ColisionBox.X + (int)(E.ColisionBox.X * (float)0.5);
                        EnemyProjectile.Projectile_ColisionBox.Y = E.ColisionBox.Y + (int)(E.ColisionBox.Y * (float)0.2);
                        EnemyProjectile2.Projectile_ColisionBox.X = E.ColisionBox.X + (int)(E.ColisionBox.X * (float)0.5);
                        EnemyProjectile2.Projectile_ColisionBox.Y = E.ColisionBox.Y + (int)(E.ColisionBox.Y * (float)0.8);

                        EnemyProjectile.ShootSpeed = 15;
                        EnemyProjectile2.ShootSpeed = 15;

                        EnemyProjectilesList.Add(EnemyProjectile);
                        EnemyProjectilesList.Add(EnemyProjectile2);

                    }
                }
            }
            foreach (EnemyShip E in Wave3)
            {
                if (E.ShootTimer == 0)
                {
                    if (E.isboss == false)
                    {
                        Projectile EnemyProjectile = new Projectile(Projectileimg);
                        EnemyProjectile.ProjectileDamage = 22;
                        EnemyProjectile.Projectile_ColisionBox.X = E.ColisionBox.X + (E.ColisionBox.Width / 2);
                        EnemyProjectile.Projectile_ColisionBox.Y = E.ColisionBox.Y + (E.ColisionBox.Height / 2);
                        EnemyProjectile.ShootSpeed = 15;
                        EnemyProjectilesList.Add(EnemyProjectile);
                    }
                    if (E.isboss == true)
                    {
                        Projectile EnemyProjectile = new Projectile(Projectileimg);
                        Projectile EnemyProjectile2 = new Projectile(Projectileimg);


                        EnemyProjectile.ProjectileDamage = 40;
                        EnemyProjectile2.ProjectileDamage = 40;

                        EnemyProjectile.Projectile_ColisionBox.X = E.ColisionBox.X + (int)(E.ColisionBox.X * (float)0.5);
                        EnemyProjectile.Projectile_ColisionBox.Y = E.ColisionBox.Y + (int)(E.ColisionBox.Y * (float)0.2);
                        EnemyProjectile2.Projectile_ColisionBox.X = E.ColisionBox.X + (int)(E.ColisionBox.X * (float)0.5);
                        EnemyProjectile2.Projectile_ColisionBox.Y = E.ColisionBox.Y + (int)(E.ColisionBox.Y * (float)0.8);

                        EnemyProjectile.ShootSpeed = 13;
                        EnemyProjectile2.ShootSpeed = 13;

                        EnemyProjectilesList.Add(EnemyProjectile);
                        EnemyProjectilesList.Add(EnemyProjectile2);

                    }
                }
            }
            foreach (EnemyShip E in Wave4)
            {
                if (E.ShootTimer == 0)
                {
                    if (E.isboss == false)
                    {
                        Projectile EnemyProjectile = new Projectile(Projectileimg);
                        EnemyProjectile.ProjectileDamage = 25;
                        EnemyProjectile.Projectile_ColisionBox.X = E.ColisionBox.X + (E.ColisionBox.Width / 2);
                        EnemyProjectile.Projectile_ColisionBox.Y = E.ColisionBox.Y + (E.ColisionBox.Height / 2);
                        EnemyProjectile.ShootSpeed = 12;
                        EnemyProjectilesList.Add(EnemyProjectile);
                    }
                    if (E.isboss == true)
                    {
                        Projectile EnemyProjectile = new Projectile(Projectileimg);
                        Projectile EnemyProjectile2 = new Projectile(Projectileimg);


                        EnemyProjectile.ProjectileDamage = 50;
                        EnemyProjectile2.ProjectileDamage = 55;

                        EnemyProjectile.Projectile_ColisionBox.X = E.ColisionBox.X + (int)(E.ColisionBox.X * (float)0.5);
                        EnemyProjectile.Projectile_ColisionBox.Y = E.ColisionBox.Y + (int)(E.ColisionBox.Y * (float)0.2);
                        EnemyProjectile2.Projectile_ColisionBox.X = E.ColisionBox.X + (int)(E.ColisionBox.X * (float)0.5);
                        EnemyProjectile2.Projectile_ColisionBox.Y = E.ColisionBox.Y + (int)(E.ColisionBox.Y * (float)0.8);

                        EnemyProjectile.ShootSpeed = 12;
                        EnemyProjectile2.ShootSpeed = 12;

                        EnemyProjectilesList.Add(EnemyProjectile);
                        EnemyProjectilesList.Add(EnemyProjectile2);

                    }
                }
            }

            #endregion

            #region Ship Position Update
            for (int i = 0; i < Wave3.Count; i++)
            {
                if (Wave3[i].isdestroyed)
                {
                    Wave3.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < Wave4.Count; i++)
            {
                if (Wave4[i].isdestroyed)
                {
                    Wave4.RemoveAt(i);
                    i--;
                }
            }

            foreach (EnemyShip E in Wave1)
            {
                if (!E.isdestroyed)
                    E.Update(GameTime);

            }
            foreach (EnemyShip E in Wave3)
            {
                if (!E.isdestroyed)
                    E.Update(GameTime);

            }

            foreach (EnemyShip E in Wave4)
            {
                if (!E.isdestroyed)
                    E.Update(GameTime);

            }

            for (int i = 0; i < Wave1.Count; i++)
            {
                if (Wave1[i].isdestroyed)
                {
                    Wave1.RemoveAt(i);
                    i--;
                }
            }
         

            #endregion



            #region timekeepers
            wave1Time++;
            if (B1.isdestroyed == true) wave2Time++;
            if (B2.isdestroyed == true) wave3Time++;


            for (int i = 0; i < 7; i++)
            {
                Spawntimes[i]++;
            }
            #endregion
        
        }


        public void DrawContent_LVL(SpriteBatch SpriteBatch, SpriteFont Font)
        {
            foreach (Projectile P in EnemyProjectilesList)
            {
                P.Draw_Projectile(SpriteBatch);
            }

            foreach (EnemyShip E in Wave1)
            {
                if (!E.isdestroyed)
                {
                    E.Draw(SpriteBatch);
                    E.isbeingdamaged = false;
                }

            }
            foreach (EnemyShip E in Wave3)
            {
                if (!E.isdestroyed)
                {
                    E.Draw(SpriteBatch);
                    E.isbeingdamaged = false;
                }

            }

            foreach (EnemyShip E in Wave4)
            {
                if (!E.isdestroyed)
                {
                    E.Draw(SpriteBatch);
                    E.isbeingdamaged = false;
                }

            }

            LVLpowerup.Draw(SpriteBatch);



        }
    }
}

