using SDL2;
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

            color.r = 0;
            color.g = 0;
            color.b = 255;
        }
        
        public override void Update()
        {
            
            if(Input.GetKeyDown(SDL.SDL_Keycode.SDLK_w) )
            {
                if(!PredictionCollection(X, Y-1))
                {
                    Y--;
                }
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_a) )
            {
                if (!PredictionCollection(X-1, Y))
                {
                    X--;
                }
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_s) )
            {
                if (!PredictionCollection(X, Y+ 1))
                {
                    Y++;
                }
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_d) )
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
