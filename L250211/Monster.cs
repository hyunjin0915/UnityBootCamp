using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250211
{
    public class Monster : GameObject
    {
        private Random random = new Random();
        public Monster(int inX, int inY, char inShape)
        {
            X = inX;
            Y = inY;
            Shape = inShape;
        }
        ~Monster() { }



        public override void Update()
        {
            int direction = random.Next() % 4;
            if (direction == 0)
            {
                Y--;
            }
            else if (direction == 1)
            {
                Y++;
            }
            else if (direction == 2)
            {
                X--;
            }
            else
            {
                X++;
            }
            /*int randomX = new Random().Next(-1, 2);
            int randomY = new Random().Next(-1, 2);
            X += randomX;
            Y += randomY;*/
        }

    }
}
