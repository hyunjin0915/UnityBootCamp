using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250211
{
    public class Engine
    {
        private Engine() 
        {

        }
        static Engine instance;
        static public Engine Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Engine();
                }
                return instance;
            }
        }
        public World world;
        Player player;
        protected bool isRunning = true;

        protected ConsoleKeyInfo keyInfo;
        
        public void Load()
        {
            //file에서 로딩
            string[] scene =
            {
                "**********",
                "*P       *",
                "*        *",
                "*        *",
                "*        *",
                "*   M    *",
                "*        *",
                "*        *",
                "*       G*",
                "**********"
            };
            world = new World();

            for (int y = 0; y < scene.Length; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {
                    if(scene[y][x] == '*')
                    {
                        Wall wall = new Wall(x, y, scene[y][x]);
                        
                        world.Instantiate(wall);
                    }
                    else if(scene[y][x] == ' ')
                    {
                        Floor floor = new Floor(x, y, scene[y][x]);
                        
                        world.Instantiate(floor);
                    }
                    else if (scene[y][x] == 'P')
                    {
                         player = new Player(x, y, scene[y][x]);
                        
                        world.Instantiate(player);
                    }
                    else if (scene[y][x] == 'M')
                    {
                        Monster monster = new Monster(x, y, scene[y][x]);
                        
                        world.Instantiate(monster);
                    }
                    else if (scene[y][x] == 'G')
                    {
                        Goal goal = new Goal(x, y, scene[y][x]);
                        
                        world.Instantiate(goal);
                    }
                }
            }
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
            Console.Clear();
            world.Render();
        }
        public void Run()
        {
            while (isRunning)
            {
                InputProcess();
                Update();
                Render();
            }
        }

        /*public void GetKeyDown(ConsoleKeyInfo _keyInfo)
        {
            if(ConsoleKey.UpArrow == _keyInfo.Key)
            {
                player.Y--;
            }
            else if (ConsoleKey.DownArrow == _keyInfo.Key)
            {
                player.Y++;
            }
            else if (ConsoleKey.RightArrow == _keyInfo.Key)
            {
                player.X++;
            }
            else if (ConsoleKey.LeftArrow == _keyInfo.Key)
            {
                player.X--;
            }
        }*/
        
    }
}
