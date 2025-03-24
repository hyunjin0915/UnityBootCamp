using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L250324_Thread
{
    internal class Program
    {
        static Object _lock = new Object();

        static SpinLock spinLock = new SpinLock();

        volatile static int Money = 0;

        static bool lockTaken = false;

        static void Add()
        {

            for (int i = 0; i < 100000; i++)
            {
                spinLock.Enter(ref lockTaken);
                Money++;
                spinLock.Exit();

            }
        }
        static void Remove()
        {
            for (int i = 0; i < 100000; i++)
            {
                spinLock.Enter(ref lockTaken);
                Money--;
                spinLock.Exit();
            }

        }


        static void Main(string[] args)
        {
            //OS 에 B함수를 등록해줘 -> instance
            Thread thread1 = new Thread(new ThreadStart(Add));
            Thread thread2 = new Thread(new ThreadStart(Remove));

            thread1.IsBackground = true;
            thread1.Start();
            thread2.IsBackground = true;
            thread2.Start();

            thread1.Join(); //메인 thread와 합친다 : B가 끝날 때까지 기다림
            thread2.Join();

            Console.WriteLine(Money);
        }
    }
}
