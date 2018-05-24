using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Lezione_33__01Dicembre2017_SquaresInvaders
{
    static class GfxTools
    {
        public static Window Window;
        static public void Init(Window w)
        {
            Window = w;
        }

        public static void ClearScreen()
        {
            for (int i = 0; i < Window.bitmap.Length; i++)
            {
                Window.bitmap[i] = 0;
            }
        }
        public static void PutPixel(int x, int y, byte r, byte g, byte b)
        {
            if (x < 0 || x >= Window.width || y < 0 || y >= Window.height)
                return;
            int position = (Window.width * y + x) * 3;
            Window.bitmap[position] = r;
            Window.bitmap[position + 1] = g;
            Window.bitmap[position + 2] = b;
        }
        public static void DrawFullSquare(int startX, int startY, int finishX, int finishY, byte r, byte g, byte b)
        {
            for (int i = startX; i <= finishX; i++)
            {
                for (int j = startY; j <= finishY; j++)
                {
                    PutPixel(i, j, r, g, b);
                }
            }
        }
    }
}
