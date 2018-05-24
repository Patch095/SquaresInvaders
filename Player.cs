using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Lezione_33__01Dicembre2017_SquaresInvaders
{
    class Player
    {
        Colors playerColor;
        Vector2 position;
        int width;
        int height;
        Rectangle baseRectangle;
        Rectangle cannonRectangle;
        float speed;
        const float maxSpeed = 180.0f;
        int distToSide;
        Bullet[] bullets;
        float counter;
        float shootDelay;
        float ray;
        int hp;
        int score;

        public bool IsAlive;

        public Player(Vector2 pos, int w, int h)
        {
            this.position = pos;
            this.width = w;
            this.height = h;
            this.ray = width / 2;
            this.hp = 3;
            this.IsAlive = true;
            this.score = 0;

            this.playerColor.R = 0;
            this.playerColor.G = 255;
            this.playerColor.B = 100;

            this.distToSide = 20;
            this.shootDelay = 0.3f;

            baseRectangle = new Rectangle(position.X - width/2, position.Y - height/2, width, height/2,  playerColor);
            int cannonWidth = width/3;
            cannonRectangle = new Rectangle(position.X - cannonWidth / 2, position.Y - height, cannonWidth, height / 2, playerColor);

            bullets = new Bullet[30];
            for(int i = 0; i < bullets.Length; i++)
            {
                Colors playerB;
                playerB.R = 255;
                playerB.G = 255;
                playerB.B = 255;
                bullets[i] = new Bullet(10, 20, playerB);
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }
        public float GetRay()
        {
            return ray;
        }

        public void ChangeColors(int hp)
        {
            if (hp == 2)
            {
                this.playerColor.R = 255;
                this.playerColor.G = 20;
                this.playerColor.B = 20;
                baseRectangle.ColorChange(playerColor);
            }
            else if (hp == 1)
            {
                this.playerColor.R = 255;
                this.playerColor.G = 20;
                this.playerColor.B = 20;
                baseRectangle.ColorChange(playerColor);
                cannonRectangle.ColorChange(playerColor);
            }
        }

        public void Input()
        {
            counter += GfxTools.Window.deltaTime;

            if (EnemyManager.NumAlives > 0) {
                if (GfxTools.Window.GetKey(KeyCode.Right))
                {
                    speed = maxSpeed;
                }
                else if (GfxTools.Window.GetKey(KeyCode.Left))
                {
                    speed = -maxSpeed;
                }
                else
                    speed = 0;

                if (GfxTools.Window.GetKey(KeyCode.Space))
                {
                    if (counter >= shootDelay)
                    {
                        Shoot();
                        counter = 0;
                    }
                }
            }
        }

        private Bullet GetFreeBullet()
        {
            for(int i = 0; i< bullets.Length; i++)
            {
                if (bullets[i].IsAlive == false)
                    return bullets[i];
            }
            return null;
        }
        public void Shoot()
        {
            Bullet b = GetFreeBullet();
            if (b != null)
            {
                b.Shoot(new Vector2(position.X, position.Y - height - 15),-250);
            }
        }

        public bool OnHit()
        {
            hp--;
            if (hp <= 0)
            {
                IsAlive = false;
            }
            ChangeColors(hp);
            return !IsAlive;
        }

        public void Update()
        {
            ChangeColors(hp);

            float deltaX = speed * GfxTools.Window.deltaTime;
            position.X += deltaX;
            float maxX = position.X + width / 2;
            float minX = position.X - width / 2;

            if (maxX > GfxTools.Window.width - distToSide)
            {
                float overflowX = maxX - (GfxTools.Window.width - distToSide);
                position.X -= overflowX;
                deltaX -= overflowX;
            }
            else if (minX < distToSide)
            {
                float overflowX = minX - distToSide;
                position.X -= overflowX;
                deltaX -= overflowX;
            }

            baseRectangle.Translate(deltaX, 0);
            cannonRectangle.Translate(deltaX, 0);

            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].IsAlive)
                {
                    bullets[i].Update();
                    if (EnemyManager.CollideWithBullet(bullets[i]))
                    {
                        bullets[i].IsAlive = false;
                    }
                }
            }

        }

        public void Draw()
        {
            baseRectangle.Draw();
            cannonRectangle.Draw();

            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].IsAlive)
                    bullets[i].Draw();
            }
        }

        public int GetScore()
        {
            return score;
        }
        public void AddScore(int amount)
        {
            score += amount;
        }
    }
}