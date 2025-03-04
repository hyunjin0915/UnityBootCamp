using SDL2;

namespace L250304
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            //Unity 초기화
            if(SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) <0) //timer, audio.. 모두 초기화하겠다는 뜻, | 연산, enum 형식
            {
                Console.WriteLine("Fail Init");
            }

            //설정 파일 읽어오기

            //창 만들기
            IntPtr myWindow = SDL.SDL_CreateWindow( //메모리 어디에 만들었는지 return
                "Game",
                100, 100,
                640, 480,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            //붓
            IntPtr myRenderer = SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC |
                SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);  //붓, gpu사용 | 주사율 맞춤 | 텍스쳐렌더링지원


            SDL.SDL_Event myEvent;
            bool isRunning = true;
            Random random = new Random();

            //메세지 처리(사용자 처리가 추가 구조를 바꿈)
            while (isRunning) //이벤트가 있는지 끝났는지 물어보는
            {
                SDL.SDL_PollEvent(out myEvent);
                switch (myEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        isRunning = false;
                        break;

                }

                SDL.SDL_SetRenderDrawColor(myRenderer, 0,0,0,0);
                SDL.SDL_RenderClear(myRenderer); //붓으로 화면 지우기


                for (int i = 0; i < 100; ++i)
                {
                    byte r = (byte)(random.Next() % 256);
                    byte g = (byte)(random.Next() % 256);
                    byte b = (byte)(random.Next() % 256);


                    SDL.SDL_Rect myRect;
                    myRect.x = random.Next() % 640 - 200;
                    myRect.y = random.Next() % 480 - 200;
                    myRect.w = random.Next() % 640;
                    myRect.h = random.Next() % 480;

                    SDL.SDL_SetRenderDrawColor(myRenderer, r, g, b, 0);

                    int type = random.Next() % 3;
                    switch (type)
                    {
                        case 0:
                            SDL.SDL_RenderDrawRect(myRenderer, ref myRect);
                            break;
                        case 1:
                            SDL.SDL_RenderFillRect(myRenderer, ref myRect);
                            break;
                        case 2:
                            int step = 10;
                            int x0 = myRect.x;
                            int y0 = myRect.y;

                            double radius = myRect.w;

                            //시작 값
                            int prevX = (int)(radius * Math.Cos(0 * (Math.PI / 180.0f)));
                            int prevY = (int)(radius * Math.Sin(0 * (Math.PI / 180.0f)));
                            for (int angle = 1; angle <= 360 + step; angle += step)
                            {
                                int x = (int)(radius * Math.Cos(angle * (Math.PI / 180.0f)));
                                int y = (int)(radius * Math.Sin(angle * (Math.PI / 180.0f)));

                                //SDL.SDL_RenderDrawPoint(myRenderer, x0 + x, y0 + y);
                                SDL.SDL_RenderDrawLine(myRenderer, x0 + prevX, y0 + prevY, x0 + x, y0 + y);
                                prevX = x;
                                prevY = y;
                            }
                            break;
                    }
                }

                SDL.SDL_RenderPresent(myRenderer); //CPU 에서 작업 → GPU에 작업하라고 던져주는 과정 필요
            }

            SDL.SDL_DestroyWindow(myWindow); //지우기

            SDL.SDL_Quit(); //라이브러리도 지워줘야 함
        }
    }
}
