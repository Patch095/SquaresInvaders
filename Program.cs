using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_33__01Dicembre2017_SquaresInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Play();

            Console.WriteLine("Score: " + Game.GetScore());
            Console.ReadLine();
        }
    }
}
