using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250211
{
    public class Player : GameObject
    {
        public Player(int inX, int inY, char inShape)
        {
            X = inX;
            Y = inY;
            Shape = inShape;
        }

        public override void Update()
        {
            if(Input.GetKeyDown(ConsoleKey.W))
            {
                Y--;
            }
            else if (Input.GetKeyDown(ConsoleKey.A))
            {
                X--;
            }
            else if(Input.GetKeyDown(ConsoleKey.S))
            {
                Y++;
            }
            else if(Input.GetKeyDown(ConsoleKey.D))
            {
                
;               X++;
            }
        }
    }
}
