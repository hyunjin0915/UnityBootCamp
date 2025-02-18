using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class Monster : GameObject
    {
        private Random random = new Random();
        public Monster(int _X, int _Y, char _Shape)
        {
            X = _X;
            Y = _Y;
            Shape = _Shape;
        }

        public override void Update()
        {
            int direction = random.Next() % 4;
            if (direction == 0 && Y > 1)
            {
                Y--;
            }
            else if (direction == 1 && Y <8)
            {
                Y++;
            }
            else if (direction == 2 && X < 8)
            {
                X++;
            }
            else if (direction == 3 && X > 1)
            {
                X--;
            }
        }
        
    }
}
