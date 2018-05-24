using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Lezione_33__01Dicembre2017_SquaresInvaders
{
    class Rectangle
    {
        Colors rectColor;
        private Vector2 position;
        int width;
        int height;

        public Rectangle(float x, float y, int w, int h, Colors col)
        {
            this.position.X = x;
            this.position.Y = y;
            this.width = w;
            this.height = h;
            this.rectColor = col;
        }

        public void ColorChange(Colors newColors)
        {
            this.rectColor = newColors;
        }

        public void Translate(float x, float y)
        {
            this.position.X += x;
            this.position.Y += y;
        }
        public void Draw()
        {
            GfxTools.DrawFullSquare((int)position.X, (int)position.Y, (int)(position.X + width), (int)(position.Y + height), rectColor.R, rectColor.G, rectColor.B);
        }
    }
}
