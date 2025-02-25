using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    internal class Input
    {
        public Input()
        {
        }

        static protected ConsoleKeyInfo keyInfo;
        static public void Process()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
            }
            
        }
        static public bool GetKeyDown(ConsoleKey key)
        {
            return (keyInfo.Key == key);
        }

        public static void ClearInput()
        {
            keyInfo = new ConsoleKeyInfo(); //생성자로 새로 초기화해주는 방법밖에 없는,,
        }
    }
}
