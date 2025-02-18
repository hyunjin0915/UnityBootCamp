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

        public override bool OnCollisionEnter(GameObject colliison)
        {
            if (X == colliison.X && Y == colliison.Y)
            {
                return true;
            }
            else return false;
        }
        public override bool OnCollisionEnter(char colliison)
        {
            if (colliison == ' ')
            {
                return false;
            }
            else if (colliison == '*')
            {
                return true;
            }
            
            else return false;
        }
        
        int temp = 0;
        public override void Update()
        {
            
            if(Input.GetKeyDown(ConsoleKey.W) )
            {
                temp = Y - 1;
                if(!OnCollisionEnter(Engine.Instance.scene[X][temp]))
                {
                    Y--;
                    
                }
            }
            else if (Input.GetKeyDown(ConsoleKey.A) )
            {
                temp = X - 1;
                if (!OnCollisionEnter(Engine.Instance.scene[temp][Y]))
                {
                    X--;
                    
                }
            }
            else if (Input.GetKeyDown(ConsoleKey.S) )
            {
                temp = Y + 1;
                if (!OnCollisionEnter(Engine.Instance.scene[X][temp]))
                {
                    Y++;
                    
                }
            }
            else if (Input.GetKeyDown(ConsoleKey.D) )
            {
                temp = X + 1;
                if (!OnCollisionEnter(Engine.Instance.scene[temp][Y]))
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
