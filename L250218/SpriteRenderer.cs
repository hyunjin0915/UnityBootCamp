using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class SpriteRenderer : Component
    {
        public int OrderLayer;

        public char Shape;
        public SDL.SDL_Color color;
        public int spriteSize = 30;

        protected bool isAnimation = false;
        protected IntPtr myTexture;
        protected IntPtr mySurface;

        protected int spriteIndexX = 0;
        protected int spriteIndexY = 0;

        public SDL.SDL_Color colorKey;

        protected string filename;

        private float elapseTime = 0.0f;

        SDL.SDL_Rect sourceRect;
        SDL.SDL_Rect destinationRect; //screen size

        public float processTime = 100.0f;
        public int MaxCellCountX = 5;
        public int MaxCellCountY = 5;

        public SpriteRenderer() { }

        


        public override void Update()
        {
            int X = gameObject.transform.X;
            int Y = gameObject.transform.Y;

            
            
            //Screen Bitmap
            destinationRect.x = X * spriteSize;
            destinationRect.y = Y * spriteSize;
            destinationRect.w = spriteSize;
            destinationRect.h = spriteSize;

            unsafe
            {
                //이미지 정보 가져와서 할 일이 있음
                SDL.SDL_Surface* surface = (SDL.SDL_Surface*)(mySurface);
               
                if (isAnimation)
                {
                    if (elapseTime >= processTime)
                    {
                        elapseTime = 0.0f;
                        spriteIndexX %= MaxCellCountX;
                        spriteIndexX++;
                    }
                    else
                    {
                        elapseTime += Time.deltaTime;
                    }

                    int cellSizeX = (surface->w) / MaxCellCountX;
                    int cellSizeY = (surface->h) / MaxCellCountY;

                    sourceRect.x = cellSizeX * spriteIndexX;
                    sourceRect.y = cellSizeY * spriteIndexY;
                    sourceRect.w = cellSizeX;
                    sourceRect.h = cellSizeY;
                    //spriteIndexX++;

                }
                else
                {
                    sourceRect.x = 0;
                    sourceRect.y = 0;
                    sourceRect.w = surface->w;
                    sourceRect.h = surface->h;
                }
            }
        }

        public virtual void Render()
        {
            int X = gameObject.transform.X;
            int Y = gameObject.transform.Y;

            //Console
            Engine.backBuffer[Y, X] = Shape;

            unsafe
            {
                SDL.SDL_RenderCopy(Engine.Instance.myRenderer, myTexture, ref sourceRect, ref destinationRect);
            }
        }

        public void LoadBMP(string inFilename, bool inIsAnimation = false)
        {
            //bin 폴더에 안 넣고 실행하려고 실행파일의 윗 파일 이름을 가져와서 Data 파일 로딩되게 수정
            string projectFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            isAnimation = inIsAnimation;
            filename = inFilename;

            //SDL C 접근할 수 있는 게 없어서 
            mySurface = SDL.SDL_LoadBMP(projectFolder + "/Data/" + filename);
            unsafe
            {
                //이미지 정보 가져와서 할 일이 있음
                SDL.SDL_Surface* surface = (SDL.SDL_Surface*)(mySurface);
                SDL.SDL_SetColorKey(mySurface, 1, SDL.SDL_MapRGB(surface->format, colorKey.r, colorKey.g, colorKey.b));
            }
            myTexture = SDL.SDL_CreateTextureFromSurface(Engine.Instance.myRenderer, mySurface);
        }
    }
}
