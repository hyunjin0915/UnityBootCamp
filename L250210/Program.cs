namespace L250210
{
/*    public class Monster : Creature
    {
       
    }
    public class Player : Creature
    {
        
    }
    public class Ground : Objects
    {

    }
    public class Wall : Objects
    {
        
    }
    public class Creature
    {
        public void Move()
        {

        }
    }
    public class Objects
    {
        protected bool isWalkable;
    }
    public class WorldMap
    {
        public Position pos;
    }


    public class Color
    {
        public int R, G, B;

        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
    public class Position
    {
        public int x;
        public int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }
*/
    /*class Image
    {
        Position pos;
        Color color;
       
        public Image(Position position, Color color)
        {
            this.pos = position;
            this.color = color;
        }
        ~Image()
        {
            Console.WriteLine("소멸자");
        }
    }*/

/*    class Apple
    {
        int hp;
        public enum EColor
        {
            Red,
            Green, 
            Blue
        }
        public EColor color;
        public void CanEat()
        {
            hp -= 10;
        }
        public void Drop()
        {

        }
    }
*/    
    internal class Program
    {
      
        static void Main(string[] args)
        {
            /*//Position[] positions = new Position[10];
            //positions[0].x = 10; //오류 X

            Apple apple1 = new Apple();
            apple1.color = Apple.EColor.Red;

            Apple[] appleArry = new Apple[3]; //stack 에 참조변수 heap을 가르킬 변수 생성

            appleArry[0] = new Apple(); // heap 사과 모양 메모리공간 확보

            for (int i = 0; i < 3; i++)
            {
                appleArry[i] = new Apple();
            }

            Image[] carImg = new Image[14];
            carImg[0] = new Image(new Position(0,0), new Color(1, 2,3));
            //...*/

            //WorldMap[] map = new WorldMap[10];

            World world = new World();
        }
    }
}
