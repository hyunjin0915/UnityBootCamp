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

        List<string> scene;

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

            try
            {
                scene = new List<string>();

                sr = new StreamReader(path);
                while (!sr.EndOfStream)
                {
                    scene.Add(sr.ReadLine());
                }
                sr.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.FileName);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("여기는 파일 처리 예외말고 다른 예외");
            }
            finally
            {
                Console.WriteLine("network, 파일 입출력 처리하는 부분");
                sr.Close (); //잘 안됐어도 파일을 닫기는 해야 하니까
            }
            

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
