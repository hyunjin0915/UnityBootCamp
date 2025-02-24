using System.Text;

namespace L250218
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            Engine engine = Engine.Instance;
            engine.Load("level02.map");
            engine.Run();
        }
    }
}
