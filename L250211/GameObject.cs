using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250211
{
    public class GameObject
    {
        public int X, Y;
        public char Shape; //Mesh, Sprite

        public virtual void Update()
        {

        }
        public virtual void Render()
        {
            //x, y 위치의 shape 출력
            Console.SetCursorPosition(X, Y);
            Console.Write(Shape);
        }

    }
}
