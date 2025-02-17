namespace L250217
{
    public class Fruit()
    {
        

        public void Do()
        {
            Console.WriteLine("부모가 한 일");
        }
        int gold;

        public int Gold { get => gold; set => gold = value; }
    }

    public class Orange : Fruit
    {
        public void Do()
        {

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Orange derived = new Orange();
            derived.Do();

            Fruit fruit = new Fruit();
            fruit.Gold = 5;
        }
    }
}
