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
            OrderLayer = 4;
            isTrigger = true;
        }
        
        public override void Update()
        {
            
            if(Input.GetKeyDown(ConsoleKey.W) )
            {
                if(!PredictionCollection(X, Y-1))
                {
                    Y--;
                }
            }
            else if (Input.GetKeyDown(ConsoleKey.A) )
            {
                if (!PredictionCollection(X-1, Y))
                {
                    X--;
                }
            }
            else if (Input.GetKeyDown(ConsoleKey.S) )
            {
                if (!PredictionCollection(X, Y+ 1))
                {
                    Y++;
                }
            }
            else if (Input.GetKeyDown(ConsoleKey.D) )
            {
                if (!PredictionCollection(X+1, Y))
                {
                    X++;
                }
            }
        }

        public void GameOver()
        {
            
                
            
        }

    }
}
