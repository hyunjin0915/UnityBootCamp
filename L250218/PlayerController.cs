using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class PlayerController : Component
    {
        private float elapseTime = 0.005f;

        public override void Update()
        {

            if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_w))
            {
                /*if (!PredictionCollection(X, Y - 1))
                {
                    Y--;
                }
                spriteIndexY = 2;*/
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_a))
            {
                /*if (!PredictionCollection(X - 1, Y))
                {
                    X--;
                }
                spriteIndexY = 0;*/
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_s))
            {
                /*if (!PredictionCollection(X, Y + 1))
                {
                    Y++;
                }
                spriteIndexY = 3;*/
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_d))
            {
                /*if (!PredictionCollection(X + 1, Y))
                {
                    X++;
                }
                spriteIndexY = 1;*/
            }

            
        }

        public void GameOver()
        {



        }

    }
}
