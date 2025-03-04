using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class GameObject
    {
        public GameObject() { }
        public int X, Y;
        public char Shape;
        public int OrderLayer;
        public bool isTrigger = false;
        public bool isCollide = false;


        public SDL.SDL_Color color;
        public int spriteSize = 30;

        public bool PredictionCollection(int newX, int newY)
        {
            for (int i = 0; i < Engine.Instance.world.GetAllGameObjects.Count; i++)
            {
                if (Engine.Instance.world.GetAllGameObjects[i].isCollide)
                {
                    if ((Engine.Instance.world.GetAllGameObjects[i].X == newX) &&
                        (Engine.Instance.world.GetAllGameObjects[i].Y == newY))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public virtual void Update()
        {

        }
        public virtual void Render()
        {
            Engine.backBuffer[Y, X] = Shape;
            
            SDL.SDL_SetRenderDrawColor(Engine.Instance.myRenderer, color.r, color.g, color.b, color.a);

            SDL.SDL_Rect myRect;
            myRect.x = X * spriteSize;
            myRect.y = Y * spriteSize;
            myRect.w = spriteSize;
            myRect.h = spriteSize;

            SDL.SDL_RenderFillRect(Engine.Instance.myRenderer, ref myRect);
        }

    }
}
