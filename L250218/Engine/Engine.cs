using SDL2;
using System;
using System.Collections.Generic;
//using System.Linq;
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
        public IntPtr Font;
        public PlayerController player;
        public AIController monster;
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

            string projectFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            SDL_ttf.TTF_Init();
            //SDL_ttf.TTF_OpenFont(projectFolder + "/data/" + 30);
            Font =  SDL_ttf.TTF_OpenFont("c:/Windows/Fonts/gulim.ttc", 30);
            world = new World();
            return true;
        }
        public bool Quit()
        {
            SDL_ttf.TTF_Quit();
            isRunning = false;
            SDL.SDL_DestroyRenderer(myRenderer);
            SDL.SDL_DestroyWindow(myWindow); //지우기

            SDL.SDL_Quit(); //라이브러리도 지워줘야 함

            return true;
        }

        

        //더블 버퍼링
        static public char[,] frontBuffer = new char[20, 40];
        static public char[,] backBuffer = new char[20, 40];

        public void Load(string path)
        {
            List<string> scene = new List<string>();
            StreamReader sr = new StreamReader(path);

            while (!sr.EndOfStream)
            {
                scene.Add(sr.ReadLine());
            }
            sr.Close();


            
            for (int y = 0; y < scene.Count; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {
                    if (scene[y][x] == '*')
                    {
                        GameObject wall = new GameObject();
                        wall.name = "Wall";
                        wall.transform.X = x;
                        wall.transform.Y = y;

                        SpriteRenderer spriteRenderer = wall.AddComponent<SpriteRenderer>();
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.OrderLayer = 2;
                        spriteRenderer.LoadBMP("wall.bmp");

                        wall.AddComponent<BoxCollider2D>();

                        spriteRenderer.Shape = 'W';

                        world.Instantiate(wall);
                        
                    }
                    else if (scene[y][x] == 'P')
                    {
                        GameObject player = new GameObject();
                        player.name = "Player";
                        player.transform.X = x;
                        player.transform.Y = y;

                        player.AddComponent<PlayerController>(new PlayerController());
                        SpriteRenderer spriteRenderer =  player.AddComponent<SpriteRenderer>(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 0;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.LoadBMP("player.bmp", true);
                        spriteRenderer.processTime = 150.0f;
                        spriteRenderer.MaxCellCountX = 5;
                        spriteRenderer.OrderLayer = 5;
                        spriteRenderer.Shape = 'P';

                        player.AddComponent<CharacterController2D>();
                        

                        world.Instantiate(player);
                    }
                    else if (scene[y][x] == 'M')
                    {
                        GameObject monster = new GameObject();
                        monster.name = "Monster";
                        monster.transform.X = x;
                        monster.transform.Y = y;

                        monster.AddComponent<AIController>(new AIController());
                        CharacterController2D characterController2D = monster.AddComponent<CharacterController2D>();
                        characterController2D.isTrigger = true;

                        SpriteRenderer spriteRenderer = monster.AddComponent<SpriteRenderer>(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.OrderLayer = 4;
                        spriteRenderer.LoadBMP("monster.bmp");


                        spriteRenderer.Shape = 'M';

                        world.Instantiate(monster);
                    }
                    else if (scene[y][x] == 'G')
                    {
                        GameObject goal = new GameObject();
                        goal.name = "Goal";
                        goal.transform.X = x;
                        goal.transform.Y = y;

                        SpriteRenderer spriteRenderer = goal.AddComponent<SpriteRenderer>(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.OrderLayer = 3;
                        spriteRenderer.LoadBMP("goal.bmp");

                        spriteRenderer.Shape = 'G';
                        CircleCollider2D circleCollider2D = goal.AddComponent<CircleCollider2D>(new CircleCollider2D());
                        circleCollider2D.isTrigger = true;

                        world.Instantiate(goal);
                    }

                    GameObject floor = new GameObject();
                    floor.name = "Floor";
                    floor.transform.X = x;
                    floor.transform.Y = y;

                    SpriteRenderer fspriteRenderer = floor.AddComponent<SpriteRenderer>(new SpriteRenderer());
                    fspriteRenderer.colorKey.r = 255;
                    fspriteRenderer.colorKey.g = 255;
                    fspriteRenderer.colorKey.b = 255;
                    fspriteRenderer.colorKey.a = 255;
                    fspriteRenderer.OrderLayer = 1;
                    fspriteRenderer.LoadBMP("floor.bmp");


                    fspriteRenderer.Shape = 'F';

                    world.Instantiate(floor);
                }
                //심판 생성
                GameObject gameManager = new GameObject();
                gameManager.name = "GameManager";
                gameManager.AddComponent<GameManager>();
                world.Instantiate(gameManager);
            }
            world.Sort();
            Awake();
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
                    if (frontBuffer[Y, X] != backBuffer[Y, X])
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
        public void Awake()
        {
            world.Awake();
        }
        public void setSortCompare(World.SortCompare inSortCompare)
        {
            world.sortCompare = inSortCompare;
        }

    }
}
