namespace L250218
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Engine engine = Engine.Instance;
            engine.Load();
            engine.Run();

        }
    }
}
