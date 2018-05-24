using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Lezione_33__01Dicembre2017_SquaresInvaders
{
    static class Game
    {
        static Window window;
        static Player player;
        static float totalTime;
        static public float Gravity { get; }

        static Game()
        {
            window = new Window(1200, 700, "Square's Invaders", PixelFormat.RGB);
            GfxTools.Init(window);

            Gravity = 555;

            Vector2 playerPosition;
            playerPosition.X = window.width / 2;
            playerPosition.Y = window.height - 20;
            EnemyManager.Init(66, 6);
            player = new Player(playerPosition, 100, 60);
        }

        public static Player GetPlayer()
        {
            return player;
        }

        public static void Play()
        {
            int timer = 120;
            while (window.opened)
            {
                totalTime += GfxTools.Window.deltaTime;
                GfxTools.ClearScreen();
                //Input
                if (window.GetKey(KeyCode.Esc))
                    return;
                if (!player.IsAlive || EnemyManager.Landed)
                    return;

                player.Input();
                //Update
                EnemyManager.Update();
                if (EnemyManager.GetAlives() <= 0)
                {
                    timer--;
                    if (timer <= 0)
                        return;
                }
                player.Update();
                //Draw
                EnemyManager.Draw();
                player.Draw();

                window.Blit();
            }
        }

        public static int GetScore()
        {
            return player.GetScore() - (int)(totalTime * 0.25);
        }
    }
}
