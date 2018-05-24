using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Lezione_33__01Dicembre2017_SquaresInvaders
{
    class Alien
    {
        int width;
        int height;
        Colors color;
        int b;
        int inc;
        int distToSide;
        Pixel[] sprite;
        byte[] pixelArr;
        int verticalPixel;
        int horizontalPixel;
        float nextShoot;

        public int Score;
        public Vector2 Velocity;
        public Vector2 Position;
        public bool IsVisible;
        public bool IsAlive;
        public bool CanShoot;

        public Alien(Vector2 pos, Vector2 vel, int w, int h, int t)
        {
            this.Position = pos;
            this.Velocity = vel;
            this.width = w;
            this.height = h;
      
            this.color.R = 0;
            this.color.G = 255;
            this.color.B = (byte)b;
            this.inc = 15;

            this.distToSide = 20;
            this.IsAlive = true;
            this.IsVisible = true;

            nextShoot = RandomGenerator.GetRandom(3, 12);

            switch (t)
            {
                case 1:
                    this.pixelArr = new byte[]{ 0, 0, 0, 1, 1, 0, 0, 0,
                                                0, 0, 1, 1, 1, 1, 0, 0,
                                                0, 1, 1, 1, 1, 1, 1, 0,
                                                1, 1, 0, 1, 1, 0, 1, 1,
                                                1, 1, 1, 1, 1, 1, 1, 1,
                                                0, 0, 1, 0, 0, 1, 0, 0,
                                                0, 1, 0, 1, 1, 0, 1, 0,
                                                1, 0, 1, 0, 0, 1, 0, 1
                    };
                    this.verticalPixel = 8;
                    this.horizontalPixel = 8;
                    this.Score = 3;
                    break;

                case 2:
                    this.pixelArr = new byte[]{  0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                                                 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                                                 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0,
                                                 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0,
                                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                                 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1,
                                                 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1,
                                                 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0
                    };
                    this.verticalPixel = 8;
                    this.horizontalPixel = 11;
                    this.Score = 2;
                    break;
                case 3:
                    this.pixelArr = new byte[]{  0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                                                 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                                                 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0,
                                                 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0,
                                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                                 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1,
                                                 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1,
                                                 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0
                    };
                    this.verticalPixel = 8;
                    this.horizontalPixel = 11;
                    this.Score = 2;
                    break;

                default:
                    this.pixelArr = new byte[]{ 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
                                                0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
                                                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                                1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1,
                                                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                                0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0,
                                                0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0,
                                                0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0
                    };
                    this.verticalPixel = 8;
                    this.horizontalPixel = 12;
                    this.Score = 1;
                    break;
            }
            
            int numPixels = 0;
            for (int i = 0; i < pixelArr.Length; i++)
            {
                if (pixelArr[i] == 1)
                    numPixels++;
            }
            sprite = new Pixel[numPixels];

            int pixelSize = height / verticalPixel;
            width = horizontalPixel * pixelSize;
            float startPosX = pos.X - (float)width / 2;
            float posY = pos.Y - height / 2;
            int sp = 0;
            for (int i = 0; i < pixelArr.Length; i++)
            {
                if (i != 0 && i % horizontalPixel == 0)
                    posY += pixelSize;

                if (pixelArr[i] != 0)
                {
                    float pixelX = startPosX + (i % horizontalPixel) * (pixelSize);
                    sprite[sp] = new Pixel(new Vector2(pixelX, posY), pixelSize, color);
                    sp++;
                }
            }
        }

        public bool OnHit()
        {
            IsAlive = false;
            for(int i = 0; i < sprite.Length; i++)
            {
                Vector2 startVel = sprite[i].Position.Sub(Position);
                startVel.X *= RandomGenerator.GetRandom(3, 16);
                startVel.Y *= RandomGenerator.GetRandom(5, 20);
                sprite[i].Velocity = startVel;
                sprite[i].IsAfectdedByGravity = true;
            }
            SpeedUp(20);
            return true;
        }
        public void AlienColors()
        {
            this.b += inc;
            if (b > 255)
            {
                b = 255;
                inc = -inc;
            }
            else if (b < 0)
            {
                b = 0;
                inc = -inc;
            }
            if (CanShoot)
            {
                this.color.R = (byte)b;
                this.color.G = 120;
                this.color.B = 40;
                for (int i = 0; i < sprite.Length; i++)
                {
                    sprite[i].ColorChange(color);
                }
            }
            else
            {
                this.color.R = 80;
                this.color.G = 255;
                this.color.B = (byte)b;
                for (int i = 0; i < sprite.Length; i++)
                {
                    sprite[i].ColorChange(color);
                }
            }
        }
        public void SpeedUp(int value)
        {
            if(Velocity.X > 0)
                Velocity.X += value;
            else
                Velocity.X -= value;
        }

        public bool Update(ref float overflowX)
        {
            AlienColors();
            bool endReached = false;
            if (!IsAlive)
            {
                for (int i = 0; i < sprite.Length; i++)
                {
                    sprite[i].Update();
                }
            }
            else if(IsAlive && IsVisible)
            {
                float deltaX = Velocity.X * GfxTools.Window.deltaTime;
                float deltaY = Velocity.Y * GfxTools.Window.deltaTime;
                Position.X += deltaX;
                Position.Y += deltaY;
                float maxX = Position.X + width / 2;
                float minX = Position.X - width / 2;

                if (maxX > GfxTools.Window.width - distToSide)
                {
                    overflowX = maxX - (GfxTools.Window.width - distToSide);
                    endReached = true;
                }
                else if (minX < distToSide)
                {
                    overflowX = minX - distToSide;
                    endReached = true;
                }

                TranslateSprite(new Vector2(deltaX, deltaY));

                if (Position.Y + height / 2 >= Game.GetPlayer().GetPosition().Y)
                {
                    EnemyManager.Landed = true;
                }
                else if (CanShoot)
                {
                    nextShoot -= GfxTools.Window.deltaTime;
                    if (nextShoot <= 0)
                    {
                        EnemyManager.Shoot(this);
                        nextShoot = RandomGenerator.GetRandom(3, 12);
                    }
                }
            }
            else if (IsVisible)
            {
                for (int i = 0; i < sprite.Length; i++)
                {
                    if(sprite[i].IsVisible)
                        sprite[i].Update();
                }
            }
            return endReached;
        }

        public void Draw()
        {
            for (int i = 0; i < sprite.Length; i++)
            {
                if(sprite[i].IsVisible)
                    sprite[i].Draw();
            }
        }

        public int GetWidth()
        {
            return width;
        }
        public int GetHeight()
        {
            return height;
        }

        private void TranslateSprite(Vector2 transVect)
        {
            for (int i = 0; i < sprite.Length; i++)
            {
                sprite[i].Translate(transVect);
            }
        }
        public void Translate(Vector2 transl)
        {
            Position.X += transl.X;
            Position.Y += transl.Y;

            TranslateSprite(transl);
        }
    }
}
