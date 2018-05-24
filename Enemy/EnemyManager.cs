using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_33__01Dicembre2017_SquaresInvaders
{
    static class EnemyManager
    {
        static Alien[] aliens;
        static int numAliens;
        static int numRows;
        static int aliensPerRow;
        static int alienWidth;
        static int alienHeight;
        static Bullet[] bullets;

        public static int NumAlives;
        public static bool Landed;

        public static void Init(int numOfAlien, int numOfRow)
        {
            numAliens = numOfAlien;
            numRows = numOfRow;
            aliensPerRow = numAliens / numRows;
            NumAlives = numOfAlien;

            aliens = new Alien[numAliens];

            int startX = 5;
            alienWidth = 50;
            alienHeight = 35;
            int posY = 5;
            int dist = 3;
            int counter = 1;

            for (int i = 0; i < aliens.Length; i++)
            {
                if (i != 0 && i % aliensPerRow == 0)
                {
                    posY += alienHeight + dist;
                    counter++;
                }
                int alienX = startX + ((i % aliensPerRow) * (alienWidth + dist));

                aliens[i] = new Alien(new Vector2(alienX, posY), new Vector2(200, 0), alienWidth, alienHeight, counter);
                if (i >= numAliens - aliensPerRow)
                    aliens[i].CanShoot = true;
            }


            bullets = new Bullet[aliensPerRow];
            for(int i = 0; i < bullets.Length; i++)
            {
                Colors alienB;
                alienB.R = 255;
                alienB.G = 100;
                alienB.B = 0;
                bullets[i] = new Bullet(10, 20, alienB);
            }
        }

        private static Bullet GetFreeBullet()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].IsAlive == false)
                    return bullets[i];
            }
            return null;
        }
        public static void Shoot(Alien shooter)
        {
            Bullet b = GetFreeBullet();
            if(b != null)
            {
                b.Shoot(new Vector2(shooter.Position.X, shooter.Position.Y + shooter.GetHeight() / 2 + 15), 250);
            }
        }

        static public void Update()
        {
            bool endReached = false;
            float overflowX = 0;
            float tmpOverflowX = 0;
            for (int i = 0; i < aliens.Length; i++)
            {
                if (aliens[i].IsVisible)
                {
                    if (aliens[i].Update(ref tmpOverflowX))
                    {
                        endReached = true;
                        overflowX = tmpOverflowX;
                    }
                }
            }

            if (endReached)
            {
                for (int i = 0; i < aliens.Length; i++)
                {
                    aliens[i].Translate(new Vector2(-overflowX, 20));
                    aliens[i].SpeedUp(30);
                    aliens[i].Velocity.X = -aliens[i].Velocity.X;
                }
            }

            Player player = Game.GetPlayer();
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].IsAlive)
                {
                    bullets[i].Update();
                    if (bullets[i].Collides(player.GetPosition(), player.GetRay()))
                    {
                        bullets[i].IsAlive = false;
                        player.OnHit();
                    }
                }
            }
        }

        static public void Draw()
        {
            for (int i = 0; i < aliens.Length; i++)
            {
                if (aliens[i].IsVisible)
                    aliens[i].Draw();
            }

            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].IsAlive)
                    bullets[i].Draw();
            }
        }

        public static bool CollideWithBullet(Bullet bullet)
        {
            for (int i = 0; i < aliens.Length; i++)
            {
                if (aliens[i].IsAlive)
                {
                    if (bullet.Collides(aliens[i].Position, aliens[i].GetWidth() / 2))
                    {
                        if (aliens[i].OnHit())
                        {
                            Game.GetPlayer().AddScore(5 * aliens[i].Score);
                            if (aliens[i].CanShoot)
                            {
                                int prevAlienIndex = i - aliensPerRow;
                                while (prevAlienIndex >= 0)
                                {
                                    if (aliens[prevAlienIndex].IsAlive)
                                    {
                                        aliens[prevAlienIndex].CanShoot = true;
                                        break;
                                    }

                                    prevAlienIndex -= aliensPerRow;
                                }
                            }
                            NumAlives--;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public static void OnAlienDies()
        {
            EnemyManager.NumAlives--;
        }
        public static int GetAlives()
        {
            return NumAlives;
        }
    }
}
