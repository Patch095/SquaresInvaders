using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Lezione_33__01Dicembre2017_SquaresInvaders
{ 
    class Bullet
    {
        int width;
        int height;
        Colors color;
        Vector2 velocity;

        public Vector2 Position;
        public bool IsAlive;
       
        public Bullet(int w, int h, Colors c)
        {
            this.width = w;
            this.height = h;

            this.color = c;
            this.IsAlive = false;

            this.Position.X = 0;
            this.Position.Y = 0;
            this.velocity = Position;
        }

        public bool Collides(Vector2 centerPosition, float ray)
        {
            Vector2 dist = Position.Sub(centerPosition);
            return (dist.GetLenght() <= width / 2 + ray);
        }

        public void Update()
        {
            Position.X += velocity.X * GfxTools.Window.deltaTime;
            Position.Y += velocity.Y * GfxTools.Window.deltaTime;

            if (Position.Y + height/2 < 0 || Position.Y - height/2 > GfxTools.Window.height - 20)
            {
                IsAlive = false;
            }
        }

        public void SetVelocity(Vector2 newVel)
        {
            velocity = newVel;
        }

        public void Draw()
        {
            GfxTools.DrawFullSquare((int)(Position.X - width / 2),(int)(Position.Y - height / 2), (int)(Position.X + width / 2), (int)(Position.Y + height / 2), color.R, color.G, color.B);
        }

        public void Shoot(Vector2 startPos, float dirY)
        {
            Position = startPos;
            velocity.X = 0;
            velocity.Y = dirY;
            IsAlive = true;
        }

        public int GetWidth()
        {
            return width;
        }
    }
}
