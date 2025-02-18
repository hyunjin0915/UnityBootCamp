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
            int temp = 0;
            if(Input.GetKeyDown(ConsoleKey.W) )
            {
                temp = Y - 1;
                if (Engine.Instance.scene[X][temp]!='*')
                {
                    Y--;
                }
                
            }
            else if (Input.GetKeyDown(ConsoleKey.A) )
            {
                temp = X - 1;
                if (Engine.Instance.scene[X][temp] != '*')
                {
                    X--;
                }
                    
            }
            else if (Input.GetKeyDown(ConsoleKey.S) )
            {
                temp = Y + 1;
                if (Engine.Instance.scene[X][temp] != '*')
                {
                    Y++;
                }
          
            }
            else if (Input.GetKeyDown(ConsoleKey.D) )
            {
                temp = X + 1;
                if (Engine.Instance.scene[X][temp] != '*')
                {
                    X++;
                }
         
            }
        }
    }
}
