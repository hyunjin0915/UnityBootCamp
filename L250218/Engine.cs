using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class Engine
    {
        private Engine() { }
        static Engine instance;
        static public Engine Instance
        {
            get
            {
                if (instance == null)
                {
                    return instance = new Engine();
                }
                return instance;
            }
        }

        public World world;
        public Player player;
        public Monster monster;
        public Goal goal;
        public bool isRunning = true;

        
        public IntPtr myWindow;
        public IntPtr myRenderer;
        public SDL.SDL_Event myEvent;
        public bool Init()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
            {
                Console.WriteLine("Fail Init");
                return false;
            }

            myWindow = SDL.SDL_CreateWindow( 
                "Game",
                100, 100,
                640, 480,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            myRenderer = SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC |
                SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);  

            return true;
        }
        public bool Quit()
        {
            SDL.SDL_DestroyWindow(myWindow); //지우기

            SDL.SDL_Quit(); //라이브러리도 지워줘야 함

            return true;
        }

        List<string> scene;

        //더블 버퍼링
        static public char[,] frontBuffer = new char[20, 40];
        static public char[,] backBuffer = new char[20, 40];

        public void Load(string path)
        {
            /*string tempScene = "";
            byte[] buffer = new byte[1024];
            FileStream fs = new FileStream("level01.map", FileMode.Open);
            int offset = 0;

            fs.Seek(0, SeekOrigin.End); //커러를 맨끝으로 보냄
            long fileSize = fs.Position;

            fs.Seek(0, SeekOrigin.Begin); //다시 커서 처음으로
            int readCount = fs.Read(buffer, 0, (int)fileSize);
            tempScene = Encoding.UTF8.GetString(buffer);
            tempScene = tempScene.Replace("\0", "");
            scene = tempScene.Split("\r\n");
*/

            StreamReader sr = null;


            scene = new List<string>();

            sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                scene.Add(sr.ReadLine());
            }
            sr.Close();


            world = new World();
            for (int y = 0; y < scene.Count; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {
                    if (scene[y][x] == '*')
                    {
                        Wall wall = new Wall(x, y, scene[y][x]);

                        world.Instantiate(wall);
                    }
                    else if (scene[y][x] == ' ')
                    {
                        /*Floor floor = new Floor(x, y, scene[y][x]);

                        world.Instantiate(floor);*/
                    }
                    else if (scene[y][x] == 'P')
                    {
                        player = new Player(x, y, scene[y][x]);

                        world.Instantiate(player);
                    }
                    else if (scene[y][x] == 'M')
                    {
                        monster = new Monster(x, y, scene[y][x]);

                        world.Instantiate(monster);
                    }
                    else if (scene[y][x] == 'G')
                    {
                        goal = new Goal(x, y, scene[y][x]);

                        world.Instantiate(goal);
                    }

                    Floor floor = new Floor(x, y,' ');

                    world.Instantiate(floor);
                }
            }
            world.Sort();
        }

        public void InputProcess()
        {
            Input.Process();
        }
        protected void Update()
        {
            world.Update();
        }
        protected void Render()
        {
            SDL.SDL_SetRenderDrawColor(myRenderer, 0, 51, 0, 0);
            SDL.SDL_RenderClear(myRenderer); //붓으로 화면 지우기

            world.Render();

            //back <-> front (flip)
            for (int Y = 0; Y < 20; Y++)
            {
                for (int X = 0; X < 40; X++)
                {
                    if (frontBuffer[Y,X] != backBuffer[Y,X])
                    {
                        frontBuffer[Y, X] = backBuffer[Y, X];
                        Console.SetCursorPosition(X, Y);
                        Console.Write(backBuffer[Y, X]);
                    }
                    
                }
            }
            SDL.SDL_RenderPresent(myRenderer);
        }
        public void Run()
        {
            Console.CursorVisible = false;
            while (isRunning)
            {
                SDL.SDL_PollEvent(out myEvent);

                Time.Update();

                //InputProcess();

                switch (myEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        isRunning = false;
                        break;

                }

                Update();

                Render();

            }
        }
        public void GameOver()
        {
            if(player.X == monster.X && player.Y == monster.Y)
            {
                isRunning = false;
            }
        }
        public void NextLevel()
        {
            if (player.X == goal.X && player.Y == goal.Y)
            {
                player.X = 1;
                player.Y = 1;
            }
        }
    }
}
