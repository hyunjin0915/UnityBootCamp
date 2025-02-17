namespace L250211
{
    internal class Program
    {
        /*class Singleton()
        {
            private Singleton()
            {
            }
            static Singleton instance;
            static public Singleton Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
                
                
            }
        }*/
        static void Main(string[] args)
        {
            /*Monster goblin = new Goblin(10, true, 4, 1);
            Player player = new Player(10, 5, 3, true);*/
            
            Engine engine = Engine.Instance;
            engine.Load();
            engine.Run();
        }
    }
}
