using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace L250211
{
    public class Input
    {
        public Input() { }

        static protected ConsoleKeyInfo keyInfo;
        static public void Process()
        {
            keyInfo = Console.ReadKey();
        }
        static public bool GetKeyDown(ConsoleKey key)
        {
            return (keyInfo.Key == key);
        }
    }
}
