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

        public string[] scene;

        public void Load()
        {
            scene = new string[]
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
                    if (scene[y][x] == '*')
                    {
                        Wall wall = new Wall(x, y, scene[y][x]);

                        world.Instantiate(wall);
                    }
                    else if (scene[y][x] == ' ')
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
                        monster = new Monster(x, y, scene[y][x]);

                        world.Instantiate(monster);
                    }
                    else if (scene[y][x] == 'G')
                    {
                        goal = new Goal(x, y, scene[y][x]);

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
                GameOver();
                NextLevel();
            }
            Console.Clear();
            Console.WriteLine("게임 끝");
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
