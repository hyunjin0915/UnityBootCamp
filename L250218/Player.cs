using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class Player : GameObject
    {
        public Player(int _X, int _Y, char _Shape)
        {
            X = _X;
            Y = _Y;
            Shape = _Shape;
        }

        public override void Update()
        {
            if(Input.GetKeyDown(ConsoleKey.W) && Y > 1)
            {
                Y--;
            }
            else if (Input.GetKeyDown(ConsoleKey.A) && X > 1)
            {
                X--;
            }
            else if (Input.GetKeyDown(ConsoleKey.S) && Y <8)
            {
                Y++;
            }
            else if (Input.GetKeyDown(ConsoleKey.D) && X <8)
            {

                ; X++;
            }
        }
    }
}
