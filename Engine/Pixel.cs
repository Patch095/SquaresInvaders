using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Lezione_33__01Dicembre2017_SquaresInvaders
{
    class Pixel
    {
        Vector2 position;
        Vector2 velocity;
        int width;
        Colors color;

        public Vector2 Position { get { return position; } }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public bool IsAfectdedByGravity { get; set; }
        public bool IsVisible;


        public Pixel(Vector2 pos, int w, Colors c)
        {
            this.position = pos;
            this.width = w;
            this.color = c;
            this.IsVisible = true;
        }

        public void Update()
        {
            if (IsAfectdedByGravity)
                velocity.Y += Game.Gravity * GfxTools.Window.deltaTime;
            position.X += velocity.X * GfxTools.Window.deltaTime;
            position.Y += velocity.Y * GfxTools.Window.deltaTime;

            if (position.Y >= GfxTools.Window.height || position.X + width < 0 || position.X - width >= GfxTools.Window.width)
                IsVisible = false;
        }

        public void Draw()
        {
            if(IsVisible)
                GfxTools.DrawFullSquare((int)position.X, (int)position.Y, (int)position.X + width, (int)position.Y + width, color.R, color.G, color.B);
        }
        public void Translate(Vector2 transl)
        {
            position.X += transl.X;
            position.Y += transl.Y;
        }

        public void ColorChange(Colors newColors)
        {
            this.color = newColors;
        }
    }
}
